using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;

namespace FastUnityCreationKit.Status.Tests.Data
{
    public class StackableStatus : IStackableStatus, IWithMaxLimit<int32>, IWithMinLimit<int32>
    {
        public bool wasStatusAdded;
        public bool wasStatusRemoved;
        public bool wasStackCountIncreased;
        public bool wasStackCountDecreased;
        public bool wasMaxStackCountReached;
        public bool wasMinStackCountReached;

        public void OnStatusAdded(IObjectWithStatus objectWithStatus)
        {
            wasStatusAdded = true;
        }

        public void OnStatusRemoved(IObjectWithStatus objectWithStatus)
        {
            wasStatusRemoved = true;
        }

        int32 IStackableStatus.StackCount { get; set; }

        public void OnStackCountIncreased(IObjectWithStatus objectWithStatus, int amount)
        {
            wasStackCountIncreased = true;
        }

        public void OnStackCountDecreased(IObjectWithStatus objectWithStatus, int amount)
        {
            wasStackCountDecreased = true;
        }

        public void OnMaxStackCountReached(IObjectWithStatus objectWithStatus)
        {
            wasMaxStackCountReached = true;
        }

        public void OnMinStackCountReached(IObjectWithStatus objectWithStatus)
        {
            wasMinStackCountReached = true;
        }

        public int32 MaxLimit => 3;
        public int32 MinLimit => -3;
        public int CurrentStack => (this as IStackableStatus).StackCount;
    }
}