using FastUnityCreationKit.Status;

namespace FastUnityCreationKit.Tests.Status.Data
{
    public sealed class PercentageStatus : IPercentageStatus
    {
        float IPercentageStatus.Percentage { get; set; }
        
        public int wasMaxPercentageReached;
        public int wasMinPercentageReached;
        public int wasStatusAdded;
        public int wasStatusRemoved;
        
        public void OnStatusAdded(IObjectWithStatus objectWithStatus)
        {
            wasStatusAdded++;
        }

        public void OnStatusRemoved(IObjectWithStatus objectWithStatus)
        {
            wasStatusRemoved++;
        }


        public void OnMaxPercentageReached(IObjectWithStatus objectWithStatus)
        {
            wasMaxPercentageReached++;
        }

        public void OnMinPercentageReached(IObjectWithStatus objectWithStatus)
        {
            wasMinPercentageReached++;
            
            if(objectWithStatus is EntityWithStatus entityWithStatus)
            {
                entityWithStatus.statusPercentageReachedZeroTimes++;
            }
        }
        
        public float GetPercentage() => (this as IPercentageStatus).Percentage;
    }
}