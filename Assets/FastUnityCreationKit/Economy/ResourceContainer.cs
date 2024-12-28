using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Identification.Identifiers;
using FastUnityCreationKit.Utility.Limits;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace FastUnityCreationKit.Economy
{
    /// <summary>
    /// This is default implementation of a resource container. If any custom logic is needed,
    /// this class should be inherited and custom logic should be implemented.
    /// </summary>
    public class ResourceContainer
    {
        /// <summary>
        /// Resource identifier.
        /// </summary>
        [OdinSerialize] [ShowInInspector] [TabGroup("Debug")] [ReadOnly]
        public Snowflake128 Identifier { get; private set; }
        
        [OdinSerialize] [ShowInInspector] [TabGroup("Debug")] [ReadOnly]
        public long Amount { get; internal set; }

        public ResourceBase Resource => ResourceDatabase.Instance.GetResource(Identifier);
        
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
            if(!HasSpaceFor(amount) && !force)
            {
                await Resource.OnResourceAddFailedAsync(this, amount, Amount);
                return amount;
            }
            
            // Add amount to container
            Amount += amount;

            // Ensure limits for this container (in case resource is limited and force is true)
            await CheckLimitsAndRaiseEvents();
           
            // Get added amount and call event
            long addedAmount = Amount - startAmount;
            await Resource.OnResourceAddedAsync(this, addedAmount);
                
            // Check if amount is equal to expected amount
            return Amount == expectedAmount ? 0L :
                expectedAmount - Amount;
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
            if(!HasEnough(amount) && !force)
            {
                await Resource.OnResourceRemoveFailedAsync(this, amount, Amount);
                return amount;
            }
            
            // Take amount from container
            Amount -= amount;
            
            // Ensure limits for this container (in case resource is limited and force is true)
            await CheckLimitsAndRaiseEvents();
            
            // Get removed amount and call event
            long removedAmount = startAmount - Amount;
            await Resource.OnResourceRemovedAsync(this, removedAmount);
            
            // Check if amount is equal to expected amount
            // expectedAmount will be always lower than Amount, so
            // we invert subtraction to get positive value
            return Amount == expectedAmount ? 0L :
                expectedAmount - Amount;
        }
        
        /// <summary>
        /// Sets the amount of resource in the container.
        /// </summary>
        /// <param name="amount">Amount of resource to set.</param>
        public virtual async UniTask SetAmount(long amount)
        {
            Amount = amount;
            await CheckLimitsAndRaiseEvents();
        }

        /// <summary>
        /// Checks if the container has enough resource to take the specified amount.
        /// </summary>
        /// <param name="amount">Amount of resource to check.</param>
        /// <returns>True if the container has enough resource, otherwise false.</returns>
        /// <remarks>Takes limits into account!</remarks>
        public virtual bool HasEnough(long amount)
        {
            // Get min limit for resource
            long minLimit = (long)(Resource is IWithMinLimit minLimitResource ? minLimitResource.MinLimit : 0);
            
            return Amount - amount >= minLimit;
        }
        
        /// <summary>
        /// Checks if the container can store the specified amount.
        /// Do not mistake with <see cref="HasSpaceFor"/>
        /// </summary>
        /// <param name="amount">Amount of resource to check.</param>
        /// <returns>True if the container can store the resource, otherwise false.</returns>
        public virtual bool CanStore(long amount)
        {
            // Get max limit for resource
            long maxLimit = (long)(Resource is IWithMaxLimit maxLimitResource ? maxLimitResource.MaxLimit : long.MaxValue);
            
            return amount <= maxLimit;
        }
        
        /// <summary>
        /// Checks if this container has space for the specified amount.
        /// </summary>
        /// <param name="amount">Amount of resource to check.</param>
        /// <returns>True if the container has space for the resource, otherwise false.</returns>
        public virtual bool HasSpaceFor(long amount)
        {
            // Get max limit for resource
            long maxLimit = (long)(Resource is IWithMaxLimit maxLimitResource ? maxLimitResource.MaxLimit : long.MaxValue);
            
            return Amount + amount <= maxLimit;
        }
        
        protected virtual async UniTask CheckLimitsAndRaiseEvents()
        {
            // Acquire limit information from referenced status
            LimitHit limitHit = Resource.EnsureLimitsFor(this);
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
        }
    }
}