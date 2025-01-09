using FastUnityCreationKit.Unity.Actions.Interfaces.Results;
using FastUnityCreationKit.Unity.Actions.Results;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Configuration
{
    /// <summary>
    ///     Used with <see cref="ActionBaseWithCooldown"/> to specify what result should be returned when action is on cooldown.
    /// </summary>
    public interface IWithCooldownResult<out TCooldownResult> : IWithCooldownResult
        where TCooldownResult : IActionIsOnCooldown, new()
    {
        IActionIsOnCooldown IWithCooldownResult.GetResult() => new TCooldownResult();
    }
    
    public interface IWithCooldownResult
    {
        /// <summary>
        ///     Create new instance of <see cref="IActionIsOnCooldown"/>.
        /// </summary>
        [NotNull] public IActionIsOnCooldown GetResult() => new DefaultActionIsOnCooldownResult();
    }
}