using FastUnityCreationKit.Unity.Actions.Interfaces.Results;

namespace FastUnityCreationKit.Unity.Actions.Results
{
    /// <summary>
    ///     Represents that this action is on cooldown.
    ///     This result is returned when action is executed while it's on cooldown and has not set a custom result
    ///     using 
    /// </summary>
    public struct DefaultActionIsOnCooldownResult : IActionIsOnCooldown
    {
        
    }
}