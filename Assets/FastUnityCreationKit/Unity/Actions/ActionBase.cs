using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Utility;
using FastUnityCreationKit.Unity.Time.Timers;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
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
        ///     Timer to use for cooldown.
        /// </summary>
        /// <remarks>
        ///     This is intentionally placed with <see cref="ActionBase"/> instead of <see cref="ActionBaseWithCooldown"/>
        ///     to allow accessing cooldown timer from derived classes without having to cast to <see cref="ActionBaseWithCooldown"/>.
        ///     <br/><br/>
        ///     <b>TODO: Think about moving this to <see cref="ActionBaseWithCooldown"/>.</b>
        /// </remarks>
        [ShowInInspector]
        [ReadOnly]
        [TitleGroup(GROUP_COOLDOWN)]
        [ShowIf(nameof(HasCooldown))]
        [CanBeNull]
        [field: SerializeField, HideInInspector]
        protected ActionCooldown CooldownTimer { get; set; }

        /// <summary>
        ///     Check if action has cooldown.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_COOLDOWN)] protected bool HasCooldown
            => CooldownTimer != null;

        /// <summary>
        ///     If <see cref="OnExecuted"/> returns one of these states, cooldown will start.
        ///     By default, only successful or interrupted actions will trigger cooldown.
        /// </summary>
        [field: SerializeField, HideInInspector]
        protected ActionExecutionState ExecutionStatesCausingCooldownToStart { get; set; }
            = ActionExecutionState.Success | ActionExecutionState.Interrupted;

        /// <summary>
        ///     Action event raised when action is executed. Returns action execution state which
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
        protected abstract UniTask<ActionExecutionState> OnExecuted();

        /// <summary>
        ///     Event raised when action execution failed due to cooldown.
        ///     Raised only for actions that inherit from <see cref="ActionBaseWithCooldown" />.
        /// </summary>
        protected virtual UniTask OnExecutedDuringCooldown()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Event raised when action cooldown has completed.
        /// </summary>
        protected virtual UniTask OnCooldownComplete()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Execute action. Returns state of action execution.
        ///     <ul>
        ///     <li>If action has cooldown, it will be checked and executed if not on cooldown.</li>
        ///     <li>If action is on cooldown, <see cref="OnExecutedDuringCooldown"/> will be called and
        ///     <see cref="ActionExecutionState.OnCooldown"/> will be returned. Otherwise, <see cref="OnExecuted"/>
        ///     will be called and its result will be returned.</li>
        ///     <li>If action execution was one of <see cref="ExecutionStatesCausingCooldownToStart"/>,
        ///     cooldown will start.</li>
        ///     </ul>
        /// </summary>
        public async UniTask<ActionExecutionState> Execute()
        {
            // Check if action has cooldown.
            // ReSharper disable once NullableWarningSuppressionIsUsed
            if (HasCooldown && CooldownTimer!.Enabled)
            {
                // Execute action during cooldown.
                await OnExecutedDuringCooldown();
                return ActionExecutionState.OnCooldown;
            }

            // Execute action.
            ActionExecutionState state = await OnExecuted();

            // Check if action execution state is one of the states that cause cooldown to start.
            // If not, return the state to prevent cooldown from starting.
            if ((state & ExecutionStatesCausingCooldownToStart) == 0) return state;

            // Successful action executions can trigger cooldown.
            if (HasCooldown) CooldownTimer?.Run();

            return state;
        }

        public UniTask Reset()
        {
            CooldownTimer?.Stop();
            CooldownTimer?.Reset();
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Cooldown timer for actions. This timer is used to prevent actions from being executed too often
        ///     and to provide a delay between action executions.
        /// </summary>
        [Serializable] protected sealed class ActionCooldown : OneShotTimerBase
        {
            /// <summary>
            ///     Base constructor for cooldown timer.
            /// </summary>
            internal ActionCooldown([NotNull] ActionBase action) : this(action, 0)
            {
            }

            public ActionCooldown([NotNull] ActionBase action, float totalTimeSeconds) : base(totalTimeSeconds)
            {
                OwnerReference = action;
            }

            public ActionCooldown([NotNull] ActionBase action, TimeSpan totalTime) : base(totalTime)
            {
                OwnerReference = action;
            }

            /// <summary>
            ///     Reference to action that owns this cooldown.
            /// </summary>
            [ShowInInspector]
            [ReadOnly]
            [TitleGroup(GROUP_CONFIGURATION)]
            [NotNull]
            [field: SerializeReference, HideInInspector]
            private ActionBase OwnerReference { get; set; }

            protected override bool ResetTimeToFull => false;
            public override bool RestartOnElapsed => false;
            public override bool DisposeOnElapsed => false;

            protected override async UniTask OnCompleted()
            {
                // Call base method.
                await OwnerReference.OnCooldownComplete();
            }
        }

#if UNITY_EDITOR
        [ShowInInspector] [Button(nameof(Execute), ButtonSizes.Medium)] private void ExecuteInEditor()
        {
            Execute().Forget();
        }

        [ShowInInspector] [ShowIf(nameof(HasCooldown))] [Button(nameof(ActionCooldown.Reset), ButtonSizes.Medium)]
        private void ResetCooldownInEditor()
        {
            Reset().Forget();
        }
#endif
    }
}