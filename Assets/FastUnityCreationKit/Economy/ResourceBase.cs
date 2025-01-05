using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Addressables;
using FastUnityCreationKit.Annotations.Data;
using FastUnityCreationKit.Annotations.Unity;
using FastUnityCreationKit.Core.Limits;
using FastUnityCreationKit.Identification;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.Economy
{
    [AutoCreatedObject(RESOURCES_OBJECT_DIRECTORY)]
    [AddressableGroup(RESOURCE_ADDRESSABLE_TAG)]
    [AutoRegisterIn(typeof(ResourceDatabase))]
    public abstract class ResourceBase : UniqueDefinitionBase
    {
        public ResourceDatabase Database => ResourceDatabase.Instance;

        public virtual UniTask OnResourceAddedAsync(ResourceContainerBase containerBase, int amount)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnResourceAddFailedAsync(
            ResourceContainerBase containerBase,
            int amount,
            int spaceLeft)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnResourceRemovedAsync(ResourceContainerBase containerBase, int amount)
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

        public virtual UniTask OnResourceChangedAsync(
            ResourceContainerBase containerBase,
            int oldAmount,
            int newAmount)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnMaxLimitReachedAsync(ResourceContainerBase containerBase)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnMinLimitReachedAsync(ResourceContainerBase containerBase)
        {
            return UniTask.CompletedTask;
        }
    }
}