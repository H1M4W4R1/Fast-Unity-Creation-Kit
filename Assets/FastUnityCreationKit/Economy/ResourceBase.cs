using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Addressables;
using FastUnityCreationKit.Annotations.Data;
using FastUnityCreationKit.Annotations.Unity;
using FastUnityCreationKit.Core.Limits;
using FastUnityCreationKit.Identification;

namespace FastUnityCreationKit.Economy
{
    [AutoCreatedObject(LocalConstants.RESOURCES_OBJECT_DIRECTORY)]
    [AddressableGroup(LocalConstants.RESOURCE_ADDRESSABLE_TAG)]
    [AutoRegisterIn(typeof(ResourceDatabase))]
    public abstract class ResourceBase : UniqueDefinitionBase
    {
        public ResourceDatabase Database => ResourceDatabase.Instance;

        public virtual UniTask OnResourceAddedAsync(ResourceContainerBase containerBase, long amount)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnResourceAddFailedAsync(ResourceContainerBase containerBase, long amount, long spaceLeft)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnResourceRemovedAsync(ResourceContainerBase containerBase, long amount)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnResourceRemoveFailedAsync(
            ResourceContainerBase containerBase,
            long amount,
            long availableAmount)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnResourceChangedAsync(ResourceContainerBase containerBase, long oldAmount, long newAmount)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnMaxLimitReached(ResourceContainerBase containerBase)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnMinLimitReached(ResourceContainerBase containerBase)
        {
            return UniTask.CompletedTask;
        }


        /// <summary>
        ///     Ensure resource limits for the container.
        /// </summary>
        public LimitHit CheckLimitsFor(ResourceContainerBase containerBase)
        {
            // Check if resource is limited
            // ReSharper disable once Unity.NoNullPatternMatching
            if (this is not ILimited) return LimitHit.None;

            // Check if container is for this resource
            if (containerBase.Identifier != Id) return LimitHit.None;

            // Check max status limit
            if (this is IWithMaxLimit maxLimit && containerBase.Amount > maxLimit.MaxLimit)
                return LimitHit.UpperLimitHit;

            // Check min status limit
            if (this is IWithMinLimit minLimit && containerBase.Amount < minLimit.MinLimit)
                return LimitHit.LowerLimitHit;

            return LimitHit.None;
        }
    }
}