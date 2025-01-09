using FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Results
{
    /// <summary>
    ///     Represents that action failed because it is disabled.
    /// </summary>
    public interface IActionDisabled : IActionFailed<IActionDisabledCallback>
    {
        
    }
}