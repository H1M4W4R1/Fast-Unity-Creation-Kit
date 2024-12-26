using System;
using System.Collections.Generic;
using FastUnityCreationKit.Data.Attributes;
using FastUnityCreationKit.Data.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;

namespace FastUnityCreationKit.Data.Abstract
{
    /// <summary>
    /// Represents a core data container that is used to store data of a specific type.
    /// </summary>
    public abstract class DataContainerBase<TDataType> : IDataContainer<TDataType>, ISelfValidator
    {
        /// <summary>
        /// Data storage. 
        /// </summary>
        [ShowInInspector] [ReadOnly] [OdinSerialize]
        protected readonly List<TDataType> data = new();
        
        /// <summary>
        /// Indexer for the data.
        /// </summary>
        public TDataType this[int index] => data[index];

        public DataContainerBase()
        {
            // Auto populate the data if the container implements IAutoPopulatedContainer
            if (this is IAutoPopulatedContainer autoPopulateDataContainer)
                autoPopulateDataContainer.Populate();
        }

        public IReadOnlyList<TDataType> All => data;

        /// <summary>
        /// Adds the data to the container.
        /// </summary>
        /// <remarks>
        /// If container is of type IUniqueDataContainer, it will check if the data already exists in the container.
        /// </remarks>
        public virtual void Add(TDataType obj)
        {
            if(this is IUniqueDataContainer && Contains(obj))
            {
                Debug.LogError($"Data already exists in the container [{GetType()}].");
                return;
            }
            
            this.data.Add(obj);
        }

        /// <summary>
        /// Removes the data from the container.
        /// </summary>
        public virtual void Remove(TDataType obj) => this.data.Remove(obj);
        
        /// <summary>
        /// Clears all data from the container.
        /// </summary>
        public virtual void Clear() => data.Clear();
        
        /// <summary>
        /// Checks if the container contains the data.
        /// </summary>
        public virtual bool Contains(TDataType obj) => this.data.Contains(obj);
        
        /// <summary>
        /// Count of the data items in the container.
        /// </summary>
        public virtual int Count => data.Count;
        
        /// <summary>
        /// Removes the data at the specified index.
        /// </summary>
        public virtual void RemoveAt(int index) => data.RemoveAt(index);
        
        public void Validate(SelfValidationResult result)
        {
            // Check if container cannot have any null entries
            // and remove them if found.
            if (GetType().GetCustomAttribute<NoNullEntriesAttribute>() != null)
            {
                for (int index = Count - 1; index >= 0; index--)
                {
                    TDataType element = this[index];
                    
                    // Check if element is null
                    // Use Unity's Object class to check if object is null
                    // as otherwise false-negative results can be returned.
                    if (element is UnityEngine.Object unityObject)
                    {
                        if (unityObject != null) continue;
                    }
                    else if (element != null) continue;
                    
                    RemoveAt(index);
                    
                    Debug.Log($"Null data found in the container {GetType().Name}." +
                              $" Data was automatically removed.");
                }
            }

            // Check if container cannot have any duplicate entries
            // and remove them if found.
            if(GetType().GetCustomAttribute<NoDuplicatesAttribute>() == null){
                for (int index = Count - 1; index >= 0; index--)
                {
                    // Get data and ensure it's not null
                    TDataType element = this[index];
                    if (element == null) continue;

                    for (int innerIndex = Count - 1; innerIndex > index; innerIndex--)
                    {
                        TDataType innerData = this[innerIndex];
                        if (innerData == null) continue;

                        // Check if data is the same, if so, add error
                        if (!ReferenceEquals(element, innerData)) continue;
                        RemoveAt(innerIndex);
                        
                        Debug.Log($"Duplicate data found in the container {GetType().Name}." +
                                  $" Data was automatically removed.");
                    }
                }
            }
            
            // All container values must be sealed classes
            // this is enforced to prevent any issues with data handling
            // and improve performance.
            if (GetType().GetCustomAttribute<OnlySealedElementsAttribute>() != null)
            {
                for (int index = Count - 1; index >= 0; index--)
                {
                    TDataType element = this[index];
                    if (element == null) continue;

                    if (element.GetType().IsSealed) continue;
                    result.AddError("Container values must be sealed classes.")
                        .WithButton("Information",
                            () =>
                            {
                                Debug.Log($"Add `sealed` keyword to class definition of {element.GetType().Name}.");
                            });
                }
            }

            // Container should be populated to ensure all data is loaded
            // and available for use. 
            if (this is IAutoPopulatedContainer {IsPopulated: false} autoPopulatedContainer)
            {
                result.AddWarning("Container is not populated.")
                    .WithFix("Populate the container.", async () => { await autoPopulatedContainer.Populate(); });
            }
        }
    }
}