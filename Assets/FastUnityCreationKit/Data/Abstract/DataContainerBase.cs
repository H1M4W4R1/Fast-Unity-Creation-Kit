using System;
using System.Collections.Generic;
using FastUnityCreationKit.Annotations.Editor;
using FastUnityCreationKit.Annotations.Utility;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Data.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace FastUnityCreationKit.Data.Abstract
{
    /// <summary>
    ///     Represents a core data container that is used to store data of a specific type.
    /// </summary>
    [Serializable] [Polymorph]
    public abstract class DataContainerBase<TDataType> : IDataContainer<TDataType>,
        ISerializationCallbackReceiver
    {
        /// <summary>
        /// Serialized data of the container.
        /// Data is serialized using Odin Serializer thus polymorphism is supported.
        /// </summary>
        [SerializeField] [HideInInspector] [ReadOnly] [BinaryData]
        protected byte[] dataSerialized;
        
        /// <summary>
        ///     Data storage. Not serialized as it's provided
        ///     with custom serialization.
        /// </summary>
        [ShowInInspector] [ReadOnly] [NonSerialized] [Required]
        protected List<TDataType> data = new();

        public DataContainerBase()
        {
            // Auto populate the data if the container implements IAutoPopulatedContainer
            if (this is IAutoPopulatedContainer autoPopulateDataContainer) autoPopulateDataContainer.Populate();
        }

        /// <summary>
        ///     Indexer for the data.
        /// </summary>
        public TDataType this[int index] => data[index];

        public IReadOnlyList<TDataType> All => data;

        /// <summary>
        ///     Adds the data to the container.
        /// </summary>
        /// <remarks>
        ///     If container is of type IUniqueDataContainer, it will check if the data already exists in the container.
        /// </remarks>
        public virtual void Add(TDataType obj)
        {
            if (this is IUniqueDataContainer && Contains(obj))
            {
                Guard<ValidationLogConfig>.Warning($"Data already exists in the container [{GetType()}].");
                return;
            }

            data.Add(obj);
        }

        /// <summary>
        ///     Removes the data from the container.
        /// </summary>
        public virtual void Remove(TDataType obj)
        {
            data.Remove(obj);
        }

        /// <summary>
        ///     Clears all data from the container.
        /// </summary>
        public virtual void Clear()
        {
            data.Clear();
        }

        /// <summary>
        ///     Checks if the container contains the data.
        /// </summary>
        public virtual bool Contains(TDataType obj)
        {
            return data.Contains(obj);
        }

        /// <summary>
        ///     Count of the data items in the container.
        /// </summary>
        public virtual int Count => data.Count;

        /// <summary>
        ///     Removes the data at the specified index.
        /// </summary>
        public virtual void RemoveAt(int index)
        {
            data.RemoveAt(index);
        }

        public void OnBeforeSerialize()
        {
            // Serialize internal container using Odin Serializer
            dataSerialized = SerializationUtility.SerializeValue(data, DataFormat.Binary);
        }

        public void OnAfterDeserialize()
        {
            // Deserialize internal container using Odin Serializer
            data = SerializationUtility.DeserializeValue<List<TDataType>>(dataSerialized, DataFormat.Binary);
            
            // Clear the serialized data to avoid consuming too much memory
            dataSerialized = Array.Empty<byte>();
        }
    }
}