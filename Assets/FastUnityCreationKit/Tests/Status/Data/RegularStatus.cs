using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Status;
using FastUnityCreationKit.Status.Context;

namespace FastUnityCreationKit.Tests.Status.Data
{
    /// <summary>
    /// Represents a regular status that does not have any additional properties.
    /// </summary>
    public sealed class RegularStatus : IStatus
    {
        public bool wasStatusAdded;
        public bool wasStatusRemoved;
        
        public UniTask OnStatusAddedAsync(IStatusContext context)
        {
            wasStatusAdded = true;
            return UniTask.CompletedTask;
        }

        public UniTask OnStatusRemovedAsync(IStatusContext context)
        {
            wasStatusRemoved = true;
            return UniTask.CompletedTask;
        }
    }
}