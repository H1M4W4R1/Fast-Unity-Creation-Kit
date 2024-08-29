using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Status.Enums;

namespace FastUnityCreationKit.Status.Tests.Data
{
    public sealed class StackablePercentageStatus : IStackablePercentageStatus
    {
        public int statusWasAdded = 0;
        public int statusWasRemoved = 0;
        public int stackCountChanged = 0;
        public int maxStackCountReached = 0;
        public int minStackCountReached = 0;

        public void OnStatusAdded(IObjectWithStatus objectWithStatus)
        {
            statusWasAdded++;
        }

        public void OnStatusRemoved(IObjectWithStatus objectWithStatus)
        {
            statusWasRemoved++;
        }

        float IPercentageStatus.Percentage { get; set; }

        int32 IStackableStatus.StackCount { get; set; }

        public MaxStackLimitReachedNotificationMode MaxStackLimitReachedNotificationMode => MaxStackLimitReachedNotificationMode.Once;

        public void OnStackCountChanged(IObjectWithStatus objectWithStatus, int amount)
        {
            stackCountChanged += amount;
        }

        public void OnMaxStackCountReached(IObjectWithStatus objectWithStatus)
        {
            maxStackCountReached++;
        }

        public void OnMinStackCountReached(IObjectWithStatus objectWithStatus)
        {
            minStackCountReached++;
        }
    }
}