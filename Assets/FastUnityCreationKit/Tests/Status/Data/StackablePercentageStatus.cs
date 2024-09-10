using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Status;
using FastUnityCreationKit.Status.Enums;

namespace FastUnityCreationKit.Tests.Status.Data
{
    public sealed class StackablePercentageStatus : IStackablePercentageStatus
    {
        public int statusWasAdded = 0;
        public int statusWasRemoved = 0;
        public int stackCountChanged = 0;
        public int maxStackCountReached = 0;
        public int minStackCountReached = 0;

        public UniTask OnStatusAddedAsync(IObjectWithStatus objectWithStatus)
        {
            statusWasAdded++;
            return UniTask.CompletedTask;
        }

        public UniTask OnStatusRemovedAsync(IObjectWithStatus objectWithStatus)
        {
            statusWasRemoved++;
            return UniTask.CompletedTask;
        }

        float IPercentageStatus.Percentage { get; set; }

        int32 IStackableStatus.StackCount { get; set; }

        public MaxStackLimitReachedNotificationMode MaxStackLimitReachedNotificationMode => MaxStackLimitReachedNotificationMode.Once;

        public UniTask OnStackCountChangedAsync(IObjectWithStatus objectWithStatus, int amount)
        {
            stackCountChanged += amount;
            return UniTask.CompletedTask;
        }

        public UniTask OnMaxStackCountReachedAsync(IObjectWithStatus objectWithStatus)
        {
            maxStackCountReached++;
            return UniTask.CompletedTask;
        }

        public UniTask OnMinStackCountReachedAsync(IObjectWithStatus objectWithStatus)
        {
            minStackCountReached++;
            return UniTask.CompletedTask;
        }
    }
}