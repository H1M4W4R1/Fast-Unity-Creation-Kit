using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Data.Attributes;
using FastUnityCreationKit.Identification;
using FastUnityCreationKit.Utility.Attributes;
using FastUnityCreationKit.Utility.Limits;

namespace FastUnityCreationKit.Economy
{
    [AutoCreatedObject(LocalConstants.RESOURCES_OBJECT_DIRECTORY)]
    [AddressableGroup(LocalConstants.RESOURCE_ADDRESSABLE_TAG)]
    [AutoRegisterIn(typeof(ResourceDatabase))]
    public abstract class ResourceBase : UniqueDefinitionBase
    {
        public ResourceDatabase Database => ResourceDatabase.Instance;
        
        public virtual UniTask OnResourceAddedAsync(ResourceContainer container, long amount)
        {
            return UniTask.CompletedTask;
        } 
        
        public virtual UniTask OnResourceAddFailedAsync(ResourceContainer container, long amount, long availableAmount)
        {
            return UniTask.CompletedTask;
        }
        
        public virtual UniTask OnResourceRemovedAsync(ResourceContainer container, long amount)
        {
            return UniTask.CompletedTask;
        }
        
        public virtual UniTask OnResourceRemoveFailedAsync(ResourceContainer container, long amount, long availableAmount)
        {
            return UniTask.CompletedTask;
        }
        
        public virtual UniTask OnMaxLimitReached(ResourceContainer container)
        {
            return UniTask.CompletedTask;
        }
        
        public virtual UniTask OnMinLimitReached(ResourceContainer container)
        {
            return UniTask.CompletedTask;
        }
        
        /// <summary>
        /// Ensure resource limits for the container.
        /// </summary>
        public LimitHit CheckLimitsFor(ResourceContainer container)
        {
            // Check if resource is limited
            if (this is not ILimited) return LimitHit.None;

            // Check if container is for this resource
            if (container.Identifier != Id)
            {
                return LimitHit.None;
            }

            // Check max status limit
            if (this is IWithMaxLimit maxLimit && container.Amount > maxLimit.MaxLimit)
                return LimitHit.UpperLimitHit;

            // Check min status limit
            if (this is IWithMinLimit minLimit && container.Amount < minLimit.MinLimit)
                return LimitHit.LowerLimitHit;

            return LimitHit.None;
        }
    }
}