using FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Results
{
    /// <summary>
    ///     Represents that action has failed because it is on cooldown.
    /// </summary>
    public interface IActionIsOnCooldown : IActionFailed<IActionIsOnCooldownCallback>
    {
    }
}