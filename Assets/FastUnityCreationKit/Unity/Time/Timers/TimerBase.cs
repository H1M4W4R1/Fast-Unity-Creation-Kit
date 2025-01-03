using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Events;
using FastUnityCreationKit.Unity.Time.Enums;
using FastUnityCreationKit.Utility.Logging;
using Sirenix.Serialization;

namespace FastUnityCreationKit.Unity.Time.Timers
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
        [OdinSerialize]
        private TimeSpan _remainingTime;

        /// <summary>
        /// Enable or disable the timer.
        /// </summary>
        [OdinSerialize]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// The time the timer is used to count down.
        /// </summary>
        [OdinSerialize]
        public TimeSpan TotalTime { get; protected set; }

        /// <summary>
        /// Time scale of the timer, also takes into account the global time scale
        /// when updating the timer - when global time scale is 0.5f and timer time scale
        /// is 0.5f the timer will be updated at 0.25f (if it takes global time scale into account).
        /// </summary>
        [OdinSerialize]
        public float TimeScale { get; set; } = 1f;

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
        public abstract bool RestartOnElapsed { get; }

        /// <summary>
        /// Check if the timer should be disposed when it finishes.
        /// </summary>
        public abstract bool DisposeOnElapsed { get; }

        /// <summary>
        /// The time remaining until the timer finishes.
        /// </summary>
        public TimeSpan RemainingTime => _remainingTime;

        /// <summary>
        /// True if the timer has finished.
        /// </summary>
        public bool HasFinished => _remainingTime.Ticks <= 0;

        /// <summary>
        /// Used to add time to the timer (used to slow down the timer).
        /// </summary>
        public async UniTask AddTime(TimeSpan time)
        {
            _remainingTime += time;
            await OnTimePassed((float) -time.TotalSeconds);
        }

        /// <summary>
        /// Subtract time from the timer (used to speed up the timer).
        /// </summary>
        public async UniTask SubtractTime(TimeSpan time)
        {
            _remainingTime -= time;
            await OnTimePassed((float) time.TotalSeconds);
        }

        public async UniTask Advance(TimeSpan time) => await SubtractTime(time);
        public async UniTask Advance(float time) => await SubtractTime(TimeSpan.FromSeconds(time));

        /// <summary>
        /// Finish current timer cycle with the next frame update.
        /// This is implemented as auto-set remaining time to 0.
        /// </summary>
        /// <remarks>
        /// This does not finish timer by itself, but waits for the next frame update
        /// to check if the timer has finished. <br/><br/>
        /// Due to this behaviour it's possible to Finish() timer that is disabled,
        /// and it will wait until enabled to finish the cycle (unless is configured
        /// to work when disabled or disposed).
        /// </remarks>
        public async UniTask Finish() => await SetRemainingTime(TimeSpan.Zero);
        
        /// <summary>
        /// Set the remaining time of the timer.
        /// </summary>
        public async UniTask SetRemainingTime(TimeSpan time)
        {
            // Compute the difference between the new time and the old time.
            // We do it in inverted fashion to handle it in OnTimePassed.
            //
            // When time is larger than the remaining time, the difference will be negative.
            // So the time passed will be negative, which is correct.
            TimeSpan difference = _remainingTime - time;
            
            // Set the new time.
            _remainingTime = time;
            
            // Trigger events for the difference.
            await OnTimePassed((float) difference.TotalSeconds);
        }

        /// <summary>
        /// Set the total time of the timer (used to reset the timer).
        /// Does not affect the remaining time of current cycle.
        /// </summary>
        public void SetTotalTime(TimeSpan time) => TotalTime = time;

        /// <summary>
        /// Reset the timer to its initial state.
        /// </summary>
        public void Reset()
        {
            _remainingTime = TotalTime;
            OnReset();
        }

        /// <summary>
        /// Start the timer.
        /// </summary>
        public void Start()
        {
            Enabled = true;
            OnStarted();
        }

        /// <summary>
        /// Stop the timer.
        /// </summary>
        public void Stop()
        {
            Enabled = false;
            OnStopped();
        }

        /// <summary>
        /// Restart the timer.
        /// </summary>
        public void Restart()
        {
            Reset();
            Start();
        }

        /// <summary>
        /// Configure this timer with the total time specified in seconds.
        /// </summary>
        protected TimerBase(float totalTimeSeconds) : this(TimeSpan.FromSeconds(totalTimeSeconds))
        {
        }

        /// <summary>
        /// Configure this timer
        /// </summary>
        protected TimerBase(TimeSpan totalTime)
        {
            TotalTime = totalTime;
            _remainingTime = totalTime;

            OnFrameRenderedEvent.RegisterEventListener(OnFrameRenderedHandler);
            CheckConfiguration();
        }

        private void CheckConfiguration()
        {
            if (DisposeOnElapsed && RestartOnElapsed)
                Guard<ValidationLogConfig>.Error("Timer cannot be disposed and reset at the same time.");

            OnSetup();
        }

        private async UniTask OnFrameRenderedHandler()
        {
            // Check if update is forbidden
            if ((UpdateMode & UpdateMode.Forbidden) != 0) return;

            // Check if timer is enabled, if not and update mode does not allow it to
            // be updated when disabled, return.
            if (!Enabled && (UpdateMode & UpdateMode.UpdateWhenDisabled) == 0) return;

            // Check if time is paused, if not and update mode does not allow it to
            // be updated when time is paused, return.
            if (TimeAPI.IsTimePaused && (UpdateMode & UpdateMode.UpdateWhenTimePaused) == 0) return;

            // Reduce timer time based on delta time
            // provided by time configuration.
            float deltaTime = GetDeltaTime() * TimeScale;
            _remainingTime -= TimeSpan.FromSeconds(deltaTime);
            await OnTimePassed(deltaTime);

            // Check if timer has finished and act accordingly.
            if (HasFinished)
            {
                await OnCompleted();

                // Prevent further updates.
                Stop();

                if (RestartOnElapsed) Restart(); // also automatically starts the timer again.
                else if (DisposeOnElapsed) Dispose();
            }
        }

        /// <summary>
        /// Called when timer has elapsed.
        /// </summary>
        protected virtual UniTask OnCompleted() => UniTask.CompletedTask;

        /// <summary>
        /// Called when timer has started.
        /// </summary>
        protected virtual UniTask OnStarted() => UniTask.CompletedTask;

        /// <summary>
        /// Called when timer has stopped.
        /// </summary>
        protected virtual UniTask OnStopped() => UniTask.CompletedTask;

        /// <summary>
        /// Called when time is reduced.
        /// </summary>
        protected virtual UniTask OnTimePassed(float deltaTime) => UniTask.CompletedTask;

        /// <summary>
        /// Called when timer is reset.
        /// </summary>
        protected virtual UniTask OnReset() => UniTask.CompletedTask;

        /// <summary>
        /// Called when timer is configured for the first time.
        /// </summary>
        protected virtual UniTask OnSetup() => UniTask.CompletedTask;

        /// <summary>
        /// Get delta time based on time configuration.
        /// </summary>
        private float GetDeltaTime()
        {
            return UpdateTime switch
            {
                UpdateTime.DeltaTime => TimeAPI.DeltaTime,
                UpdateTime.UnscaledDeltaTime => TimeAPI.UnscaledDeltaTime,
                UpdateTime.RealtimeSinceStartup => throw new NotSupportedException(
                    "RealtimeSinceStartup is not supported for timers."),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <summary>
        /// Checks if the timer should be destroyed.
        /// </summary>
        public bool ShouldBeDestroyed() => HasFinished;

        public override string ToString() => _remainingTime.ToString(TimeFormat);

        public void Dispose()
        {
            OnFrameRenderedEvent.UnregisterEventListener(OnFrameRenderedHandler);
        }
    }
}