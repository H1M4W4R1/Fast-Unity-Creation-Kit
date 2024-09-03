namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents an object that can be locked.
    /// </summary>
    public interface ILockable
    {
        /// <summary>
        /// Checks if the object is locked.
        /// </summary>
        public bool IsLocked { get; protected set; }
        
        /// <summary>
        /// Locks the object.
        /// </summary>
        public void Lock() => IsLocked = true;
        
        /// <summary>
        /// Unlocks the object.
        /// </summary>
        public void Unlock() => IsLocked = false;
    }
}