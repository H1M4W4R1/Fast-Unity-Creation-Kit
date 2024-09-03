namespace FastUnityCreationKit.Core.Utility.Properties.Data
{
    /// <summary>
    /// Represents a lock that can be picked.
    /// </summary>
    public interface IPickableLock : ILock
    {
        /// <summary>
        /// Called when the lock is successfully picked,
        /// you need to implement the logic for this in your minigame.
        /// </summary>
        public void OnLockpickingSuccess();
        
        /// <summary>
        /// Called when the lockpicking attempt fails,
        /// you need to implement the logic for this in your minigame.
        /// </summary>
        public void OnLockpickingFailure();
    }
}