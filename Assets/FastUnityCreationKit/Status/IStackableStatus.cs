using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// Represents a status that can be stacked.
    /// Allows to stack multiple instances of the same status on an object.
    /// It can go either positive or negative way to support both buffs and debuffs
    /// using a single class.
    /// <br/><br/>
    /// Also supports limits <see cref="IWithMinLimit{T}"/> and <see cref="IWithMaxLimit{T}"/> with
    /// <see cref="int32"/> type used for stack count as this numeric type is used to contain
    /// the number of times a status is stacked.
    /// </summary>
    public interface IStackableStatus : IStatus
    {
        /// <summary>
        /// Number of times this status is stacked on an object.
        /// </summary>
        public int32 StackCount { get; protected set; }
        
        /// <summary>
        /// Called when the stack count is changes.
        /// </summary>
        public void OnStackCountChanged([NotNull] IObjectWithStatus objectWithStatus, int amount);
        
        /// <summary>
        /// Called when the stack count reaches the maximum limit.
        /// This is called each time the stack count is increased and reaches the maximum limit,
        /// even if it is already at the maximum limit.
        /// </summary>
        public void OnMaxStackCountReached([NotNull] IObjectWithStatus objectWithStatus);
        
        /// <summary>
        /// Called when the stack count reaches the minimum limit.
        /// This is called each time the stack count is decreased and reaches the minimum limit,
        /// even if it is already at the minimum limit.
        /// </summary>
        public void OnMinStackCountReached([NotNull] IObjectWithStatus objectWithStatus);

        /// <summary>
        /// Increases the stack count by the given amount.
        /// </summary>
        public void IncreaseStackCount([NotNull] IObjectWithStatus objectWithStatus, int amount = 1)
        {
            int currentStackCount = StackCount;
            ChangeStackCount(objectWithStatus, amount);
            int change = StackCount - currentStackCount;
            OnStackCountChanged(objectWithStatus, change);
        }

        /// <summary>
        /// Decreases the stack count by the given amount.
        /// </summary>
        public void DecreaseStackCount([NotNull] IObjectWithStatus objectWithStatus, int amount = 1)
        {
            int currentStackCount = StackCount;
            ChangeStackCount(objectWithStatus, -amount);
            int change = StackCount - currentStackCount;
            OnStackCountChanged(objectWithStatus, change);
        }

        /// <summary>
        /// Changes the stack count by the given amount.
        /// You need to call increase/decrease stack count methods instead of this method
        /// events should be manually added after calling method wrappers.
        /// </summary>
        private void ChangeStackCount([NotNull] IObjectWithStatus objectWithStatus, int amount)
        {
            StackCount += amount;
            CheckLimits(objectWithStatus);
        }

        /// <summary>
        /// Checks if the stack count is within the limits.
        /// </summary>
        private void CheckLimits([NotNull] IObjectWithStatus objectWithStatus)
        {
            // Check if the stack count is within the minimum limit.
            if (this is IWithMinLimit<int32> minLimit && StackCount < minLimit.MinLimit)
            {
                StackCount = minLimit.MinLimit;
                OnMinStackCountReached(objectWithStatus);
            }
            
            // Check if the stack count is within the maximum limit.
            if (this is IWithMaxLimit<int32> maxLimit && StackCount > maxLimit.MaxLimit)
            {
                StackCount = maxLimit.MaxLimit;
                OnMaxStackCountReached(objectWithStatus);
            }
        }
    }
}