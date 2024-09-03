namespace FastUnityCreationKit.Core.Utility.Properties.Data
{
    /// <summary>
    /// Represents a lock that can be jammed.
    /// A jammed lock can't be unlocked.
    /// </summary>
    public interface IJammableLock : ILock
    {
        /// <summary>
        /// Checks if the lock is jammed.
        /// </summary>
        public bool IsJammed { get; protected set; }

        /// <summary>
        /// Jam the lock - you can call this e.g. on percentage basis if your
        /// player tries to unlock the lock via a lock-picking minigame.
        /// </summary>
        public void Jam()
        {
            // Check if not already jammed
            if(IsJammed) return;
            
            IsJammed = true;
            OnLockJammed();
        }

        /// <summary>
        /// Unjam the lock - this can be called when e.g. vendor is used to repair the lock.
        /// </summary>
        public void Unjam()
        {
            // Check if not already unjammed
            if(!IsJammed) return;
            
            IsJammed = false;
            OnLockUnjammed();
        }
        
        /// <summary>
        /// Called when the lock is jammed.
        /// </summary>
        public void OnLockJammed();
        
        /// <summary>
        /// Called when the lock is unjammed.
        /// </summary>
        public void OnLockUnjammed();
    }
}