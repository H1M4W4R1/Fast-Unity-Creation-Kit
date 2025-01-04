namespace FastUnityCreationKit.Core.Objects
{
    /// <summary>
    /// Represents a temporary object that should be destroyed after use.
    /// </summary>
    public interface ITemporaryObject
    {
        /// <summary>
        /// Gets a value indicating whether this object has expired.
        /// </summary>
        bool HasExpired { get; }
    }
}