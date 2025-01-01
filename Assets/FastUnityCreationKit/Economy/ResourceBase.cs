using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Addressables;
using FastUnityCreationKit.Annotations.Data;
using FastUnityCreationKit.Annotations.Unity;
using FastUnityCreationKit.Identification;
using FastUnityCreationKit.Utility.Limits;

namespace FastUnityCreationKit.Economy
{
    [AutoCreatedObject(LocalConstants.RESOURCES_OBJECT_DIRECTORY)]
    [AddressableGroup(LocalConstants.RESOURCE_ADDRESSABLE_TAG)]
    [AutoRegisterIn(typeof(ResourceDatabase))]
    public abstract class ResourceBase : UniqueDefinitionBase
    {
        public ResourceDatabase Database => ResourceDatabase.Instance;

        public virtual UniTask OnResourceAddedAsync(ResourceContainer container, long amount) => UniTask.CompletedTask;
        public virtual UniTask OnResourceAddFailedAsync(ResourceContainer container, long amount, long spaceLeft) =>
            UniTask.CompletedTask;
        public virtual UniTask OnResourceRemovedAsync(ResourceContainer container, long amount) =>
            UniTask.CompletedTask;
        public virtual UniTask OnResourceRemoveFailedAsync(ResourceContainer container,
            long amount,
            long availableAmount)
            => UniTask.CompletedTask;
        public virtual UniTask OnResourceChangedAsync(ResourceContainer container, long oldAmount, long newAmount) => UniTask.CompletedTask;
        public virtual UniTask OnMaxLimitReached(ResourceContainer container) => UniTask.CompletedTask;
        public virtual UniTask OnMinLimitReached(ResourceContainer container) => UniTask.CompletedTask;


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