using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Addressables;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Data.Abstract;
using FastUnityCreationKit.Data.Interfaces;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.Data.Containers
{
    /// <summary>
    ///     Storage for addressable definitions.
    /// </summary>
    [AddressableGroup(DATABASE_ADDRESSABLE_TAG)]
    public abstract class AddressableDatabase<TSelfSealed, TDataType> : AddressableDataContainer<TDataType>,
        IDatabase<TDataType>
        where TSelfSealed : AddressableDatabase<TSelfSealed, TDataType>, new()
        where TDataType : Object
    {
        [ShowInInspector] [ReadOnly] private static TSelfSealed _instance;
        private static AsyncOperationHandle<TSelfSealed> _databaseInstanceAcquireHandle;

        /// <summary>
        ///     Internal flag to check if the database is preloading.
        /// </summary>
        private bool _isPreloading;

        /// <summary>
        ///     Handle for the preload operation.
        /// </summary>
        private AsyncOperationHandle<IList<TDataType>> _preloadOperationHandle;

        /// <summary>
        ///     Amount of times this database is preloaded.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_PREVIEW)] [NonSerialized] private int _preloadCount;

        /// <summary>
        ///     Keys of the preloaded objects.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_PREVIEW)] [NonSerialized]
        private List<string> _preloadedObjectKeys = new();

        /// <summary>
        ///     List of preloaded objects.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_PREVIEW)] [NonSerialized]
        private List<TDataType> _preloadedObjectsContainer = new();

        /// <summary>
        ///     Check if the database is preloaded.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_PREVIEW)] public bool IsPreloaded => _preloadCount > 0;

        /// <summary>
        ///     Amount of preloaded objects.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_PREVIEW)] public int PreloadedCount
            => _preloadedObjectsContainer.Count;

        /// <summary>
        ///     Get the instance of the database.
        /// </summary>
        public static TSelfSealed Instance
        {
            get
            {
                // ReferenceEquals is used to check if the instance is null
                // this way we don't check Unity object lifecycle which is expensive
                // so the performance of this method is as fast as possible
                // and on-par with regular class null check.
                //
                // We assume that instance will be either null or a reference to database
                // ScriptableObject, so we don't need to check if it's destroyed.
                // The issue will arise if user will destroy the database, but it's not
                // within specification to destroy the database, as database is managed by the system. 
                if (!ReferenceEquals(_instance, null)) return _instance;

                // Prevent loading the database multiple times
                if (_databaseInstanceAcquireHandle.IsValid() && !_databaseInstanceAcquireHandle.IsDone)
                {
                    _databaseInstanceAcquireHandle.WaitForCompletion();
                    _instance = _databaseInstanceAcquireHandle.Result;
                    return _instance;
                }

                // Load the database 
                _databaseInstanceAcquireHandle =
                    Addressables.LoadAssetAsync<TSelfSealed>(typeof(TSelfSealed).GetCompilableNiceFullName());
                _databaseInstanceAcquireHandle.WaitForCompletion();

                _instance = _databaseInstanceAcquireHandle.Result;
                return _instance;
            }
        }

        /// <summary>
        /// Wait until the database is loaded.
        /// </summary>
        public async UniTask WaitUntilLoaded()
        {
            // If database is already preloaded, return
            if (IsPreloaded) return;
            
            // Run preload operation
            if(!_isPreloading)
                Preload();
            
            while (!_databaseInstanceAcquireHandle.IsDone)
                await UniTask.Yield();
        }
        
        [NotNull] public TSelfSealed EnsurePreloaded()
        {
            // If database is already preloaded, return the instance
            if (IsPreloaded) return (TSelfSealed) this;

            // Preload the database
            Preload();

            return (TSelfSealed) this;
        }

        /// <summary>
        ///     Preload the database.
        /// </summary>
        public void Preload(bool waitForComplete = true)
        {
            // If database is not yet preloaded and preload operation is not running
            if (!IsPreloaded && !_isPreloading)
            {
                _isPreloading = true;

                // Load resource locations to prevent exceptions being 
                // thrown left and right due to bad architecture of Addressables package.
                IList<IResourceLocation> resourceLocations = Addressables
                    .LoadResourceLocationsAsync(addressableTags, MergeMode)
                    .WaitForCompletion();

                // Update resource locations to only include objects of type TDataType
                List<string> resourceKeys = resourceLocations.Where(IsValidLocation)
                    .Select(location => location.PrimaryKey)
                    .ToList();

                // Check if any objects were found
                if (resourceLocations.Count == 0)
                {
                    Guard<ValidationLogConfig>.Warning("No objects found in database " +
                                                       $"{GetType().GetCompilableNiceFullName()}.");
                    return;
                }

                // Load the objects if any were found
                _preloadOperationHandle = Addressables.LoadAssetsAsync<TDataType>(resourceKeys,
                    foundObject =>
                    {
                        // Check if the object is already in the container
                        // to prevent duplicates during race conditions
                        if (_preloadedObjectsContainer.Contains(foundObject)) return;

                        // Add the object to the container 
                        _preloadedObjectsContainer.Add(foundObject);
                        _preloadedObjectKeys.Add(foundObject.name);
                    }, MergeMode);
                
                // Switch the flag off when the operation is completed
                _preloadOperationHandle.Completed += handle =>
                {
                    _isPreloading = false;
                };

                // Wait for the operation to complete if required
                if (waitForComplete) _preloadOperationHandle.WaitForCompletion();
            }

            // Increase preload count 
            _preloadCount++;
            return;

            bool IsValidLocation([NotNull] IResourceLocation location, int index)
            {
                return IsValidType(location.ResourceType);
            }

            bool IsValidType([NotNull] Type type)
            {
                // For same class or value type
                if (type == typeof(TDataType)) return true;

                // For subclasses
                if (type.IsSubclassOf(typeof(TDataType))) return true;

                return false;
            }
        }

        public void Unload()
        {
            if (_preloadCount == 0) return;

            // Reduce preload count
            _preloadCount--;
            if (_preloadCount > 0) return;

            // Unload the handle
            if (_preloadOperationHandle.IsValid())
            {
                _preloadOperationHandle.WaitForCompletion();
                Addressables.Release(_preloadOperationHandle);
            }

            _preloadOperationHandle = default;

            _preloadedObjectsContainer.Clear();
            _preloadedObjectKeys.Clear();

            _isPreloading = false;
        }

        [CanBeNull] public TDataType GetElementAt(int index)
        {
            if (!IsPreloaded)
            {
                Guard<ValidationLogConfig>.Error(
                    $"Database {GetType().GetCompilableNiceFullName()} is not preloaded.");
                return null;
            }

            // Check if index is within bounds
            if (index < 0 || index >= _preloadedObjectsContainer.Count)
            {
                Guard<ValidationLogConfig>.Error(
                    $"Index {index} is out of bounds for database {GetType().GetCompilableNiceFullName()}.");
                return null;
            }

            return _preloadedObjectsContainer[index];
        }

#if UNITY_EDITOR
        [Button("Preload", ButtonSizes.Medium)] [TitleGroup(GROUP_DEBUG)] private void PreloadInEditor()
        {
            EnsurePreloaded();
        }

        [Button("Unload", ButtonSizes.Medium)] [TitleGroup(GROUP_DEBUG)] private void UnloadInEditor()
        {
            Unload();
        }

#endif
    }
}