using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Info;
using FastUnityCreationKit.Identification.Identifiers;
using FastUnityCreationKit.Utility.Limits;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Unity.Mathematics;

namespace FastUnityCreationKit.Economy
{
    /// <summary>
    /// This is default implementation of a resource container. If any custom logic is needed,
    /// this class should be inherited and custom logic should be implemented.
    /// </summary>
    /// <remarks>
    /// Methods can be overriden to implement custom logic like health resource can't be taken
    /// if entity is invulnerable or can't be added if entity is dead.
    /// </remarks>
    [SupportedFeature(typeof(ILimited))]
    public class ResourceReference
    {
        /// <summary>
        /// Resource identifier.
        /// </summary>
        [OdinSerialize]
        [ShowInInspector]
        [TabGroup("Debug")]
        [ReadOnly]
        public Snowflake128 Identifier { get; private set; }

        /// <summary>
        /// Amount of resource stored currently in the container.
        /// Does not take any limits into account.
        /// </summary>
        [OdinSerialize]
        [ShowInInspector]
        [TabGroup("Debug")]
        [ReadOnly]
        public long Amount { get; internal set; }

        public ResourceBase Resource => ResourceDatabase.Instance.GetResource(Identifier);

        public long ContainerMaxLimit =>
            (long) (this is IWithMaxLimit maxLimitContainer ? maxLimitContainer.MaxLimit : long.MaxValue);

        public long ContainerMinLimit =>
            (long) (this is IWithMinLimit minLimitContainer ? minLimitContainer.MinLimit : 0);

        public long ResourceMaxLimit =>
            (long) (Resource is IWithMaxLimit maxLimitResource ? maxLimitResource.MaxLimit : long.MaxValue);

        public long ResourceMinLimit =>
            (long) (Resource is IWithMinLimit minLimitResource ? minLimitResource.MinLimit : 0);

        protected virtual async UniTask OnResourceAddedAsync(long amount) => await UniTask.CompletedTask;
        protected virtual async UniTask OnResourceRemovedAsync(long amount) => await UniTask.CompletedTask;
        protected virtual async UniTask OnResourceAddFailedAsync(long amount, long spaceLeft) => await UniTask.CompletedTask;
        protected virtual async UniTask OnResourceRemoveFailedAsync(long amount, long amountLeft) => await UniTask.CompletedTask;
        protected virtual async UniTask OnResourceAmountChangedAsync(long oldAmount, long newAmount) => await UniTask.CompletedTask;
        
        /// <summary>
        /// Space left in this container that takes into account both resource and container limits.
        /// </summary>
        public long SpaceLeft => math.min(ResourceMaxLimit, ContainerMaxLimit) - Amount;

        /// <summary>
        /// Amount of resource left in this container that takes into account both resource and container limits.
        /// </summary>
        public long AmountLeft => Amount - math.max(ResourceMinLimit, ContainerMinLimit);

        /// <summary>
        /// Checks if the container is empty (there is no resource).
        /// </summary>
        public bool IsEmpty => Amount == 0;

        /// <summary>
        /// Checks if the container has no resource left (container is at minimum limit)
        /// </summary>
        public bool HasAvailableResource => AmountLeft == 0;
        
        /// <summary>
        /// Checks if the container is full (is at maximum limit).
        /// </summary>
        public bool IsFull => SpaceLeft == 0;
        
        /// <summary>
        /// Tries to add the specified amount of resource to the container.
        /// </summary>
        /// <param name="amount">Amount of resource to add.</param>
        /// <param name="force">Force add resource even if it exceeds the limit.</param>
        /// <returns>Amount of resource that was NOT added.</returns>
        public virtual async UniTask<long> Add(long amount, bool force = true)
        {
            long startAmount = Amount;
            long expectedAmount = Amount + amount;

            // Check if amount exceeds max limit
            if (!HasSpaceFor(amount) && !force)
            {
                await Resource.OnResourceAddFailedAsync(this, amount, SpaceLeft);
                await OnResourceAddFailedAsync(amount, SpaceLeft);
                return amount;
            }

            // Add amount to container
            Amount += amount;

            // Ensure limits for this container (in case resource is limited and force is true)
            await CheckLimitsAndRaiseEvents();

            // Get added amount and call events
            long addedAmount = Amount - startAmount;
            await Resource.OnResourceAddedAsync(this, addedAmount);
            await OnResourceAddedAsync(addedAmount);
            await Resource.OnResourceChangedAsync(this, startAmount, Amount);
            await OnResourceAmountChangedAsync(startAmount, Amount);
            
            // Check if amount is equal to expected amount
            return Amount == expectedAmount ? 0L : expectedAmount - Amount;
        }

        /// <summary>
        /// Tries to take the specified amount of resource from the container.
        /// </summary>
        /// <param name="amount">Amount of resource to take.</param>
        /// <returns>True if the resource was taken, otherwise false.</returns>
        public virtual async UniTask<bool> TryTake(long amount)
        {
            long amountLeft = await Take(amount, false);
            return amountLeft == 0;
        }

