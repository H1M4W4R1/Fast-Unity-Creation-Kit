using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Time.Timers;
using JetBrains.Annotations;
using Sirenix.Serialization;

namespace FastUnityCreationKit.Unity.Actions
{
    /// <summary>
    /// Represents an action that can be executed
    /// for example this can be "Return to base" command in RTS or player active skills in ARPG. <br/><br/>
    /// If you need to add cooldown to the action you can simply use
    /// <see cref="ActionBaseWithCooldown"/> instead.
    /// </summary>
    /// <seealso cref="ActionBaseWithCooldown"/>
    public abstract class ActionBase
    {
        /// <summary>
        /// Timer to use for cooldown.
        /// </summary>
        [OdinSerialize]
        protected ActionCooldown CooldownTimer { get; set; }

        /// <summary>
        /// Check if action has cooldown
        /// </summary>
        protected bool HasCooldown => CooldownTimer != null;
        
        /// <summary>
        /// Action event raised when action is executed.
        /// </summary>
        protected abstract UniTask OnExecuted();
         
        /// <summary>
        /// Event raised when action execution failed due to cooldown.
        /// </summary>
        protected virtual UniTask OnExecutedDuringCooldown() => UniTask.CompletedTask;
        
        /// <summary>
        /// Event raised when action cooldown has completed.
        /// </summary>
        protected virtual UniTask OnCooldownComplete() => UniTask.CompletedTask;

        /// <summary>
        /// Execute action.
        /// </summary>
        public async UniTask Execute()
        {
            // Check if action has cooldown.
            if(HasCooldown && CooldownTimer.Enabled)
            {
                // Execute action during cooldown.
                await OnExecutedDuringCooldown();
                return;
            }
            
            // Execute action.
            await OnExecuted();
            
            // Perform cooldown start if timer is present.
            if(HasCooldown)
                CooldownTimer.Run();
        }

        /// <summary>
        /// Cooldown for actions
        /// </summary>
        public sealed class ActionCooldown : OneShotTimerBase
        {
            /// <summary>
            /// Reference to action that owns this cooldown.
            /// </summary>
            [OdinSerialize]
            private ActionBase OwnerReference { get; set; }
            public override bool RestartOnElapsed => false;
            public override bool DisposeOnElapsed => false;

            /// <summary>
            /// Base constructor for cooldown timer.
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

            protected override async UniTask OnCompleted()
            {
                // Call base method.
                await OwnerReference.OnCooldownComplete();
            }
        }
    }
}