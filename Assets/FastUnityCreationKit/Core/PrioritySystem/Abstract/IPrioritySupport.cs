using System;

namespace FastUnityCreationKit.Core.PrioritySystem.Abstract
{
    /// <summary>
    /// Represents an object that supports priority.
    /// By default, all priority objects are comparable to each other.
    /// Priority 0 is always the highest priority.
    /// Equal priorities result in undefined sorting order between the objects as it
    /// cannot be guaranteed that the objects will be sorted in the same order every time.
    /// </summary>
    public interface IPrioritySupport : IComparable<IPrioritySupport>
    {
        /// <summary>
        /// Priority of the object.
        /// </summary>
        public uint Priority { get; }

    }
}