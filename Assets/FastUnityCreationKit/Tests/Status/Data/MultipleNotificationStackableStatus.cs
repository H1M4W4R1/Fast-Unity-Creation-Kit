using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;
using FastUnityCreationKit.Status;
using FastUnityCreationKit.Status.Context;
using FastUnityCreationKit.Status.Enums;

namespace FastUnityCreationKit.Tests.Status.Data
{
    public sealed class MultipleNotificationStackableStatus : IStackableStatus, IWithMaxLimit<int32>,
        IWithMinLimit<int32>
    {
        public int wasStatusAdded;
        public int wasStatusRemoved;
        public int wasStackCountChanged;
        public int wasMaxStackCountReached;
        public int wasMinStackCountReached;

        public UniTask OnStatusAddedAsync(IStatusContext context)
        {
            wasStatusAdded++;
            return UniTask.CompletedTask;
        }

        public UniTask OnStatusRemovedAsync(IStatusContext context)
        {
            wasStatusRemoved++;
            return UniTask.CompletedTask;
        }

        int32 IStackableStatus.StackCount { get; set; }

        MaxStackLimitReachedNotificationMode IStackableStatus.MaxStackLimitReachedNotificationMode =>
            MaxStackLimitReachedNotificationMode.EveryTime;

        MinStackLimitReachedNotificationMode IStackableStatus.MinStackLimitReachedNotificationMode =>
            MinStackLimitReachedNotificationMode.EveryTime;


        public UniTask OnStackCountChangedAsync(IStatusContext context, int amount)
        {
            wasStackCountChanged += amount;
            return UniTask.CompletedTask;
        }

        public UniTask OnMaxStackCountReachedAsync(IStatusContext context)
        {
            wasMaxStackCountReached++;
            return UniTask.CompletedTask;
        }

        public UniTask OnMinStackCountReachedAsync(IStatusContext context)
        {
            wasMinStackCountReached++;
            return UniTask.CompletedTask;
        }

        public int32 MaxLimit => 3;
        public int32 MinLimit => -3;
        public int CurrentStack => (this as IStackableStatus).StackCount;
    }
}