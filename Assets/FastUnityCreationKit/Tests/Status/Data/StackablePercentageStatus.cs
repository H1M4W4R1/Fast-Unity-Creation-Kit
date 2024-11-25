using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Status;
using FastUnityCreationKit.Status.Context;
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

        public UniTask OnStatusAddedAsync(IStatusContext context)
        {
            statusWasAdded++;
            return UniTask.CompletedTask;
        }

        public UniTask OnStatusRemovedAsync(IStatusContext context)
        {
            statusWasRemoved++;
            return UniTask.CompletedTask;
        }

        float IPercentageStatus.Percentage { get; set; }

        int32 IStackableStatus.StackCount { get; set; }

        public MaxStackLimitReachedNotificationMode MaxStackLimitReachedNotificationMode => MaxStackLimitReachedNotificationMode.Once;

        public UniTask OnStackCountChangedAsync(IStatusContext context, int amount)
        {
            stackCountChanged += amount;
            return UniTask.CompletedTask;
        }

        public UniTask OnMaxStackCountReachedAsync(IStatusContext context)
        {
            maxStackCountReached++;
            return UniTask.CompletedTask;
        }

        public UniTask OnMinStackCountReachedAsync(IStatusContext context)
        {
            minStackCountReached++;
            return UniTask.CompletedTask;
        }
    }
}