        /// <summary>
        /// Tries to take the specified amount of resource from the container.
        /// </summary>
        /// <param name="amount">Amount of resource to take.</param>
        /// <param name="force">Force take resource even if it exceeds the limit.</param>
        /// <returns>Amount of resource that was NOT taken.</returns>
        public virtual async UniTask<long> Take(long amount, bool force = true)
        {
            long startAmount = Amount;
            long expectedAmount = Amount - amount;

            // Check if amount is less than min limit, if so, return false when force is false
            if (!HasEnough(amount) && !force)
            {
                await Resource.OnResourceRemoveFailedAsync(this, amount, AmountLeft);
                await OnResourceRemoveFailedAsync(amount, AmountLeft);
                return amount;
            }

            // Take amount from container
            Amount -= amount;

            // Ensure limits for this container (in case resource is limited and force is true)
            await CheckLimitsAndRaiseEvents();

            // Get removed amount and call events
            long removedAmount = startAmount - Amount;
            await Resource.OnResourceRemovedAsync(this, removedAmount);
            await OnResourceRemovedAsync(removedAmount);
            await Resource.OnResourceChangedAsync(this, startAmount, Amount);
            await OnResourceAmountChangedAsync(startAmount, Amount);

            // Check if amount is equal to expected amount
            // expectedAmount will be always lower than Amount, so
            // we invert subtraction to get positive value
            return Amount == expectedAmount ? 0L : expectedAmount - Amount;
        }

        /// <summary>
        /// Sets the amount of resource in the container.
        /// </summary>
        /// <param name="amount">Amount of resource to set.</param>
        public virtual async UniTask SetAmount(long amount)
        {
            long oldAmount = Amount;
            Amount = amount;
            await CheckLimitsAndRaiseEvents();
            
            // Call events
            await Resource.OnResourceChangedAsync(this, oldAmount, Amount);
            await OnResourceAmountChangedAsync(oldAmount, Amount);
        }
        
        /// <summary>
        /// Sets the container to be full (maximum limit).
        /// </summary>
        public async UniTask SetFull() => await SetAmount(math.min(ResourceMaxLimit, ContainerMaxLimit));
        
        /// <summary>
        /// Sets the container to be empty (minimum limit).
        /// </summary>
        public async UniTask SetEmpty() => await SetAmount(math.max(ResourceMinLimit, ContainerMinLimit));

        /// <summary>
        /// Checks if the container has the specified amount of resource.
        /// </summary>
        /// <param name="amount">Amount of resource to check.</param>
        /// <returns>True if the container has the resource, otherwise false.</returns>
        /// <remarks>Does not take limits into account!</remarks>
        public virtual bool Has(long amount) => Amount >= amount;

        /// <summary>
        /// Checks if the container has enough resource to take the specified amount.
        /// </summary>
        /// <param name="amount">Amount of resource to check.</param>
        /// <returns>True if the container has enough resource, otherwise false.</returns>
        /// <remarks>Takes limits into account!</remarks>
        public virtual bool HasEnough(long amount) => AmountLeft >= amount;

        /// <summary>
        /// Checks if the container can store the specified amount.
        /// Do not mistake with <see cref="HasSpaceFor"/> as this method does not take
        /// resource amount into account (it only takes container limits into account).
        /// </summary>
        /// <param name="amount">Amount of resource to check.</param>
        /// <returns>True if the container can store the resource, otherwise false.</returns>
        public virtual bool CanStore(long amount)
        {
            // Get max limit for resource
            long maxLimit = ResourceMaxLimit < ContainerMaxLimit ? ResourceMaxLimit : ContainerMaxLimit;
            return amount <= maxLimit;
        }

        /// <summary>
        /// Checks if this container has space for the specified amount of resource.
        /// This current resource amount into account.
        /// </summary>
        /// <param name="amount">Amount of resource to check.</param>
        /// <returns>True if the container has space for the resource, otherwise false.</returns>
        public virtual bool HasSpaceFor(long amount) => SpaceLeft >= amount;

        protected UniTask OnMaxLimitReachedAsync() => UniTask.CompletedTask;
        protected UniTask OnMinLimitReachedAsync() => UniTask.CompletedTask;

        protected virtual async UniTask CheckLimitsAndRaiseEvents()
        {
            // Acquire limit information from referenced status
            // Beware that this only ensures resource limits not container limits to prevent 
            // events being raised with container limits.
            LimitHit limitHit = Resource.CheckLimitsFor(this);
            switch (limitHit)
            {
                // Check if max limit is reached
                case LimitHit.UpperLimitHit:
                    await Resource.OnMaxLimitReached(this);
                    break;
                case LimitHit.LowerLimitHit:
                    await Resource.OnMinLimitReached(this);
                    break;
                case LimitHit.None:
                    break;
                default:
                    throw new NotSupportedException();
            }

            // Check container limits and call container events
            if (Amount > ContainerMaxLimit)
            {
                Amount = ContainerMaxLimit;
                await OnMaxLimitReachedAsync();
            }
            else if (Amount < ContainerMinLimit)
            {
                Amount = ContainerMinLimit;
                await OnMinLimitReachedAsync();
            }

            // Ensure limits for resource again and update
            // container amount without raising resource events
            // as those were already raised.
            if (Amount > ResourceMaxLimit)
                Amount = ResourceMaxLimit;
            else if (Amount < ResourceMinLimit)
                Amount = ResourceMinLimit;
        }
    }
}