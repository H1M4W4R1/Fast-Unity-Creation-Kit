using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Status.Context;

namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// Represents a status that can be stacked and has a percentage value.
    /// </summary>
    public interface IStackablePercentageStatus : IStackableStatus, IPercentageStatus
    {
        async UniTask IPercentageStatus.OnMaxPercentageReachedAsync(IStatusContext context)
        {
            await IncreaseStackCountAsync(context);
            Percentage = 0;
        }

        async UniTask IPercentageStatus.OnMinPercentageReachedAsync(IStatusContext context)
        {
            // If there are stacks left, decrease the stack count and set the percentage to 100%.
            if (StackCount <= 0) return;
            
            await DecreaseStackCountAsync(context);
            Percentage = 1;
        }
    }
}