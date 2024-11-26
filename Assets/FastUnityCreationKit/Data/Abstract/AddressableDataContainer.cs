using System.Collections.Generic;
using FastUnityCreationKit.Data.Interfaces;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FastUnityCreationKit.Data.Abstract
{
    /// <summary>
    /// This is a container that is auto-populated with data that is stored in an addressable asset group.
    /// </summary>
    /// <typeparam name="TDataType"></typeparam>
    public abstract class AddressableDataContainer<TDataType> : DataContainerBase<TDataType>, IAutoPopulatedContainer
        where TDataType : Object
    {
        [Header("Addressable Asset Group")]
        [Required]
        [SerializeField]
        [NotNull]
        protected AddressableAssetGroup addressableAssetGroup = default!;
        
        private int _loadCount;
        private int _loadedCount;
        
        /// <summary>
        /// Checks if the data container is populated.
        /// </summary>
        public bool IsPopulated => _loadedCount == _loadCount;
        
        public void Populate()
        {
            // Clear the data before populating
            Clear();

            // Get all the assets in the addressable asset group
            ICollection<AddressableAssetEntry> entries = addressableAssetGroup.entries;

            // Add all the assets to the data container
            foreach (AddressableAssetEntry entry in entries)
            {
                // Load asset
                _loadCount++;
                AsyncOperationHandle<TDataType> loadRequest = Addressables.LoadAssetAsync<TDataType>(entry.guid);
                loadRequest.Completed += handle =>
                {
                    Add(handle.Result);
                    _loadedCount++;
                };
                
                // We don't store the handle because we don't need to release the asset
                // as it is stored in the data container permanently
                
                // Also we don't care about order of completion as we are just populating the data
                // so we don't need to wait for the completion of the load request
            }
        }
    }
}