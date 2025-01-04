using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

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
        [UsedImplicitly] private new void Start(bool withEvents) => base.Start(withEvents);
        [UsedImplicitly] private new void Reset(bool toFull, bool withEvents) => base.Reset(toFull, withEvents);
        [UsedImplicitly] private new void Stop(bool withEvents) => base.Stop(withEvents);

        /// <summary>
        /// Run timer once (if not already running).
        /// </summary>
        public void Run()
        {
            // Check if timer is already running.
            if (Enabled) return;
            
            // Reset the timer to default time value.
            Reset(true);
            
            // Start the timer.
            Start();
        }
        
        /// <summary>
        /// If timer was completed stop it.
        /// </summary>
        protected override UniTask OnCompleted() => UniTask.CompletedTask;

        protected OneShotTimerBase(float totalTimeSeconds) : base(totalTimeSeconds)
        {
        }

        protected OneShotTimerBase(TimeSpan totalTime) : base(totalTime)
        {
        }
    }
}