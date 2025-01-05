using System;
using System.Collections.Generic;
using FastUnityCreationKit.Annotations.Utility;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Data.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace FastUnityCreationKit.Data.Abstract
{
    /// <summary>
    ///     Represents a core data container that is used to store data of a specific type.
    /// </summary>
    [RequiresOdinSerialization]
    public abstract class DataContainerBase<TDataType> : IDataContainer<TDataType>
    {
        /// <summary>
        ///     Data storage.
        /// </summary>
        [ShowInInspector] [ReadOnly] [OdinSerialize] [NonSerialized] [Required]
        protected readonly List<TDataType> data = new();

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
    }
}