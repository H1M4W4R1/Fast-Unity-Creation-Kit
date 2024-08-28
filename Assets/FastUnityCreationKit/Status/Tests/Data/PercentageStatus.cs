namespace FastUnityCreationKit.Status.Tests.Data
{
    public class PercentageStatus : IPercentageStatus
    {
        float IPercentageStatus.Percentage { get; set; }
        
        public bool wasMaxPercentageReached;
        public bool wasMinPercentageReached;
        public bool wasStatusAdded;
        public bool wasStatusRemoved;
        
        public void OnStatusAdded(IObjectWithStatus objectWithStatus)
        {
            wasStatusAdded = true;
        }

        public void OnStatusRemoved(IObjectWithStatus objectWithStatus)
        {
            wasStatusRemoved = true;
        }


        public void OnMaxPercentageReached(IObjectWithStatus objectWithStatus)
        {
            wasMaxPercentageReached = true;
        }

        public void OnMinPercentageReached(IObjectWithStatus objectWithStatus)
        {
            wasMinPercentageReached = true;
        }
        
        public float GetPercentage() => (this as IPercentageStatus).Percentage;
    }
}