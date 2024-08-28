using JetBrains.Annotations;

namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// Represents a status that can be applied to an object.
    /// This can be used to represent any status that can be applied to an object - like a buff, debuff, etc.
    /// </summary>
    public interface IStatus
    {
        /// <summary>
        /// Called when the status is added to an object.
        /// </summary>
        public void OnStatusAdded([NotNull] IObjectWithStatus objectWithStatus);
        
        /// <summary>
        /// Called when the status is removed from an object.
        /// </summary>
        public void OnStatusRemoved([NotNull] IObjectWithStatus objectWithStatus);
        
    }
}