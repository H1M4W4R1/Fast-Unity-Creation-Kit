using System;
using System.Collections.Generic;
using FastUnityCreationKit.Annotations.Data;
using FastUnityCreationKit.Annotations.Utility;
using FastUnityCreationKit.Data.Containers;
using FastUnityCreationKit.Data.Interfaces;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.Data.Abstract
{
    /// <summary>
    ///     This is a container that is auto-populated with data that is stored in an addressable asset group.
    /// </summary>
    /// <remarks>
    ///     This type contains <see cref="OdinSerializeAttribute"/> properties, however, it is not required to
    ///     use <see cref="RequiresOdinSerializationAttribute"/> to mark it as Odin-serialized. This is because
    ///     the type is derived from <see cref="SerializedScriptableObject"/> which is already serialized
    ///     by Odin Serializer and as it's <see cref="ScriptableObject"/> it will exist in asset database and be
    ///     automatically handled by the serialization system.
    /// </remarks>
    /// <typeparam name="TDataType">Type of data that is stored in the container.</typeparam>
    [NoDuplicates] [NoNullEntries] [OnlySealedElements]
    public abstract class AddressableDataContainer<TDataType> : SerializedScriptableObject,
        IDataContainer<AddressableReferenceEntry<TDataType>>, IIndexableBy<AssetReferenceT<TDataType>, string>
        where TDataType : Object
    {
        /// <summary>
        ///     Addressable tags that are used to filter the addressable assets.
        ///     Can be used with <see cref="MergeMode"/> to create interference or union of the data.
        /// </summary>
        [Required]
        [SerializeField]
        [NotNull]
        [TitleGroup(GROUP_CONFIGURATION)]
        [ListDrawerSettings(ShowFoldout = false)]
        [ReadOnly]
        [Tooltip("Tags are defined by developer and are used to filter the addressable assets.")]
        protected List<string> addressableTags = new();

        /// <summary>
        ///     Internal data container.
        /// </summary>
        [ShowInInspector] [ReadOnly] [OdinSerialize] [NonSerialized] [TitleGroup(GROUP_DEBUG, order: int.MaxValue)]
        protected readonly AddressableDataContainerStorageObject internalContainer = new();

        /// <summary>
        ///     Merge mode used to determine how the data is merged - union or intersection is the most common
        ///     use case for this setting.
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        [TitleGroup(GROUP_CONFIGURATION)]
        [Tooltip("This setting is configured by the developer and is used to determine how the data is merged.")]
        protected virtual Addressables.MergeMode MergeMode => Addressables.MergeMode.Union;

        public AssetReferenceT<TDataType> this[string index] => internalContainer[index];

        /// <summary>
        ///     Internal data container storage object.
        /// </summary>
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
                        if (ReferenceEquals(element, null)) continue;

                        // Check if the key is the same as the index
                        if (element.Address == index) return element.Entry;
                    }

                    // Return null if not found
                    return null;
                }
            }
        }

#region IDataContainer

        public IReadOnlyList<AddressableReferenceEntry<TDataType>> All => internalContainer.All;

        public void Add(AddressableReferenceEntry<TDataType> data)
        {
            internalContainer.Add(data);
        }

        public void Remove(AddressableReferenceEntry<TDataType> data)
        {
            internalContainer.Remove(data);
        }

        public void Clear()
        {
            internalContainer.Clear();
        }

        public bool Contains(AddressableReferenceEntry<TDataType> data)
        {
            for (int i = 0; i < internalContainer.Count; i++)
            {
                AddressableReferenceEntry<TDataType> entryDefinition = internalContainer[i];
                if (ReferenceEquals(entryDefinition, null)) continue;

                if (entryDefinition.Address != data.Address) continue;

                if (entryDefinition.Entry == null) return data.Entry == null;

                // Check if the asset GUID is the same
                if (entryDefinition.Entry.AssetGUID == data.Entry.AssetGUID) return true;
            }

            return false;
        }

        public AddressableReferenceEntry<TDataType> this[int index] => internalContainer[index];

        public virtual int Count => internalContainer.Count;

        public void RemoveAt(int index)
        {
            internalContainer.RemoveAt(index);
        }

#endregion
    }
}