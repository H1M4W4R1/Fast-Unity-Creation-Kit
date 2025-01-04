namespace FastUnityCreationKit.Core.Objects
{
    /// <summary>
    /// Represents a temporary object that should be destroyed after use.
    /// </summary>
    public interface ITemporaryObject
    {
        /// <summary>
        /// This method should be called when the object is no longer needed.
        /// </summary>
        bool ShouldBeDestroyed();
        
    }
}