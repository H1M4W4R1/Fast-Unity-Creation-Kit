using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Data.Attributes;
using FastUnityCreationKit.Data.Interfaces;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Data.Abstract
{
    /// <summary>
    /// This is a container that is auto-populated with data that is stored in an addressable asset group.
    /// </summary>
    /// <typeparam name="TDataType">Type of data that is stored in the container.</typeparam>
    [NoDuplicates] [NoNullEntries] [OnlySealedElements]
    public abstract class AddressableDataContainer<TDataType> : SerializedScriptableObject,
        IDataContainer<TDataType>, IAutoPopulatedContainer
        where TDataType : Object
    {
        /// <summary>
        /// Internal data container.
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        [OdinSerialize]
        protected readonly AddressableDataContainerStorageObject internalContainer = new();

        [Header("Addressable Asset Group")]
        [Required]
        [SerializeField]
        [NotNull]
        protected string addressableTag = string.Empty;

        /// <summary>
        /// Checks if the data container is populated.
        /// </summary>
        public bool IsPopulated => _isCompleted;

        public async UniTask Populate()
        {
            // Clear loading status and internal container
            _isCompleted = false;
            internalContainer.Clear();

            // Load all assets from the addressable asset group based on asset tag
            _loadHandle = Addressables.LoadAssetsAsync<TDataType>(addressableTag,
                foundObject => internalContainer.Add(foundObject));

            // Wait for loading to be completed.
            await _loadHandle.Task;

            // Mark assets as loaded
            _isCompleted = true;
        }

        /// <summary>
        /// Internal data container storage object. 
        /// </summary>
        [Serializable]
        protected sealed class AddressableDataContainerStorageObject : DataContainerBase<TDataType>
        {
        }

#region LOADING_DATA

        private bool _isCompleted;
        private AsyncOperationHandle<IList<TDataType>> _loadHandle;

#endregion

#region IDataContainer

        public TDataType this[int index] => internalContainer[index];

        public IReadOnlyList<TDataType> All => internalContainer.All;

        public void Add(TDataType data) => internalContainer.Add(data);

        public void Remove(TDataType data) => internalContainer.Remove(data);

        public void Clear() => internalContainer.Clear();

        public bool Contains(TDataType data) => internalContainer.Contains(data);

        public int Count => internalContainer.Count;

        public void RemoveAt(int index) => internalContainer.RemoveAt(index);

#endregion
    }
}