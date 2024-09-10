using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Status;

namespace FastUnityCreationKit.Tests.Status.Data
{
    public sealed class NotSupportedStatusMockup : IStatus
    {
        public UniTask OnStatusAddedAsync(IObjectWithStatus objectWithStatus)
        {
            return UniTask.CompletedTask;
        }

        public UniTask OnStatusRemovedAsync(IObjectWithStatus objectWithStatus)
        {
            return UniTask.CompletedTask;
        }
    }
}