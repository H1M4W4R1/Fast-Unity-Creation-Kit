using Cysharp.Threading.Tasks;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks
{
    public interface IActionCancelledCallback : IActionCallback<IActionCancelledCallback>
    {
        UniTask OnActionCancelled();
        
        async UniTask IActionCallback<IActionCancelledCallback>.PerformCallback(IActionCancelledCallback callback)
        {
            await callback.OnActionCancelled();
        }
    }
}