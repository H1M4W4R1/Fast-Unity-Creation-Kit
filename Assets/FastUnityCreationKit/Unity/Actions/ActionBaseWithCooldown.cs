using System;

namespace FastUnityCreationKit.Unity.Actions
{
    /// <summary>
    /// Action with cooldown.
    /// </summary>
    public abstract class ActionBaseWithCooldown : ActionBase
    {
        /// <summary>
        /// Default cooldown time for the action set at
        /// first initialization.
        /// </summary>
        protected abstract float DefaultCooldownTime { get; }
        
        /// <summary>
        /// Cooldown time for the action.
        /// </summary>
        public float CooldownTime
        {
            get => CooldownTimer.TotalTime.Seconds;
            set => CooldownTimer.SetTotalTime(TimeSpan.FromSeconds(value));
        }
        
        protected ActionBaseWithCooldown() => Initialize();
        
        private void Initialize()
        {
            // Create timer and assign it to the action.
            CooldownTimer = new ActionCooldown(this, DefaultCooldownTime);
        }
    }
}