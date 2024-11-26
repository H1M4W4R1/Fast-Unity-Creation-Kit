using JetBrains.Annotations;

namespace FastUnityCreationKit.Data.Interfaces
{
    /// <summary>
    /// Represents a data container that is used to store data of a specific type.
    /// </summary>
    public interface IIndexedDataContainer<in TIndexType, TDataType> : IIndexableBy<TDataType, TIndexType>
    {
        /// <summary>
        /// Adds the data to the container.
        /// </summary>
        public void Add([NotNull] TIndexType index, [NotNull] TDataType data);
        
        /// <summary>
        /// Removes the data from the container.
        /// </summary>
        public void Remove([NotNull] TIndexType index);
        
        /// <summary>
        /// Clears all data from the container.
        /// </summary>
        public void Clear();
        
        /// <summary>
        /// Checks if the container contains the data.
        /// </summary>
        public bool ContainsValue([NotNull] TDataType data);
        
        /// <summary>
        /// Checks if the container contains the index.
        /// </summary>
        public bool ContainsIndex([NotNull] TIndexType index);
        
        /// <summary>
        /// Count of the data items in the container.
        /// </summary>
        public int Count { get; }
        
        
    }
}