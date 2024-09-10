using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Status;

namespace FastUnityCreationKit.Tests.Status.Data
{
    /// <summary>
    /// Represents a regular status that does not have any additional properties.
    /// </summary>
    public sealed class RegularStatus : IStatus
    {
        public bool wasStatusAdded;
        public bool wasStatusRemoved;
        
        public UniTask OnStatusAddedAsync(IObjectWithStatus objectWithStatus)
        {
            wasStatusAdded = true;
            return UniTask.CompletedTask;
        }

        public UniTask OnStatusRemovedAsync(IObjectWithStatus objectWithStatus)
        {
            wasStatusRemoved = true;
            return UniTask.CompletedTask;
        }
    }
}