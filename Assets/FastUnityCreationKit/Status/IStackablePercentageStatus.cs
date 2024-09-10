using Cysharp.Threading.Tasks;

namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// Represents a status that can be stacked and has a percentage value.
    /// </summary>
    public interface IStackablePercentageStatus : IStackableStatus, IPercentageStatus
    {
        async UniTask IPercentageStatus.OnMaxPercentageReachedAsync(IObjectWithStatus objectWithStatus)
        {
            await IncreaseStackCountAsync(objectWithStatus);
            Percentage = 0;
        }

        async UniTask IPercentageStatus.OnMinPercentageReachedAsync(IObjectWithStatus objectWithStatus)
        {
            // If there are stacks left, decrease the stack count and set the percentage to 100%.
            if (StackCount <= 0) return;
            
            await DecreaseStackCountAsync(objectWithStatus);
            Percentage = 1;
        }
    }
}