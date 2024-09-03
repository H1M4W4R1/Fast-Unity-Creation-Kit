namespace FastUnityCreationKit.Core.Utility.Properties.Data
{
    /// <summary>
    /// Represents a lock object - an object that can be locked
    /// </summary>
    public interface ILock : ILockable
    {
        /// <summary>
        /// Checks if the object can be lockpicked via a pick.
        /// You need to check if player has lockpicking tools separately.
        /// This only indicates if the object can be lockpicked.
        /// </summary>
        public bool CanBeLockpicked => this is IPickableLock;
        
        /// <summary>
        /// Checks if the lock can be jammed.
        /// </summary>
        public bool CanBeJammed => this is IJammableLock;
        
        /// <summary>
        /// Called when the object is locked.
        /// </summary>
        public void OnLocked();
        
        /// <summary>
        /// Called when the object is unlocked.
        /// </summary>
        public void OnUnlocked();

        /// <summary>
        /// Locks the object.
        /// </summary>
        void ILockable.Lock()
        {
            // Check if the lock is not already locked
            if(IsLocked) return;
            
            // Check if the lock is jammed
            if(this is IJammableLock {IsJammed: true}) return;
            
            // Lock the object
            IsLocked = true;
            OnLocked();
        }
        
        /// <summary>
        /// Unlocks the object.
        /// </summary>
        void ILockable.Unlock()
        {
            // Check if the lock is not already unlocked
            if(!IsLocked) return;
            
            // Check if the lock is jammed
            if(this is IJammableLock {IsJammed: true})
                return;
            
            // Unlock the object
            IsLocked = false;
            OnUnlocked();
        }
    }
}