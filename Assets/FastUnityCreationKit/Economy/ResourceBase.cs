using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Addressables;
using FastUnityCreationKit.Annotations.Data;
using FastUnityCreationKit.Annotations.Unity;
using FastUnityCreationKit.Identification;
using JetBrains.Annotations;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.Economy
{
    [AutoCreatedObject(RESOURCES_OBJECT_DIRECTORY)]
    [AddressableGroup(RESOURCE_ADDRESSABLE_TAG)]
    [AutoRegisterIn(typeof(ResourceDatabase))]
    public abstract class ResourceBase : UniqueDefinitionBase
    {
        [NotNull] public ResourceDatabase Database => ResourceDatabase.Instance;

        public virtual UniTask OnResourceAdded([NotNull] ResourceContainerBase containerBase, int amount)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnResourceAddFailed(
            [NotNull] ResourceContainerBase containerBase,
            int amount,
            int spaceLeft)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnResourceRemoved([NotNull] ResourceContainerBase containerBase, int amount)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnResourceRemoveFailed(
            [NotNull] ResourceContainerBase containerBase,
            long amount,
            long availableAmount)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnResourceChanged(
            [NotNull] ResourceContainerBase containerBase,
            int oldAmount,
            int newAmount)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnMaxLimitReached([NotNull] ResourceContainerBase containerBase)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnMinLimitReached([NotNull] ResourceContainerBase containerBase)
        {
            return UniTask.CompletedTask;
        }
    }
}