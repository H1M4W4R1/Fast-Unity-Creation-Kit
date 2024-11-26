using JetBrains.Annotations;

namespace FastUnityCreationKit.Data.Interfaces
{
    /// <summary>
    /// Represents a data container that is used to store data of a specific type.
    /// </summary>
    public interface IDataContainer<TDataType> : IIndexableBy<TDataType, int>
    {
        /// <summary>
        /// Adds the data to the container.
        /// </summary>
        public void Add([NotNull] TDataType data);
        
        /// <summary>
        /// Removes the data from the container.
        /// </summary>
        public void Remove([NotNull] TDataType data);
        
        /// <summary>
        /// Clears all data from the container.
        /// </summary>
        public void Clear();
        
        /// <summary>
        /// Checks if the container contains the data.
        /// </summary>
        public bool Contains([NotNull] TDataType data);
        
        /// <summary>
        /// Count of the data items in the container.
        /// </summary>
        public int Count { get; }
        
        
    }
}