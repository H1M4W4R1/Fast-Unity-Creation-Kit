using System;
using System.Collections;
using System.Collections.Generic;
using FastUnityCreationKit.Annotations.Addressables;
using FastUnityCreationKit.Data.Abstract;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Utility.Logging;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Data.Containers
{
    /// <summary>
    /// Storage for addressable definitions.
    /// </summary>
    [AddressableGroup(DatabaseConstants.DATABASE_ADDRESSABLE_TAG)]
    public abstract class AddressableDatabase<TSelfSealed, TDataType> : AddressableDataContainer<TDataType>, IDatabase<TDataType>
        where TSelfSealed : AddressableDatabase<TSelfSealed, TDataType>, new()
        where TDataType : Object
    {
        /// <summary>
        /// Amount of times this database is preloaded.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TabGroup("Preload")] [NonSerialized]
        private int _preloadCount;
        
        /// <summary>
        /// Keys of the preloaded objects.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TabGroup("Preload")] [NonSerialized]
        private List<string> _preloadedObjectKeys = new List<string>();
        
        /// <summary>
        /// List of preloaded objects.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TabGroup("Preload")] [NonSerialized]
        private List<TDataType> _preloadedObjectsContainer = new List<TDataType>();
        
        /// <summary>
        /// Handle for the preload operation.
        /// </summary>
        private AsyncOperationHandle<IList<TDataType>> _preloadOperationHandle;

        [ShowInInspector] [ReadOnly] [TabGroup("Debug")]
        private static TSelfSealed _instance;
        private static AsyncOperationHandle<TSelfSealed> _databaseInstanceAcquireHandle;
        
        /// <summary>
        /// Check if the database is preloaded.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TabGroup("Debug")]
        public bool IsPreloaded => _preloadCount > 0;
        
        /// <summary>
        /// Amount of preloaded objects.
        /// </summary>
        public int PreloadedCount => _preloadedObjectsContainer.Count;

        public TSelfSealed EnsurePreloaded()
        {
            if (!IsPreloaded)
                Preload();
            
            return (TSelfSealed) this;
        }
        
        /// <summary>
        /// Preload the database.
        /// </summary>
        public void Preload(bool waitForComplete = true)
        {
            // If database is not yet preloaded and preload operation is not running
            if (!IsPreloaded && !_preloadOperationHandle.IsValid())
            {
                // Load all objects in the database into cache.
                _preloadOperationHandle = Addressables.LoadAssetsAsync<TDataType>((IEnumerable) addressableTags,
                    foundObject =>
                    {
                        // Add the object to the container
                        _preloadedObjectsContainer.Add(foundObject);
                        _preloadedObjectKeys.Add(foundObject.name);
                    }, MergeMode);
                
                // Wait for the operation to complete if required
                if(waitForComplete)
                    _preloadOperationHandle.WaitForCompletion();
            }
            
            // Increase preload count
            _preloadCount++;
        }

        public void Unload()
        {
            // Reduce preload count
            _preloadCount--;
            if(_preloadCount > 0) return;
            
            // Unload the handle
            Addressables.Release(_preloadOperationHandle);
            _preloadOperationHandle = default;
            
            _preloadedObjectsContainer.Clear();
            _preloadedObjectKeys.Clear();
        }

        [CanBeNull] public TDataType GetElementAt(int index)
        {
            if (!IsPreloaded)
            {
                Guard<ValidationLogConfig>.Error($"Database {GetType().GetCompilableNiceFullName()} is not preloaded.");
                return null;
            }
            
            // Check if index is within bounds
            if (index < 0 || index >= _preloadedObjectsContainer.Count)
            {
                Guard<ValidationLogConfig>.Error($"Index {index} is out of bounds for database {GetType().GetCompilableNiceFullName()}.");
                return null;
            }
            
            return _preloadedObjectsContainer[index];
        }
        
        /// <summary>
        /// Get the instance of the database.
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
                _databaseInstanceAcquireHandle = Addressables.LoadAssetAsync<TSelfSealed>(typeof(TSelfSealed).GetCompilableNiceFullName());
                _databaseInstanceAcquireHandle.WaitForCompletion();

                _instance = _databaseInstanceAcquireHandle.Result;
                return _instance;
            }
        }
    }
}