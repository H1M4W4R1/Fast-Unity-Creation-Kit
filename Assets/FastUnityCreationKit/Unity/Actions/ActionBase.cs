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
    /// <seealso cref="ActionBaseWithCooldown" />
    [Serializable] [OnlySealed]
    public abstract class ActionBase
    {
        /// <summary>
        ///     Timer to use for cooldown.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_COOLDOWN)]
        [ShowIf(nameof(HasCooldown))] [CanBeNull]
        [field: SerializeField, HideInInspector]
        protected ActionCooldown CooldownTimer { get; set; }

        /// <summary>
        ///     Check if action has cooldown
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_COOLDOWN)] protected bool HasCooldown
            => CooldownTimer != null;

        /// <summary>
        ///     Action event raised when action is executed.
        /// </summary>
        protected abstract UniTask OnExecuted();

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
        ///     Execute action.
        /// </summary>
        public async UniTask Execute()
        {
            // Check if action has cooldown.
            // ReSharper disable once NullableWarningSuppressionIsUsed
            if (HasCooldown && CooldownTimer!.Enabled)
            {
                // Execute action during cooldown.
                await OnExecutedDuringCooldown();
                return;
            }

            // Execute action.
            await OnExecuted();

            // Perform cooldown start if timer is present.
            if (HasCooldown) CooldownTimer?.Run();
        }

        public UniTask Reset()
        {
            CooldownTimer?.Stop();
            CooldownTimer?.Reset();
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Cooldown for actions
        /// </summary>
        [Serializable]
        protected sealed class ActionCooldown : OneShotTimerBase
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
            [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)] [NotNull]
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

        [ShowInInspector]
        [ShowIf(nameof(HasCooldown))]
        [Button(nameof(ActionCooldown.Reset), ButtonSizes.Medium)]
        private void ResetCooldownInEditor()
        {
            Reset().Forget();
        }
#endif
    }
}