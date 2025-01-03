using System;
using Cysharp.Threading.Tasks;

namespace FastUnityCreationKit.Unity.Time.Timers
{
    /// <summary>
    /// One-shot timer that runs once and stops.
    /// Great for things like reloading, cooldowns, etc.
    /// Is configured as disabled timer.
    /// </summary>
    public abstract class OneShotTimerBase : TimerBase
    {
        // OneShotTimer can only be skipped by Advance method.
        private new void Start() => base.Start();
        private new void Reset() => base.Reset();
        private new void Stop() => base.Stop();

        /// <summary>
        /// Run timer once (if not already running).
        /// </summary>
        public void Run()
        {
            // Check if timer is already running.
            if (Enabled) return;
            
            // Reset the timer to default time value.
            Reset();
            
            // Start the timer.
            Start();
        }
        
        /// <summary>
        /// If timer was completed stop it.
        /// </summary>
        protected override UniTask OnCompleted()
        {
            Enabled = false;
            return UniTask.CompletedTask;
        }

        protected OneShotTimerBase(float totalTimeSeconds) : base(totalTimeSeconds)
        {
            Enabled = false;
        }
        
        protected OneShotTimerBase(TimeSpan totalTime) : base(totalTime)
        {
            Enabled = false;
        }
    }
}