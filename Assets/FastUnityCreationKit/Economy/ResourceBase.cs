using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Addressables;
using FastUnityCreationKit.Annotations.Data;
using FastUnityCreationKit.Annotations.Info;
using FastUnityCreationKit.Annotations.Unity;
using FastUnityCreationKit.Identification;
using FastUnityCreationKit.Core.Limits;

namespace FastUnityCreationKit.Economy
{
    [AutoCreatedObject(LocalConstants.RESOURCES_OBJECT_DIRECTORY)]
    [AddressableGroup(LocalConstants.RESOURCE_ADDRESSABLE_TAG)]
    [AutoRegisterIn(typeof(ResourceDatabase))]
    [SupportedFeature(typeof(ILimited))]
    public abstract class ResourceBase : UniqueDefinitionBase
    {
        public ResourceDatabase Database => ResourceDatabase.Instance;

        public virtual UniTask OnResourceAddedAsync(ResourceReference reference, long amount) => UniTask.CompletedTask;
        public virtual UniTask OnResourceAddFailedAsync(ResourceReference reference, long amount, long spaceLeft) =>
            UniTask.CompletedTask;
        public virtual UniTask OnResourceRemovedAsync(ResourceReference reference, long amount) =>
            UniTask.CompletedTask;
        public virtual UniTask OnResourceRemoveFailedAsync(ResourceReference reference,
            long amount,
            long availableAmount)
            => UniTask.CompletedTask;
        public virtual UniTask OnResourceChangedAsync(ResourceReference reference, long oldAmount, long newAmount) => UniTask.CompletedTask;
        public virtual UniTask OnMaxLimitReached(ResourceReference reference) => UniTask.CompletedTask;
        public virtual UniTask OnMinLimitReached(ResourceReference reference) => UniTask.CompletedTask;


        /// <summary>
        /// Ensure resource limits for the container.
        /// </summary>
        public LimitHit CheckLimitsFor(ResourceReference reference)
        {
            // Check if resource is limited
            // ReSharper disable once Unity.NoNullPatternMatching
            if (this is not ILimited) return LimitHit.None;

            // Check if container is for this resource
            if (reference.Identifier != Id)
            {
                return LimitHit.None;
            }

            // Check max status limit
            if (this is IWithMaxLimit maxLimit && reference.Amount > maxLimit.MaxLimit)
                return LimitHit.UpperLimitHit;

            // Check min status limit
            if (this is IWithMinLimit minLimit && reference.Amount < minLimit.MinLimit)
                return LimitHit.LowerLimitHit;

            return LimitHit.None;
        }
    }
}