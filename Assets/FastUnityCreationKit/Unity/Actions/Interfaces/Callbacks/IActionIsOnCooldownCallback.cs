using Cysharp.Threading.Tasks;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks
{
    public interface IActionIsOnCooldownCallback : IActionCallback<IActionIsOnCooldownCallback>
    {
        UniTask OnExecutedWhenOnCooldown();
        
        async UniTask IActionCallback<IActionIsOnCooldownCallback>.PerformCallback(IActionIsOnCooldownCallback callback)
        {
            await callback.OnExecutedWhenOnCooldown();
        }
    }
}