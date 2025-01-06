using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Utility;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Unity.Events;
using FastUnityCreationKit.Unity.Time.Enums;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.Unity.Time.Timers
{
    /// <summary>
    ///     Base class for timers.
    /// </summary>
    [Serializable] [OnlySealed]
    public abstract class TimerBase : IDisposable
    {
        /// <summary>
        ///     Enable or disable the timer.
        /// </summary>
        [ReadOnly] [TitleGroup(GROUP_INFO)] [field: SerializeField, HideInInspector] 
        public bool Enabled
        {
            get;
            private set;
        }
        
        /// <summary>
        ///     The time the timer is used to count down.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)] 
        [field: SerializeField, HideInInspector]
        public double TotalTime
        {
            get;
            protected set;
        }
        
        /// <summary>
        ///     Time scale of the timer, also takes into account the global time scale
        ///     when updating the timer - when global time scale is 0.5f and timer time scale
        ///     is 0.5f the timer will be updated at 0.25f (if it takes global time scale into account).
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)] 
        [field: SerializeField, HideInInspector]
        public float TimeScale
        {
            get;
            set;
        } = 1f;
        
        /// <summary>
        ///     The time remaining until the timer finishes.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_INFO)] [Unit(Units.Second)] 
        [field: SerializeField, HideInInspector]
        public double RemainingTime { get; private set; }
        
        /// <summary>
        ///     Configure this timer with the total time specified in seconds.
        /// </summary>
        protected TimerBase([Unit(Units.Second)] double totalTimeSeconds)
        {
            TotalTime = totalTimeSeconds;
            RemainingTime = totalTimeSeconds;
            CheckConfiguration();
        }

        /// <summary>
        ///     Configure this timer
        /// </summary>
        protected TimerBase(TimeSpan totalTime) : this(totalTime.TotalSeconds)
        {
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
        ///     True if the timer has finished.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_INFO)] public bool HasFinished
            => RemainingTime <= 0;

        public void Dispose()
        {
            OnFrameRenderedEvent.UnregisterEventListener(OnFrameRenderedHandler);
        }

        /// <summary>
        ///     Used to add time to the timer (used to slow down the timer).
        /// </summary>
        public async UniTask AddTime([Unit(Units.Second)] double time)
        {
            RemainingTime += time;
            await OnTimePassed(-time);
            await TryToFinish();
        }
        
        /// <summary>
        ///     Used to add time to the timer (used to slow down the timer).
        /// </summary>
        public async UniTask AddTime(TimeSpan time)
        {
            await AddTime(time.TotalSeconds);
        }
        
        /// <summary>
        ///     Subtract time from the timer (used to speed up the timer).
        /// </summary>
        public async UniTask SubtractTime([Unit(Units.Second)] double time)
        {
            if(time > RemainingTime) time = RemainingTime;
            
            RemainingTime -= time;
            await OnTimePassed(time);
            await TryToFinish();
        }

        /// <summary>
        ///     Subtract time from the timer (used to speed up the timer).
        /// </summary>
        public async UniTask SubtractTime(TimeSpan time)
        {
            await SubtractTime(time.TotalSeconds);
        }

        public async UniTask Advance(TimeSpan time)
        {
            await SubtractTime(time);
        }

        public async UniTask Advance([Unit(Units.Second)] double time)
        {
            await SubtractTime(time);
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
            await SetRemainingTimeSilent(TimeSpan.Zero);
        }
        
        /// <summary>
        ///     Same as <see cref="Finish"/>, but does not raise <see cref="OnTimePassed"/> event.
        /// </summary>
        public async UniTask FinishSilent()
        {
            await SetRemainingTimeSilent(TimeSpan.Zero);
        }

        /// <summary>
        ///     Set the remaining time of the timer without raising
        ///     <see cref="OnTimePassed"/> event.
        /// </summary>
        public async UniTask SetRemainingTimeSilent([Unit(Units.Second)] double time)
        {
            // Set the new time.
            RemainingTime = time;
            
            // Check if timer has finished and act accordingly.
            await TryToFinish();
        }
        
        public async UniTask SetRemainingTime([Unit(Units.Second)] double time)
        {
            // Compute the difference between the new time and the old time.
            // We do it in inverted fashion to handle it in OnTimePassed.
            //
            // When time is larger than the remaining time, the difference will be negative.
            // So the time passed will be negative, which is correct.
            double difference = RemainingTime - time;
            
            // If difference is larger than remaining time, set it to remaining time.
            if(difference > RemainingTime) difference = RemainingTime;

            // Set the new time.
            RemainingTime = time;

            // Trigger events for the difference.
            await OnTimePassed(difference);
            
            // Check if timer has finished and act accordingly.
            await TryToFinish();
        }
        
        /// <summary>
        ///     Set the remaining time of the timer without raising
        ///     <see cref="OnTimePassed"/> event.
        /// </summary>
        public async UniTask SetRemainingTimeSilent(TimeSpan time)
        {
            await SetRemainingTimeSilent(time.TotalSeconds);
        }
        
        /// <summary>
        ///     Set the remaining time of the timer.
        /// </summary>
        public async UniTask SetRemainingTime(TimeSpan time)
        {
            await SetRemainingTime(time.TotalSeconds);
        }

        /// <summary>
        ///     Set the remaining time of the timer.
        ///     Does not affect the total time of the timer.
        /// </summary>
        public void SetTotalTime([Unit(Units.Second)] double time)
        {
            SetTotalTime(TimeSpan.FromSeconds(time));
        }
        
        /// <summary>
        ///     Set the total time of the timer (used to reset the timer).
        ///     Does not affect the remaining time of current cycle.
        /// </summary>
        public void SetTotalTime(TimeSpan time)
        {
            TotalTime = time.TotalSeconds;
        }

        /// <summary>
        ///     Reset the timer to its initial state.
        /// </summary>
        public void Reset(bool toFullTime = false, bool withEvents = true)
        {
            // Stop the timer if it's enabled.
            if (Enabled) Stop(withEvents);

            // Reset the timer to full time or to zero. and trigger events.
            RemainingTime = ResetTimeToFull || toFullTime ? TotalTime : 0;
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
            double deltaTime = GetDeltaTime() * TimeScale;
            RemainingTime -= deltaTime;
            await OnTimePassed(deltaTime);
            await TryToFinish();
        }

        /// <summary>
        ///     Tries to finish the timer if it has elapsed.
        /// </summary>
        private async UniTask TryToFinish()
        {
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
        protected virtual UniTask OnTimePassed([Unit(Units.Second)]  double deltaTime)
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
            return RemainingTime.ToString(TimeFormat);
        }
    }
}