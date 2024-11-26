using Cysharp.Threading.Tasks;

namespace FastUnityCreationKit.Data.Interfaces
{
    /// <summary>
    /// Represents a container that is auto-populated with data on first creation of instance.
    /// </summary>
    /// <remarks>
    /// This feature can be used to quickly get all the data in the container without having to manually populate it.
    /// Good example is a container of ScriptableObjects representing Inventory Items that are stored in specific
    /// Addressable Group. This way, the container can be populated with all the items in the group on first access
    /// so it is no longer required to remember to populate the container before using it.
    /// </remarks>
    public interface IAutoPopulatedContainer
    {
        /// <summary>
        /// Populates the container with data.
        /// </summary>
        public UniTask Populate();

        /// <summary>
        /// Used to ensure that the container is populated and ready to be used.
        /// </summary>
        public bool IsPopulated { get; }
    }
}