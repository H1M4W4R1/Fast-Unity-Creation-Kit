using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Events;
using FastUnityCreationKit.Unity.Time.Enums;
using FastUnityCreationKit.Utility.Objects;

namespace FastUnityCreationKit.Unity.Time
{
    /// <summary>
    /// Base class for timers.
    /// </summary>
    public abstract class TimerBase : IDisposable
    {
        /// <summary>
        /// Span format options for the timer.
        /// </summary>
        public virtual string TimeFormat => @"{0:hh\:mm\:ss}";
        
        /// <summary>
        /// Remaining time of the timer.
        /// </summary>
        private TimeSpan _remainingTime;

        /// <summary>
        /// Enable or disable the timer.
        /// </summary>
        public bool Enabled { get; set; } = true;
        
        /// <summary>
        /// By default, the timer won't update when time is paused.
        /// And update same as MonoBehaviour.
        /// </summary>
        public virtual UpdateMode UpdateMode => UpdateMode.MonoBehaviour;
        
        /// <summary>
        /// By default, timer will use delta time.
        /// </summary>
        public virtual UpdateTime UpdateTime => UpdateTime.DeltaTime;
        
        /// <summary>
        /// If timer is temporary, it will be destroyed when it finishes.
        /// </summary>
        public bool ResetOnElapsed => this is not ITemporaryObject;
        
        /// <summary>
        /// The time the timer is used to count down.
        /// </summary>
        public TimeSpan TotalTime { get; protected set; }

        /// <summary>
        /// The time remaining until the timer finishes.
        /// </summary>
        public TimeSpan RemainingTime => _remainingTime;

        /// <summary>
        /// True if the timer has finished.
        /// </summary>
        public bool HasFinished => _remainingTime.Ticks <= 0;
        
        /// <summary>
        /// Time scale of the timer, also takes into account the global time scale
        /// when updating the timer - when global time scale is 0.5f and timer time scale
        /// is 0.5f the timer will be updated at 0.25f (if it takes global time scale into account).
        /// </summary>
        public float TimeScale { get; set; } = 1f;
        
        /// <summary>
        /// Used to add time to the timer (used to slow down the timer).
        /// </summary>
        public void AddTime(TimeSpan time) => _remainingTime += time;
        
        /// <summary>
        /// Subtract time from the timer (used to speed up the timer).
        /// </summary>
        public void SubtractTime(TimeSpan time) => _remainingTime -= time;
        
        public void Advance(TimeSpan time) => SubtractTime(time);
        public void Advance(float time) => SubtractTime(TimeSpan.FromSeconds(time));
        
        /// <summary>
        /// Reset the timer to its initial state.
        /// </summary>
        public void Reset() => _remainingTime = TotalTime;
        
        /// <summary>
        /// Configure this timer with the total time specified in seconds.
        /// </summary>
        protected TimerBase(float totalTimeSeconds) : this(TimeSpan.FromSeconds(totalTimeSeconds)) { }
        
        /// <summary>
        /// Configure this timer
        /// </summary>
        protected TimerBase(TimeSpan totalTime)
        {
            TotalTime = totalTime;
            _remainingTime = totalTime;
            
            OnFrameRenderedEvent.RegisterEventListener(OnFrameRenderedHandler);
        }

        private async UniTask OnFrameRenderedHandler()
        {
            // Check if update is forbidden
            if ((UpdateMode & UpdateMode.Forbidden) != 0) return;
            
            // Check if timer is enabled, if not and update mode does not allow it to
            // be updated when disabled, return.
            if(!Enabled && (UpdateMode & UpdateMode.UpdateWhenDisabled) == 0) return;
            
            // Check if time is paused, if not and update mode does not allow it to
            // be updated when time is paused, return.
            if(TimeAPI.IsTimePaused && (UpdateMode & UpdateMode.UpdateWhenTimePaused) == 0) return;
            
            // Reduce timer time based on delta time
            // provided by time configuration.
            _remainingTime -= TimeSpan.FromSeconds(GetDeltaTime() * TimeScale);

            // Check if timer has finished and act accordingly.
            if (HasFinished)
            {
                await OnCompleted();
                
                if (ResetOnElapsed) Reset();
                else Dispose();
            }
        }

        /// <summary>
        /// Called when timer has elapsed.
        /// </summary>
        protected UniTask OnCompleted() => UniTask.CompletedTask;
        
        /// <summary>
        /// Get delta time based on time configuration.
        /// </summary>
        private float GetDeltaTime()
        {
            return UpdateTime switch
            {
                UpdateTime.DeltaTime => TimeAPI.DeltaTime,
                UpdateTime.UnscaledDeltaTime => TimeAPI.UnscaledDeltaTime,
                UpdateTime.RealtimeSinceStartup => throw new NotSupportedException("RealtimeSinceStartup is not supported for timers."),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        /// <summary>
        /// Checks if the timer should be destroyed.
        /// </summary>
        public bool ShouldBeDestroyed() => HasFinished;

        public override string ToString() =>  _remainingTime.ToString(TimeFormat);

        public void Dispose()
        {
            OnFrameRenderedEvent.UnregisterEventListener(OnFrameRenderedHandler);
        }
    }
}