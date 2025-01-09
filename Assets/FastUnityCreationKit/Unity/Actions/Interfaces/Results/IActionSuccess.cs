using FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Results
{
    /// <summary>
    ///     Represents the result of a successful action - action that has been executed without any errors.
    /// </summary>
    public interface IActionSuccess<in TCallbackInterface> : IActionResult<TCallbackInterface>
        where TCallbackInterface : IActionCallback<TCallbackInterface>
    {
        
    }
}