using System;
using Sirenix.OdinInspector;

namespace FastUnityCreationKit.Unity.Actions
{
    /// <summary>
    /// Action with cooldown.
    /// </summary>
    public abstract class ActionBaseWithCooldown : ActionBase
    {
        private const string COOLDOWN_TO_STRING =
            "@" + nameof(CooldownTimer) + "." + nameof(ActionCooldown.ToString) + "()";

        /// <summary>
        /// Default cooldown time for the action set at
        /// first initialization.
        /// </summary>
        [Unit(Units.Second)]
        [ShowInInspector]
        [ReadOnly]
        [TitleGroup("Cooldown")]
        protected abstract float DefaultCooldownTime { get; }

        /// <summary>
        /// Total cooldown time for the action (set after each execution).
        /// Can be changed at runtime. Can be overriden to provide custom value.
        /// </summary>
        [ShowInInspector]
        [TitleGroup("Cooldown")]
        [Unit(Units.Second)]
        public virtual float CooldownTime
        {
            get => (float) CooldownTimer!.TotalTime.TotalSeconds;
            set => CooldownTimer!.SetTotalTime(TimeSpan.FromSeconds(value));
        }

        /// <summary>
        /// Cooldown left for the action, returns 0 if cooldown has ended and
        /// current cooldown time when action is on cooldown (timer is enabled).
        /// </summary>
        [Unit(Units.Second)]
        [PropertyTooltip(COOLDOWN_TO_STRING)]
        [ShowInInspector]
        [ProgressBar(0, nameof(CooldownTime))]
        public float CooldownLeft => CooldownTimer!.Enabled ? (float) CooldownTimer.RemainingTime.TotalSeconds : 0f;

        protected ActionBaseWithCooldown() => Initialize();

        private void Initialize()
        {
            // Create timer and assign it to the action.
            CooldownTimer = new ActionCooldown(this, DefaultCooldownTime);
        }
    }
}