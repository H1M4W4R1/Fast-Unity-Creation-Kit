using FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Results
{
    /// <summary>
    ///     Represents that action has failed because it is missing resources.
    /// </summary>
    public interface IActionMissingResources : IActionFailed<IActionMissingResourcesCallback>
    {
        
    }
}