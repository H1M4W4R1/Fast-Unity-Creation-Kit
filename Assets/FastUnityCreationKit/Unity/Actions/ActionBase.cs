using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Utility;
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
        protected abstract UniTask<ActionExecutionState> PerformExecution();

        /// <summary>
        ///     Event raised when action execution has failed.
        /// </summary>
        protected internal virtual UniTask OnExecutionFailed()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Event raised when action execution was interrupted by other action or event.
        /// </summary>
        protected internal virtual UniTask OnExecutionInterrupted()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Event raised when action execution was successful.
        /// </summary>
        protected internal virtual UniTask OnExecutionSuccess()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Event raised when action execution started.
        /// </summary>
        protected internal virtual UniTask OnExecutionStarted()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Event raised when action execution finished.
        /// </summary>
        protected internal virtual UniTask OnExecutionFinished()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Event raised when action execution was cancelled by user.
        /// </summary>
        /// <returns></returns>
        protected internal virtual UniTask OnExecutionCancelled()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Event raised when action execution failed due to missing requirements.
        /// </summary>
        protected internal virtual UniTask OnMissingRequirements()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Event raised when action is not ready to be executed. Do not mistake this with
        ///     <see cref="ActionBaseWithCooldown.OnExecutedDuringCooldown"/>
        /// </summary>
        protected internal virtual UniTask OnActionNotReady()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Event raised when action is disabled and can't be executed.
        /// </summary>
        protected internal virtual UniTask OnActionDisabled()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Perform action execution. This method is called by <see cref="Execute"/> method.
        ///     Can be used to implement custom logic for action execution.
        /// </summary>
        /// <remarks>
        ///     You can return <see cref="ActionExecutionState"/> to indicate the result of the action execution or
        ///     call <see cref="PerformExecution"/> if your logic checks have passed. <br/><br/>
        ///     Default implementation of this method always calls <see cref="PerformExecution"/> method.
        /// </remarks>
        protected internal virtual async UniTask<ActionExecutionState> TryPerformExecution()
        {
            return await PerformExecution();
        }

        /// <summary>
        ///     Execute action. Returns state of action execution.
        /// </summary>
        public virtual async UniTask<ActionExecutionState> Execute()
        {
            // Starts action execution
            await OnExecutionStarted();

            // Perform action execution
            ActionExecutionState actionState = await TryPerformExecution();

            // Check action execution state and call appropriate event
            switch (actionState)
            {
                // This should be sorted in logic order of execution to suggest the flow of the action
                // which should prevent confusion and make it easier to read and implement custom logic.
                case ActionExecutionState.Disabled: await OnActionDisabled(); break;
                case ActionExecutionState.NotReady: await OnActionNotReady(); break;
                case ActionExecutionState.MissingRequirements: await OnMissingRequirements(); break;
                case ActionExecutionState.OnCooldown:
                    if (this is ActionBaseWithCooldown withCooldown) await withCooldown.OnExecutedDuringCooldown();
                    break;
                case ActionExecutionState.Interrupted: await OnExecutionInterrupted(); break;
                case ActionExecutionState.Cancelled: await OnExecutionCancelled(); break;
                case ActionExecutionState.Success: await OnExecutionSuccess(); break;
                case ActionExecutionState.Failed: await OnExecutionFailed(); break;

                default: break;
            }

            // Finish action execution
            await OnExecutionFinished();
            return actionState;
        }
    }
}