namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// Represents a status that can be stacked and has a percentage value.
    /// </summary>
    public interface IStackablePercentageStatus : IStackableStatus, IPercentageStatus
    {
        void IPercentageStatus.OnMaxPercentageReached(IObjectWithStatus objectWithStatus)
        {
            IncreaseStackCount(objectWithStatus);
            Percentage = 0;
        }

        void IPercentageStatus.OnMinPercentageReached(IObjectWithStatus objectWithStatus)
        {
            // If there are stacks left, decrease the stack count and set the percentage to 100%.
            if (StackCount <= 0) return;
            
            DecreaseStackCount(objectWithStatus);
            Percentage = 1;
        }
    }
}