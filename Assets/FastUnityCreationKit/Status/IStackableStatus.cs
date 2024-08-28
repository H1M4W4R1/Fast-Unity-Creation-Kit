using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;

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
        /// Increases the stack count by the given amount.
        /// </summary>
        public void IncreaseStackCount(int amount = 1) => ChangeStackCount(amount);
        
        /// <summary>
        /// Decreases the stack count by the given amount.
        /// </summary>
        public void DecreaseStackCount(int amount = 1) => ChangeStackCount(-amount);
        
        /// <summary>
        /// Changes the stack count by the given amount.
        /// You need to call increase/decrease stack count methods instead of this method
        /// events should be manually added after calling method wrappers.
        /// </summary>
        private void ChangeStackCount(int amount)
        {
            StackCount += amount;
            CheckLimits();
        }

        /// <summary>
        /// Checks if the stack count is within the limits.
        /// </summary>
        private void CheckLimits()
        {
            // Check if the stack count is within the minimum limit.
            if (this is IWithMinLimit<int32> minLimit)
            {
                if (StackCount < minLimit.MinLimit)
                    StackCount = minLimit.MinLimit;
            }
            
            // Check if the stack count is within the maximum limit.
            if (this is IWithMaxLimit<int32> maxLimit)
            {
                if (StackCount > maxLimit.MaxLimit)
                    StackCount = maxLimit.MaxLimit;
            }
        }
    }
}