using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;
using FastUnityCreationKit.Status.Enums;
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
        /// Type of notification when the maximum stack count is reached.
        /// See <see cref="MaxStackLimitReachedNotificationMode"/> for more information.
        /// </summary>
        public MaxStackLimitReachedNotificationMode MaxStackLimitReachedNotificationMode => MaxStackLimitReachedNotificationMode.Once;
        
        /// <summary>
        /// Type of notification when the minimum stack count is reached.
        /// See <see cref="MinStackLimitReachedNotificationMode"/> for more information.
        /// </summary>
        public MinStackLimitReachedNotificationMode MinStackLimitReachedNotificationMode => MinStackLimitReachedNotificationMode.Once;
        
        /// <summary>
        /// Called when the stack count is changes.
        /// </summary>
        public UniTask OnStackCountChangedAsync([NotNull] IObjectWithStatus objectWithStatus, int amount);
        
        /// <summary>
        /// Called when the stack count reaches the maximum limit.
        /// This is called each time the stack count is increased and reaches the maximum limit,
        /// even if it is already at the maximum limit.
        /// </summary>
        public UniTask OnMaxStackCountReachedAsync([NotNull] IObjectWithStatus objectWithStatus);
        
        /// <summary>
        /// Called when the stack count reaches the minimum limit.
        /// This is called each time the stack count is decreased and reaches the minimum limit,
        /// even if it is already at the minimum limit.
        /// </summary>
        public UniTask OnMinStackCountReachedAsync([NotNull] IObjectWithStatus objectWithStatus);

        /// <summary>
        /// Increases the stack count by the given amount.
        /// </summary>
        public void IncreaseStackCount([NotNull] IObjectWithStatus objectWithStatus, int amount = 1) =>
            IncreaseStackCountAsync(objectWithStatus, amount).GetAwaiter().GetResult();
        
        /// <summary>
        /// Increases the stack count by the given amount.
        /// </summary>
        public async UniTask IncreaseStackCountAsync([NotNull] IObjectWithStatus objectWithStatus, int amount = 1)
        {
            int currentStackCount = StackCount;
            
            for (int times = 0; times < amount; times++)
                await ChangeStackCountAsync(objectWithStatus, 1);
            
            int change = StackCount - currentStackCount;
            await OnStackCountChangedAsync(objectWithStatus, change);
        }

        /// <summary>
        /// Decreases the stack count by the given amount.
        /// </summary>
        public void DecreaseStackCount([NotNull] IObjectWithStatus objectWithStatus, int amount = 1) =>
            DecreaseStackCountAsync(objectWithStatus, amount).GetAwaiter().GetResult();
        
        /// <summary>
        /// Decreases the stack count by the given amount.
        /// </summary>
        public async UniTask DecreaseStackCountAsync([NotNull] IObjectWithStatus objectWithStatus, int amount = 1)
        {
            int currentStackCount = StackCount;
            for (int times = 0; times < amount; times++)
                await ChangeStackCountAsync(objectWithStatus, -1);
            
            int change = StackCount - currentStackCount;
            await OnStackCountChangedAsync(objectWithStatus, change);
        }

        /// <summary>
        /// Changes the stack count by the given amount.
        /// You need to call increase/decrease stack count methods instead of this method
        /// events should be manually added after calling method wrappers.
        /// </summary>
        private async UniTask ChangeStackCountAsync([NotNull] IObjectWithStatus objectWithStatus, int amount)
        {
            // Acquire the previous stack count before changing it.
            int previousStackCount = StackCount;
            
            // Change the stack count.
            StackCount += amount;
            
            // Check if the stack count is within the limits.
            await CheckLimitsAsync(objectWithStatus, previousStackCount);
        }

        /// <summary>
        /// Checks if the stack count is within the limits.
        /// </summary>
        private async UniTask CheckLimitsAsync([NotNull] IObjectWithStatus objectWithStatus, int previousStackCount)
        {
            // Check if the stack count is within the minimum limit.
            if (this is IWithMinLimit<int32> minLimit && StackCount <= minLimit.MinLimit)
            {
                StackCount = minLimit.MinLimit;
                
                // Check if the stack count is changed.
                // This is to prevent calling the event multiple times when the stack count is already at the minimum limit.
                if(previousStackCount != StackCount || MaxStackLimitReachedNotificationMode != MaxStackLimitReachedNotificationMode.Once)
                    await OnMinStackCountReachedAsync(objectWithStatus);
            }
            
            // Check if the stack count is within the maximum limit.
            if (this is IWithMaxLimit<int32> maxLimit && StackCount >= maxLimit.MaxLimit)
            {
                StackCount = maxLimit.MaxLimit;
                
                // Check if the stack count is changed.
                // This is to prevent calling the event multiple times when the stack count is already at the maximum limit.
                if(previousStackCount != StackCount || MaxStackLimitReachedNotificationMode != MaxStackLimitReachedNotificationMode.Once)
                    await OnMaxStackCountReachedAsync(objectWithStatus);
            }
        }
    }
}