using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Core.Limits;
using FastUnityCreationKit.Identification.Identifiers;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.Economy
{
    /// <summary>
    ///     This is default implementation of a resource container.
    /// </summary>
    /// <remarks>
    ///     Methods can be overriden to implement custom logic like health resource can't be taken
    ///     if entity is invulnerable or can't be added if entity is dead.
    /// </remarks>
    [Serializable] public abstract class ResourceContainerBase
    {
        /// <summary>
        ///     Resource identifier. This is used to get the resource from the database.
        /// </summary>
        [ShowInInspector] [TitleGroup(GROUP_INFO, order: int.MinValue)] [ReadOnly]
        [field: SerializeField, HideInInspector]
        public Snowflake128 Identifier { get; protected set; }

        /// <summary>
        ///     Resource that is stored in this container.
        /// </summary>
        [CanBeNull] [ShowInInspector] [TitleGroup(GROUP_INFO)] [ReadOnly]
        public ResourceBase Resource => ResourceDatabase.Instance.GetResource(Identifier);
        
        /// <summary>
        ///     Amount of resource stored currently in the container.
        ///     Does not take any limits into account.
        /// </summary>
        [ShowInInspector]
        [TitleGroup(GROUP_INFO)]
        [OnValueChanged(nameof(CheckLimitsWithoutEvents))]
        [field: SerializeField, HideInInspector]
        public int Amount { get; internal set; }

        public int ContainerMaxLimit =>
            (int) (this is IWithMaxLimit maxLimitContainer ? maxLimitContainer.MaxLimit : int.MaxValue);

        public int ContainerMinLimit =>
            (int) (this is IWithMinLimit minLimitContainer ? minLimitContainer.MinLimit : 0);

        public int ResourceMaxLimit =>
            (int) (Resource is IWithMaxLimit maxLimitResource ? maxLimitResource.MaxLimit : int.MaxValue);

        public int ResourceMinLimit =>
            (int) (Resource is IWithMinLimit minLimitResource ? minLimitResource.MinLimit : 0);

        /// <summary>
        /// Checks if the container or resource has a maximum limit.
        /// </summary>
        public bool HasMaxLimit => MaximumResourceStored < int.MaxValue;

        /// <summary>
        /// Checks if the container or resource has a minimum limit.
        /// </summary>
        public bool HasMinLimit => MinimumResourceStored != 0;

        /// <summary>
        /// Maximum amount of resource that can be stored in this container.
        /// </summary>
        [ShowInInspector] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] [ReadOnly] [ShowIf(nameof(HasMaxLimit))]
        public int MaximumResourceStored => math.min(ResourceMaxLimit, ContainerMaxLimit);

        /// <summary>
        /// Minimum amount of resource that can be stored in this container.
        /// </summary>
        [ShowInInspector] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] [ReadOnly] [ShowIf(nameof(HasMinLimit))]
        public int MinimumResourceStored
        {
            get
            {
                // We need to custom-handle minimum limit as with default value set to 0
                // math.max would cause issues with negative values.
                int limitedMinResource = (Resource is IWithMinLimit) ? ResourceMinLimit : int.MinValue;
                int limitedMinContainer = (this is IWithMinLimit) ? ContainerMinLimit : int.MinValue;

                return math.max(limitedMinResource, limitedMinContainer);
            }
        }

        /// <summary>
        ///     Space left in this container that takes into account both resource and container limits.
        /// </summary>
        [HideInInspector]
        [TitleGroup(GROUP_INFO)]
        [ReadOnly]
        [ShowIf(nameof(HasMaxLimit))]
        [ProgressBar(min: 0, maxGetter: nameof(ResourceSpace))]
        public int SpaceLeft => MaximumResourceStored - Amount;

        /// <summary>
        ///     Amount of resource left in this container that takes into account both resource and container limits.
        /// </summary>
        [ShowInInspector]
        [TitleGroup(GROUP_INFO)]
        [ReadOnly]
        [ProgressBar(min: 0, maxGetter: nameof(ResourceSpace))]
        public int AmountLeft => Amount - MinimumResourceStored;

        /// <summary>
        ///     Checks if the container is empty (there is no resource).
        /// </summary>
        [ShowInInspector] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] [ReadOnly] public bool IsEmpty
            => Amount == MinimumResourceStored;

        /// <summary>
        ///     Checks if the container is full (is at maximum limit).
        /// </summary>
        [ShowInInspector] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] [ReadOnly] public bool IsFull
            => SpaceLeft == 0;

        /// <summary>
        /// Total space in this container that can be used for resource.
        /// </summary>
        [HideInInspector] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] [ReadOnly] public int ResourceSpace
            => MaximumResourceStored - MinimumResourceStored;

        protected virtual async UniTask OnResourceAddedAsync(int amount)
        {
            await UniTask.CompletedTask;
        }

        protected virtual async UniTask OnResourceRemovedAsync(int amount)
        {
            await UniTask.CompletedTask;
        }

        protected virtual async UniTask OnResourceAddFailedAsync(int amount, int spaceLeft)
        {
            await UniTask.CompletedTask;
        }

        protected virtual async UniTask OnResourceRemoveFailedAsync(int amount, int amountLeft)
        {
            await UniTask.CompletedTask;
        }

        protected virtual async UniTask OnResourceAmountChangedAsync(int oldAmount, int newAmount)
        {
            await UniTask.CompletedTask;
        }

        /// <summary>
        ///     Tries to add the specified amount of resource to the container.
        /// </summary>
        /// <param name="amount">Amount of resource to add.</param>
        /// <param name="force">Force add resource even if it exceeds the limit.</param>
        /// <returns>Amount of resource that was NOT added.</returns>
        public virtual async UniTask<int> Add(int amount, bool force = true)
        {
            int startAmount = Amount;
            int expectedAmount = Amount + amount;

            // Check if amount exceeds max limit
            if (!HasSpaceFor(amount) && !force)
            {
                if (ReferenceEquals(Resource, null)) return amount;

                await Resource.OnResourceAddFailedAsync(this, amount, SpaceLeft);
                await OnResourceAddFailedAsync(amount, SpaceLeft);
                return amount;
            }

            // Add amount to container
            Amount += amount;

            // Ensure limits for this container (in case resource is limited and force is true)
            await CheckLimitsAndRaiseEvents();

            // Get added amount and call events
            int addedAmount = Amount - startAmount;

            // Prevent null reference exception
            if (ReferenceEquals(Resource, null)) return 0;

            await Resource.OnResourceAddedAsync(this, addedAmount);
            await OnResourceAddedAsync(addedAmount);
            await Resource.OnResourceChangedAsync(this, startAmount, Amount);
            await OnResourceAmountChangedAsync(startAmount, Amount);

            // Check if amount is equal to expected amount
            return Amount == expectedAmount ? 0 : expectedAmount - Amount;
        }

        /// <summary>
        ///     Tries to take the specified amount of resource from the container.
        /// </summary>
        /// <param name="amount">Amount of resource to take.</param>
        /// <returns>True if the resource was taken, otherwise false.</returns>
        public virtual async UniTask<bool> TryTake(int amount)
        {
            int amountLeft = await Take(amount, false);
            return amountLeft == 0;
        }

        /// <summary>
        ///     Tries to take the specified amount of resource from the container.
        /// </summary>
        /// <param name="amount">Amount of resource to take.</param>
        /// <param name="force">Force take resource even if it exceeds the limit.</param>
        /// <returns>Amount of resource that was NOT taken.</returns>
        public virtual async UniTask<int> Take(int amount, bool force = true)
        {
            int startAmount = Amount;
            int expectedAmount = Amount - amount;

            // Check if amount is less than min limit, if so, return false when force is false
            if (!HasEnough(amount) && !force)
            {
                if (ReferenceEquals(Resource, null)) return amount;
                await Resource.OnResourceRemoveFailedAsync(this, amount, AmountLeft);
                await OnResourceRemoveFailedAsync(amount, AmountLeft);
                return amount;
            }

            // Take amount from container
            Amount -= amount;

            // Ensure limits for this container (in case resource is limited and force is true)
            await CheckLimitsAndRaiseEvents();

            // Get removed amount and call events
            int removedAmount = startAmount - Amount;

            // Prevent null reference exception
            if (ReferenceEquals(Resource, null)) return 0;

            await Resource.OnResourceRemovedAsync(this, removedAmount);
            await OnResourceRemovedAsync(removedAmount);
            await Resource.OnResourceChangedAsync(this, startAmount, Amount);
            await OnResourceAmountChangedAsync(startAmount, Amount);

            // Check if amount is equal to expected amount
            // expectedAmount will be always lower than Amount, so
            // we invert subtraction to get positive value
            return Amount == expectedAmount ? 0 : expectedAmount - Amount;
        }

        /// <summary>
        ///     Sets the amount of resource in the container.
        /// </summary>
        /// <param name="amount">Amount of resource to set.</param>
        public virtual async UniTask SetAmount(int amount)
        {
            int oldAmount = Amount;
            Amount = amount;
            await CheckLimitsAndRaiseEvents();

            // Prevent null reference exception
            if (ReferenceEquals(Resource, null)) return;

            // Call events
            await Resource.OnResourceChangedAsync(this, oldAmount, Amount);
            await OnResourceAmountChangedAsync(oldAmount, Amount);
        }

        /// <summary>
        ///     Sets the container to be full (maximum limit).
        /// </summary>
        public async UniTask SetFull()
        {
            await SetAmount(math.min(ResourceMaxLimit, ContainerMaxLimit));
        }

        /// <summary>
        ///     Sets the container to be empty (minimum limit).
        /// </summary>
        public async UniTask SetEmpty()
        {
            await SetAmount(math.max(ResourceMinLimit, ContainerMinLimit));
        }

        /// <summary>
        ///     Checks if the container has the specified amount of resource.
        /// </summary>
        /// <param name="amount">Amount of resource to check.</param>
        /// <returns>True if the container has the resource, otherwise false.</returns>
        /// <remarks>Does not take limits into account!</remarks>
        public virtual bool Has(int amount)
        {
            return Amount >= amount;
        }

        /// <summary>
        ///     Checks if the container has enough resource to take the specified amount.
        /// </summary>
        /// <param name="amount">Amount of resource to check.</param>
        /// <returns>True if the container has enough resource, otherwise false.</returns>
        /// <remarks>Takes limits into account!</remarks>
        public virtual bool HasEnough(int amount)
        {
            return AmountLeft >= amount;
        }

        /// <summary>
        ///     Checks if the container can store the specified amount.
        ///     Do not mistake with <see cref="HasSpaceFor" /> as this method does not take
        ///     resource amount into account (it only takes container limits into account).
        /// </summary>
        /// <param name="amount">Amount of resource to check.</param>
        /// <returns>True if the container can store the resource, otherwise false.</returns>
        public virtual bool CanStore(int amount)
        {
            // Get max limit for resource
            int maxLimit = ResourceMaxLimit < ContainerMaxLimit ? ResourceMaxLimit : ContainerMaxLimit;
            return amount <= maxLimit;
        }

        /// <summary>
        ///     Checks if this container has space for the specified amount of resource.
        ///     This current resource amount into account.
        /// </summary>
        /// <param name="amount">Amount of resource to check.</param>
        /// <returns>True if the container has space for the resource, otherwise false.</returns>
        public virtual bool HasSpaceFor(int amount)
        {
            return SpaceLeft >= amount;
        }

        protected UniTask OnMaxLimitReachedAsync()
        {
            return UniTask.CompletedTask;
        }

        protected UniTask OnMinLimitReachedAsync()
        {
            return UniTask.CompletedTask;
        }

        private void CheckLimitsWithoutEvents() => _ = CheckLimitsAndRaiseEvents(false);

        protected virtual async UniTask CheckLimitsAndRaiseEvents(bool withEvents = true)
        {
            // Check if resource is null
            if (ReferenceEquals(Resource, null)) return;

            // Check if resource is at max limit
            if (Amount > MaximumResourceStored)
            {
                Amount = MaximumResourceStored;
                if (withEvents)
                {
                    await Resource.OnMaxLimitReachedAsync(this);
                    await OnMaxLimitReachedAsync();
                }
            }

            // Check if resource is at min limit
            else if (Amount < MinimumResourceStored)
            {
                Amount = MinimumResourceStored;
                if (withEvents)
                {
                    await Resource.OnMinLimitReachedAsync(this);
                    await OnMinLimitReachedAsync();
                }
            }
        }

        protected ResourceContainerBase(Snowflake128 resourceIdentifier)
        {
            Identifier = resourceIdentifier;
            Amount = (int) (this is IWithDefaultAmount withDefaultAmount
                ? withDefaultAmount.DefaultAmount
                : MinimumResourceStored);
        }
    }
}