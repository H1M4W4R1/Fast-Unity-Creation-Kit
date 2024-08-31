namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents an object that may be removed if specific conditions are met.
    /// </summary>
    public interface IConditionallyRemovable
    {
        /// <summary>
        /// Checks if the object should be removed.
        /// </summary>
        public bool IsRemovalConditionMet();

    }
}