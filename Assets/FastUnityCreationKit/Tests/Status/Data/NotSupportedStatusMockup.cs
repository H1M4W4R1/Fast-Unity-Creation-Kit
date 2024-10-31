using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Status;
using FastUnityCreationKit.Status.Context;

namespace FastUnityCreationKit.Tests.Status.Data
{
    public sealed class NotSupportedStatusMockup : IStatus
    {
        public UniTask OnStatusAddedAsync(IStatusContext context)
        {
            return UniTask.CompletedTask;
        }

        public UniTask OnStatusRemovedAsync(IStatusContext context)
        {
            return UniTask.CompletedTask;
        }
    }
}