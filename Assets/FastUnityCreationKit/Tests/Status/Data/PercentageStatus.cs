using Cysharp.Threading.Tasks;
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


        public UniTask OnMaxPercentageReachedAsync(IObjectWithStatus objectWithStatus)
        {
            wasMaxPercentageReached++;
            return UniTask.CompletedTask;
        }

        public UniTask OnMinPercentageReachedAsync(IObjectWithStatus objectWithStatus)
        {
            wasMinPercentageReached++;
            
            if(objectWithStatus is EntityWithStatus entityWithStatus)
            {
                entityWithStatus.statusPercentageReachedZeroTimes++;
            }
            
            return UniTask.CompletedTask;
        }
        
        public float GetPercentage() => (this as IPercentageStatus).Percentage;
    }
}