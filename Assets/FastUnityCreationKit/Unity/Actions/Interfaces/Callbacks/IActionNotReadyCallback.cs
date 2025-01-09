using Cysharp.Threading.Tasks;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks
{
    public interface IActionNotReadyCallback : IActionCallback<IActionNotReadyCallback>
    {
        UniTask OnExecutedWhenNotReady();
        
        async UniTask IActionCallback<IActionNotReadyCallback>.PerformCallback(IActionNotReadyCallback callback)
        {
            await callback.OnExecutedWhenNotReady();
        }
        
    }
}