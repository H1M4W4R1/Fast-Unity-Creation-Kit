using FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Results
{
    /// <summary>
    ///     Represents the result of a failed action - action that has been executed with errors.
    /// </summary>
    public interface IActionFailed<in TCallbackInterface> : IActionResult<TCallbackInterface>
        where TCallbackInterface : IActionCallback<TCallbackInterface>
    {
        
    }
}