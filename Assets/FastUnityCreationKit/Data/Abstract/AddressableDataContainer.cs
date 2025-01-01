using System;
using System.Collections.Generic;
using FastUnityCreationKit.Annotations.Attributes;
using FastUnityCreationKit.Data.Containers;
using FastUnityCreationKit.Data.Interfaces;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Data.Abstract
{
    /// <summary>
    /// This is a container that is auto-populated with data that is stored in an addressable asset group.
    /// </summary>
    /// <typeparam name="TDataType">Type of data that is stored in the container.</typeparam>
    [NoDuplicates] [NoNullEntries] [OnlySealedElements] 
    public abstract class AddressableDataContainer<TDataType> : SerializedScriptableObject,
        IDataContainer<AddressableReferenceEntry<TDataType>>, IIndexableBy<AssetReferenceT<TDataType>, string>
        where TDataType : Object
    {
        [Header("Addressable Asset Group")]
        [Required]
        [SerializeField]
        [NotNull] [TabGroup("Configuration", Order = -1)]
        [ListDrawerSettings(ShowFoldout = false)]
        [ReadOnly] [Tooltip("Tags are defined by developer and are used to filter the addressable assets.")]
        protected string[] addressableTags = Array.Empty<string>();
        
        /// <summary>
        /// Internal data container.
        /// </summary>
        [ShowInInspector] [ReadOnly] [OdinSerialize] [TabGroup("Debug")]
        protected readonly AddressableDataContainerStorageObject internalContainer = new();
      
        [ShowInInspector] [ReadOnly] [TabGroup("Configuration")]
        [Tooltip("This setting is configured by the developer and is used to determine how the data is merged.")]
        protected virtual Addressables.MergeMode MergeMode => Addressables.MergeMode.Union; 

        /// <summary>
        /// Internal data container storage object. 
        /// </summary>
        [Serializable]
        protected sealed class AddressableDataContainerStorageObject : 
            DataContainerBase<AddressableReferenceEntry<TDataType>>,
            IIndexableBy<AssetReferenceT<TDataType>, string>
        {
            public AssetReferenceT<TDataType> this[string index]
            {
                get
                {
                    // Loop through all elements in the container
                    for (int i = 0; i < Count; i++)
                    {
                        // Get the element
                        AddressableReferenceEntry<TDataType> element = this[i];

                        // Check if the key is the same as the index
                        if (element.Address == index)
                            return element.Entry;
                    }
                    
                    // Return null if not found
                    return null;
                }
            }
        }

#region IDataContainer

        public IReadOnlyList<AddressableReferenceEntry<TDataType>> All => internalContainer.All;
        public void Add(AddressableReferenceEntry<TDataType> data) => internalContainer.Add(data);
        public void Remove(AddressableReferenceEntry<TDataType> data) => internalContainer.Remove(data);
        public void Clear() => internalContainer.Clear();

        public bool Contains(AddressableReferenceEntry<TDataType> data)
        {
            for(int i = 0; i < internalContainer.Count; i++)
            {
                AddressableReferenceEntry<TDataType> entryDefinition = internalContainer[i];
                
                if (entryDefinition.Address != data.Address)
                    continue;

                if (entryDefinition.Entry == null)
                    return data.Entry == null;
                
                // Check if the asset GUID is the same
                if (entryDefinition.Entry.AssetGUID == data.Entry.AssetGUID)
                    return true;
            }
            
            return false;
        }
        public AddressableReferenceEntry<TDataType> this[int index] => internalContainer[index];
        
        public virtual int Count => internalContainer.Count;

        public void RemoveAt(int index) => internalContainer.RemoveAt(index);

#endregion

        public AssetReferenceT<TDataType> this[string index] => internalContainer[index];


    }
}