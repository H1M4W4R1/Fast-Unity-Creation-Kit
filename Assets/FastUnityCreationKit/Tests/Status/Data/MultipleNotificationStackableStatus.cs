using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;
using FastUnityCreationKit.Status;
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

        public UniTask OnStatusAddedAsync(IObjectWithStatus objectWithStatus)
        {
            wasStatusAdded++;
            return UniTask.CompletedTask;
        }

        public UniTask OnStatusRemovedAsync(IObjectWithStatus objectWithStatus)
        {
            wasStatusRemoved++;
            return UniTask.CompletedTask;
        }

        int32 IStackableStatus.StackCount { get; set; }

        MaxStackLimitReachedNotificationMode IStackableStatus.MaxStackLimitReachedNotificationMode =>
            MaxStackLimitReachedNotificationMode.EveryTime;

        MinStackLimitReachedNotificationMode IStackableStatus.MinStackLimitReachedNotificationMode =>
            MinStackLimitReachedNotificationMode.EveryTime;


        public UniTask OnStackCountChangedAsync(IObjectWithStatus objectWithStatus, int amount)
        {
            wasStackCountChanged += amount;
            return UniTask.CompletedTask;
        }

        public UniTask OnMaxStackCountReachedAsync(IObjectWithStatus objectWithStatus)
        {
            wasMaxStackCountReached++;
            return UniTask.CompletedTask;
        }

        public UniTask OnMinStackCountReachedAsync(IObjectWithStatus objectWithStatus)
        {
            wasMinStackCountReached++;
            return UniTask.CompletedTask;
        }

        public int32 MaxLimit => 3;
        public int32 MinLimit => -3;
        public int CurrentStack => (this as IStackableStatus).StackCount;
    }
}