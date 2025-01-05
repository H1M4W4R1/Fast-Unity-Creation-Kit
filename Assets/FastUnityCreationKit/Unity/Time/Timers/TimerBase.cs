using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Unity.Events;
using FastUnityCreationKit.Unity.Time.Enums;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Time.Timers
{
    /// <summary>
    ///     Base class for timers.
    /// </summary>
    public abstract class TimerBase : IDisposable
    {
        protected const string GROUP_CONFIGURATION = "Configuration";
        protected const string GROUP_STATE = "State";

        /// <summary>
        ///     Remaining time of the timer.
        /// </summary>
        // ReSharper disable once Unity.RedundantHideInInspectorAttribute, required for Odin
        [OdinSerialize] [HideInInspector] private TimeSpan _remainingTime;

        /// <summary>
        ///     Configure this timer with the total time specified in seconds.
        /// </summary>
        protected TimerBase(float totalTimeSeconds) : this(TimeSpan.FromSeconds(totalTimeSeconds))
        {
        }

        /// <summary>
        ///     Configure this timer
        /// </summary>
        protected TimerBase(TimeSpan totalTime)
        {
            TotalTime = totalTime;
            _remainingTime = totalTime;
            CheckConfiguration();
        }

        /// <summary>
        ///     Enable or disable the timer.
        /// </summary>
        [OdinSerialize] [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_STATE)] public bool Enabled
        {
            get;
            private set;
        }

        /// <summary>
        ///     The time the timer is used to count down.
        /// </summary>
        [OdinSerialize] [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)] public TimeSpan TotalTime
        {
            get;
            protected set;
        }

        /// <summary>
        ///     Defines if time should be reset to full or to zero during reset.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)] protected virtual bool ResetTimeToFull
            => true;

        /// <summary>
        ///     Span format options for the timer.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)] [NotNull] public virtual string TimeFormat
            => @"hh\:mm\:ss";

        /// <summary>
        ///     Time scale of the timer, also takes into account the global time scale
        ///     when updating the timer - when global time scale is 0.5f and timer time scale
        ///     is 0.5f the timer will be updated at 0.25f (if it takes global time scale into account).
        /// </summary>
        [OdinSerialize] [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)] public float TimeScale
        {
            get;
            set;
        } = 1f;

        /// <summary>
        ///     By default, the timer won't update when time is paused.
        ///     And update same as MonoBehaviour.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)] public virtual UpdateMode UpdateMode
            => UpdateMode.MonoBehaviour;

        /// <summary>
        ///     By default, timer will use delta time.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)] public virtual UpdateTime UpdateTime
            => UpdateTime.DeltaTime;

        /// <summary>
        ///     If timer is temporary, it will be destroyed when it finishes.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)]
        public abstract bool RestartOnElapsed { get; }

        /// <summary>
        ///     Check if the timer should be disposed when it finishes.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)]
        public abstract bool DisposeOnElapsed { get; }

        /// <summary>
        ///     The time remaining until the timer finishes.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_STATE)] public TimeSpan RemainingTime => _remainingTime;

        /// <summary>
        ///     True if the timer has finished.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_STATE)] public bool HasFinished
            => _remainingTime.Ticks <= 0;

        public void Dispose()
        {
            OnFrameRenderedEvent.UnregisterEventListener(OnFrameRenderedHandler);
        }

        /// <summary>
        ///     Used to add time to the timer (used to slow down the timer).
        /// </summary>
        public async UniTask AddTime(TimeSpan time)
        {
            _remainingTime += time;
            await OnTimePassed((float) -time.TotalSeconds);
        }

        /// <summary>
        ///     Subtract time from the timer (used to speed up the timer).
        /// </summary>
        public async UniTask SubtractTime(TimeSpan time)
        {
            _remainingTime -= time;
            await OnTimePassed((float) time.TotalSeconds);
        }

        public async UniTask Advance(TimeSpan time)
        {
            await SubtractTime(time);
        }

        public async UniTask Advance(float time)
        {
            await SubtractTime(TimeSpan.FromSeconds(time));
        }

        /// <summary>
        ///     Finish current timer cycle with the next frame update.
        ///     This is implemented as auto-set remaining time to 0.
        /// </summary>
        /// <remarks>
        ///     This does not finish timer by itself, but waits for the next frame update
        ///     to check if the timer has finished. <br /><br />
        ///     Due to this behaviour it's possible to Finish() timer that is disabled,
        ///     and it will wait until enabled to finish the cycle (unless is configured
        ///     to work when disabled or disposed).
        /// </remarks>
        public async UniTask Finish()
        {
            await SetRemainingTime(TimeSpan.Zero);
        }

        /// <summary>
        ///     Set the remaining time of the timer.
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
        ///     Set the total time of the timer (used to reset the timer).
        ///     Does not affect the remaining time of current cycle.
        /// </summary>
        public void SetTotalTime(TimeSpan time)
        {
            TotalTime = time;
        }

        /// <summary>
        ///     Reset the timer to its initial state.
        /// </summary>
        public void Reset(bool toFullTime = false, bool withEvents = true)
        {
            // Stop the timer if it's enabled.
            if (Enabled) Stop(withEvents);

            // Reset the timer to full time or to zero. and trigger events.
            _remainingTime = ResetTimeToFull || toFullTime ? TotalTime : TimeSpan.Zero;
            if (withEvents) OnReset();
        }

        /// <summary>
        ///     Start the timer.
        /// </summary>
        public void Start(bool withEvents = true)
        {
            Enabled = true;
            OnFrameRenderedEvent.RegisterEventListener(OnFrameRenderedHandler);
            if (withEvents) OnStarted();
        }

        /// <summary>
        ///     Stop the timer.
        /// </summary>
        public void Stop(bool withEvents = true)
        {
            Enabled = false;
            OnFrameRenderedEvent.UnregisterEventListener(OnFrameRenderedHandler);
            if (withEvents) OnStopped();
        }

        /// <summary>
        ///     Restart the timer.
        /// </summary>
        public void Restart(bool withEvents = true)
        {
            Reset(false, withEvents);
            Start(withEvents);
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

                if (RestartOnElapsed)
                    Restart(); // also automatically starts the timer again.
                else if (DisposeOnElapsed) Dispose();
            }
        }

        /// <summary>
        ///     Called when timer has elapsed.
        /// </summary>
        protected virtual UniTask OnCompleted()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Called when timer has started.
        /// </summary>
        protected virtual UniTask OnStarted()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Called when timer has stopped.
        /// </summary>
        protected virtual UniTask OnStopped()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Called when time is reduced.
        /// </summary>
        protected virtual UniTask OnTimePassed(float deltaTime)
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Called when timer is reset.
        /// </summary>
        protected virtual UniTask OnReset()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Called when timer is configured for the first time.
        /// </summary>
        protected virtual UniTask OnSetup()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Get delta time based on time configuration.
        /// </summary>
        private float GetDeltaTime()
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return UpdateTime switch
            {
                UpdateTime.DeltaTime => TimeAPI.DeltaTime,
                UpdateTime.UnscaledDeltaTime => TimeAPI.UnscaledDeltaTime,
                _ => throw new NotSupportedException(
                    $"{UpdateTime} is not supported for timers.")
            };
        }

        /// <summary>
        ///     Checks if the timer should be destroyed.
        /// </summary>
        public bool ShouldBeDestroyed()
        {
            return HasFinished;
        }

        [NotNull] public override string ToString()
        {
            return _remainingTime.ToString(TimeFormat);
        }
    }
}