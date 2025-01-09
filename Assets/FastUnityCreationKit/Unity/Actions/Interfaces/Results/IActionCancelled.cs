using FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Results
{
    public interface IActionCancelled : IActionFailed<IActionCancelledCallback>
    {
        
    }
}