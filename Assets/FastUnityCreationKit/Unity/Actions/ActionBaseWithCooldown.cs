using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Time.Timers;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.Unity.Actions
{
    /// <summary>
    ///     Action with cooldown.
    /// </summary>
    public abstract class ActionBaseWithCooldown : ActionBase
    {
        private const string COOLDOWN_TO_STRING =
            "@" + nameof(CooldownTimer) + "." + nameof(ActionCooldown.ToString) + "()";

        /// <summary>
        ///     Timer to use for cooldown.
        /// </summary>
        /// <remarks>
        ///     This is intentionally placed with <see cref="ActionBase"/> instead of <see cref="ActionBaseWithCooldown"/>
        ///     to allow accessing cooldown timer from derived classes without having to cast to <see cref="ActionBaseWithCooldown"/>.
        ///     <br/><br/>
        /// </remarks>
        [ShowInInspector]
        [ReadOnly]
        [TitleGroup(GROUP_COOLDOWN)]
        [ShowIf(nameof(HasCooldown))]
        [NotNull]
        [field: SerializeField, HideInInspector]
        protected ActionCooldown CooldownTimer { get; set; }

        /// <summary>
        ///     If <see cref="ActionBase.PerformExecution"/> returns one of these states, cooldown will start.
        ///     By default, only successful or interrupted actions will trigger cooldown.
        /// </summary>
        [field: SerializeField, HideInInspector]
        protected ActionExecutionState ExecutionStatesCausingCooldownToStart { get; set; }
            = ActionExecutionState.Success | ActionExecutionState.Interrupted;

        protected ActionBaseWithCooldown()
        {
            Initialize();
            CooldownTimer = new ActionCooldown(this, 0f);
        }

        /// <summary>
        ///     Default cooldown time for the action set at
        ///     first initialization.
        /// </summary>
        [Unit(Units.Second)] [ShowInInspector] [ReadOnly] [TitleGroup("Cooldown")]
        protected abstract float DefaultCooldownTime { get; }

        private void Initialize()
        {
            // Update timer total time, as virtual members are not called properly in constructor.
            // And we want timer to be annotated with [NotNull] attribute.
            CooldownTimer.SetTotalTime(DefaultCooldownTime);
        }

        /// <summary>
        ///     Total cooldown time for the action (set after each execution).
        ///     Can be changed at runtime. Can be overriden to provide custom value.
        /// </summary>
        // ReSharper disable NullableWarningSuppressionIsUsed
        [ShowInInspector] [TitleGroup("Cooldown")] [Unit(Units.Second)] public virtual double CooldownTime
        {
            get => (float) CooldownTimer.TotalTime;
            set => CooldownTimer.SetTotalTime(value);
        }

        /// <summary>
        ///     Cooldown left for the action, returns 0 if cooldown has ended and
        ///     current cooldown time when action is on cooldown (timer is enabled).
        /// </summary>
        [Unit(Units.Second)]
        [PropertyTooltip(COOLDOWN_TO_STRING)]
        [ShowInInspector]
        [ProgressBar(0, nameof(CooldownTime))]
        public float CooldownLeft
            => CooldownTimer!.Enabled ? (float) CooldownTimer.RemainingTime : 0f;
        // ReSharper restore NullableWarningSuppressionIsUsed
        
        protected internal override async UniTask<ActionExecutionState> TryPerformExecution()
        {
            return CooldownTimer.Enabled
                ? ActionExecutionState.OnCooldown
                : await PerformExecution();
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
            internal ActionCooldown([NotNull] ActionBaseWithCooldown action) : this(action, 0)
            {
            }

            public ActionCooldown([NotNull] ActionBaseWithCooldown action, float totalTimeSeconds) : base(
                totalTimeSeconds)
            {
                OwnerReference = action;
            }

            public ActionCooldown([NotNull] ActionBaseWithCooldown action, TimeSpan totalTime) : base(totalTime)
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
            private ActionBaseWithCooldown OwnerReference { get; set; }

            protected override bool ResetTimeToFull => false;
            public override bool RestartOnElapsed => false;
            public override bool DisposeOnElapsed => false;

            protected override async UniTask OnCompleted()
            {
                // Call base method.
                await OwnerReference.OnCooldownComplete();
            }
        }

        /// <summary>
        /// Instantly finish this action calling all necessary methods.
        /// </summary>
        public UniTask EndCooldown()
        {
            // Check if action is on cooldown.
            if(CooldownLeft <= 0) return UniTask.CompletedTask;
            
            // Finish cooldown timer.
            CooldownTimer?.Finish();
            return UniTask.CompletedTask;
        }

#if UNITY_EDITOR
        [ShowInInspector] [Button(nameof(Execute), ButtonSizes.Medium)] private void ExecuteInEditor()
        {
            Execute().Forget();
        }

        [ShowInInspector] [ShowIf(nameof(HasCooldown))] [Button(nameof(ActionCooldown.Reset), ButtonSizes.Medium)]
        private void ResetCooldownInEditor()
        {
            EndCooldown().Forget();
        }
#endif
    }
}