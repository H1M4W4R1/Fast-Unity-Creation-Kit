using System.Collections;
using System.Collections.Generic;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Logging;
using UnityEngine;

namespace FastUnityCreationKit.Data.Abstract
{
    /// <summary>
    /// Represents a core data container that is used to store data of a specific type.
    /// </summary>
    public abstract class IndexedDataContainerBase<TIndexType, TDataType> : 
        IIndexedDataContainer<TIndexType, TDataType>
    {
        protected readonly List<TDataType> data = new();
        protected readonly List<TIndexType> indices = new();
     
        public IndexedDataContainerBase()
        {
            // Auto populate the data if the container implements IAutoPopulatedContainer
            if (this is IAutoPopulatedContainer autoPopulateDataContainer)
                autoPopulateDataContainer.Populate();
        }
        
        public virtual void Add(TIndexType index, TDataType obj)
        {
            // Check if this is a unique data container
            if (this is IUniqueDataContainer && ContainsValue(obj))
            {
                Guard<EditorAutomationLogConfig>.Warning($"Data already exists in the container [{GetType()}].");
                return;
            }
            
            data.Add(obj);
            indices.Add(index);
        }

        public virtual void Remove(TIndexType index)
        {
            int indexIndex = indices.IndexOf(index);
            if (indexIndex == -1) return;
            
            // Remove the data and the index
            data.RemoveAt(indexIndex);
            indices.RemoveAt(indexIndex);
        }

        public virtual void Clear()
        {
            data.Clear();
            indices.Clear();
        }

        public virtual bool ContainsValue(TDataType obj) => data.Contains(obj);

        public virtual bool ContainsIndex(TIndexType index) => indices.Contains(index);

        public virtual int Count => data.Count;

        public virtual TDataType this[int index] => data[index];

        public virtual TDataType this[TIndexType index]
        {
            get
            {
                int indexIndex = indices.IndexOf(index);
                return indexIndex == -1 ? default : data[indexIndex];
            }
        }

        public IList RawData => data;
    }
}