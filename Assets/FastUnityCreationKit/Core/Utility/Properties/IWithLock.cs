using FastUnityCreationKit.Core.Utility.Properties.Data;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents an object that can be locked via a specific lock object,
    /// this can be for example a chest, door or equipment.
    /// <br/><br/>
    /// Using <see cref="IWithLock{TLock}"/> is preferred over <see cref="ILockable"/> because it allows
    /// to define a custom lock object that can be used to lock and unlock the object.
    /// </summary>
    public interface IWithLock<TLock>
        where TLock : class, ILock, new()
    {
        /// <summary>
        /// Lock object used to lock the object.
        /// </summary>
        public TLock LockRepresentation { get; protected set; }
        
        /// <summary>
        /// Checks if the object is locked.
        /// </summary>
        public bool IsLocked => LockRepresentation.IsLocked;
     
        /// <summary>
        /// Locks the object.
        /// </summary>
        public void Lock() => LockRepresentation.Lock();
        
        /// <summary>
        /// Unlocks the object.
        /// </summary>
        public void Unlock() => LockRepresentation.Unlock();
    }
}