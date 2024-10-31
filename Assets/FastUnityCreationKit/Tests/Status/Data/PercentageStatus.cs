using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Status;
using FastUnityCreationKit.Status.Context;

namespace FastUnityCreationKit.Tests.Status.Data
{
    public sealed class PercentageStatus : IPercentageStatus
    {
        float IPercentageStatus.Percentage { get; set; }
        
        public int wasMaxPercentageReached;
        public int wasMinPercentageReached;
        public int wasStatusAdded;
        public int wasStatusRemoved;
        
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


        public UniTask OnMaxPercentageReachedAsync(IStatusContext context)
        {
            wasMaxPercentageReached++;
            return UniTask.CompletedTask;
        }

        public UniTask OnMinPercentageReachedAsync(IStatusContext context)
        {
            wasMinPercentageReached++;
            
            if(context.ObjectReference is EntityWithStatus entityWithStatus)
            {
                entityWithStatus.statusPercentageReachedZeroTimes++;
            }
            
            return UniTask.CompletedTask;
        }
        
        public float GetPercentage() => (this as IPercentageStatus).Percentage;
    }
}