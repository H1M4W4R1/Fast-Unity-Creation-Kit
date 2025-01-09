using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Results
{
    /// <summary>
    ///     Represents the result of an action.
    /// </summary>
    public interface IActionResult<in TCallbackInterface> : IActionResult
        where TCallbackInterface : IActionCallback<TCallbackInterface>
    {
        /// <summary>
        ///     Performs the callback for this result on provided action.
        ///     TCallbackInterface is provided to allow for accessing specific
        ///     callback without doing any weird enum things.
        /// </summary>
        async UniTask PerformCallbackOn([NotNull] TCallbackInterface callback)
        {
            await callback.PerformCallback(callback);
        }
        
        async UniTask IActionResult.TryPerformCallbackOn(ActionBase action)
        {
            // Ensure that the action is of the correct type.
            if(action is TCallbackInterface callback)
                await PerformCallbackOn(callback);
        }
    }

    /// <summary>
    ///     Internal interface for results. Do not implement this interface directly.
    ///     Instead, implement <see cref="IActionResult{TCallbackInterface}"/>.
    /// </summary>
    public interface IActionResult
    {
        internal UniTask TryPerformCallbackOn([NotNull] ActionBase action);
    }
}