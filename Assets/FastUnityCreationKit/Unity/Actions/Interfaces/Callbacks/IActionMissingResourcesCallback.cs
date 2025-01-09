using Cysharp.Threading.Tasks;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks
{
    public interface IActionMissingResourcesCallback : IActionCallback<IActionMissingResourcesCallback>
    {
        UniTask OnExecutedWhenMissingResources();
        
        async UniTask IActionCallback<IActionMissingResourcesCallback>.PerformCallback(IActionMissingResourcesCallback callback)
        {
            await callback.OnExecutedWhenMissingResources();
        }
    }
}