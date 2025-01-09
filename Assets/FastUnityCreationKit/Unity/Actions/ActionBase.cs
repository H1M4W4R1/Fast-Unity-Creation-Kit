using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Utility;
using FastUnityCreationKit.Unity.Actions.Interfaces.Results;
using FastUnityCreationKit.Unity.Time.Timers;
using Sirenix.OdinInspector;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.Unity.Actions
{
    /// <summary>
    ///     Represents an action that can be executed
    ///     for example this can be "Return to base" command in RTS or player active skills in ARPG. <br /><br />
    ///     If you need to add cooldown to the action you can simply use
    ///     <see cref="ActionBaseWithCooldown" /> instead.
    /// </summary>
    /// <remarks>
    ///     <ul>
    ///     <li>If you want to provide cooldown for the action, you can inherit from <see cref="ActionBaseWithCooldown" />.</li>
    ///     <li>By default, action execution does not have any arguments, but you can pass them as custom
    ///     properties in derived classes.</li>
    ///     </ul>
    /// </remarks>
    /// <seealso cref="ActionBaseWithCooldown" />
    [Serializable] [OnlySealed] public abstract class ActionBase
    {
        /// <summary>
        ///     Check if action has cooldown.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_COOLDOWN)] protected bool HasCooldown
            => this is ActionBaseWithCooldown;

        /// <summary>
        ///     Internal method called when action is executed. Returns action execution state which
        ///     represents how the action execution went.
        /// </summary>
        /// <remarks>
        ///     It's possible to implement a custom timer for actions that have warmup (like whirlwind attack)
        ///     or channeling (like channeled spells) and attaching to <see cref="TimerBase.OnTimePassed"/>
        ///     event during this task to validate if action was interrupted or not by pooling for example for
        ///     some sort of stunned status. <br /><br />
        ///     If action is being warmed-up you can use <see cref="UniTask.Yield()"/> or
        ///     <see cref="UniTask.NextFrame()"/> to wait for the warm-up to finish before executing the action.
        /// </remarks>
        protected abstract UniTask<IActionResult> PerformExecution();

        /// <summary>
        ///     Execute action. Returns state of action execution.
        /// </summary>
        public virtual async UniTask<IActionResult> Execute()
        {
            // Get action execution state (result)
            IActionResult actionResult = await PerformExecution();
            
            // Perform result callback on action
            await actionResult.TryPerformCallbackOn(this);

            return actionResult;
        }
    }
}