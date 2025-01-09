using Cysharp.Threading.Tasks;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks
{
    /// <summary>
    ///     Represents that action has event that occurs when it returns disabled state.
    /// </summary>
    public interface IActionDisabledCallback : IActionCallback<IActionDisabledCallback>
    {
        UniTask OnExecutedWhenDisabled();
        
        async UniTask IActionCallback<IActionDisabledCallback>.PerformCallback(IActionDisabledCallback callback)
        {
            await callback.OnExecutedWhenDisabled();
        }
    }
}