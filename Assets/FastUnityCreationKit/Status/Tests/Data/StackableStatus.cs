using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;

namespace FastUnityCreationKit.Status.Tests.Data
{
    public class StackableStatus : IStackableStatus, IWithMaxLimit<int32>, IWithMinLimit<int32>
    {
        public int wasStatusAdded;
        public int wasStatusRemoved;
        public int wasStackCountChanged;
        public int wasMaxStackCountReached;
        public int wasMinStackCountReached;

        public void OnStatusAdded(IObjectWithStatus objectWithStatus)
        {
            wasStatusAdded++;
        }

        public void OnStatusRemoved(IObjectWithStatus objectWithStatus)
        {
            wasStatusRemoved++;
        }

        int32 IStackableStatus.StackCount { get; set; }

        public void OnStackCountChanged(IObjectWithStatus objectWithStatus, int amount)
        {
            wasStackCountChanged += amount;
        }

        public void OnMaxStackCountReached(IObjectWithStatus objectWithStatus)
        {
            wasMaxStackCountReached++;
        }

        public void OnMinStackCountReached(IObjectWithStatus objectWithStatus)
        {
            wasMinStackCountReached++;
        }

        public int32 MaxLimit => 3;
        public int32 MinLimit => -3;
        public int CurrentStack => (this as IStackableStatus).StackCount;
    }
}