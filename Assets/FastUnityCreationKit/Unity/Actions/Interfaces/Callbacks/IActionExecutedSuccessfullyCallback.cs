using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Actions.Interfaces.Results;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks
{
    public interface IActionExecutedSuccessfullyCallback : IActionCallback<IActionExecutedSuccessfullyCallback>
    {
        UniTask OnExecuted();

        async UniTask IActionCallback<IActionExecutedSuccessfullyCallback>.PerformCallback(
            IActionExecutedSuccessfullyCallback callback)
        {
            await callback.OnExecuted();
        }
    }
}