using System.Collections.Generic;
using FastUnityCreationKit.Identification.Identifiers;
using FastUnityCreationKit.Unity.Time.Enums;

namespace FastUnityCreationKit.Unity.Time
{
    /// <summary>
    ///     Used to handle custom timing system.
    /// </summary>
    public static class TimeAPI
    {
        /// <summary>
        ///     Time multiplier.
        /// </summary>
        public static float TimeMultiplier { get; private set; } = 1f;

        /// <summary>
        ///     Checks if the time is paused.
        /// </summary>
        public static bool IsTimePaused => PauseObjects.Count > 0;

        /// <summary>
        ///     Current delta time, takes Unity Time.timeScale into account and
        ///     TimeAPI multiplier / pausing.
        /// </summary>
        public static float DeltaTime => IsTimePaused ? 0f : UnityEngine.Time.deltaTime * TimeMultiplier;

        /// <summary>
        ///     Unscaled delta time, same as Time.unscaledDeltaTime.
        /// </summary>
        public static float UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;

        /// <summary>
        ///     Realtime since startup, same as Time.realtimeSinceStartup.
        /// </summary>
        public static float RealtimeSinceStartup => UnityEngine.Time.realtimeSinceStartup;

        /// <summary>
        ///     Fixed delta time, same as Time.fixedDeltaTime.
        /// </summary>
        public static float FixedDeltaTime => UnityEngine.Time.fixedDeltaTime;

        /// <summary>
        ///     List of all objects that paused the time.
        /// </summary>
        private static List<PauseObject> PauseObjects { get; } = new();

        /// <summary>
        ///     Pauses the time and returns the pause object
        ///     that can be used to resume the time.
        /// </summary>
        public static void PauseTime(out PauseObject pauseObject)
        {
            pauseObject = new PauseObject(Snowflake128.New);
            PauseTimeUsing(pauseObject);
        }

        /// <summary>
        ///     Pauses the time using provided pause object (if not already paused).
        /// </summary>
        /// <returns>False if the time is already paused using provided object.</returns>
        public static bool PauseTimeUsing(in PauseObject pauseObject)
        {
            if (PauseObjects.Contains(pauseObject)) return false;

            PauseObjects.Add(pauseObject);
            return true;
        }

        /// <summary>
        ///     Resumes the time using provided pause object.
        ///     If object is not used to pause the time, nothing will happen.
        /// </summary>
        public static void ResumeTimeUsing(in PauseObject pauseObject)
        {
            PauseObjects.Remove(pauseObject);
        }

        /// <summary>
        ///     Forces time to be resumed (clears all objects from pause list).
        ///     This should be used with EXTREME caution.
        /// </summary>
        public static void ForceResumeTime()
        {
            PauseObjects.Clear();
        }

        /// <summary>
        ///     Sets the time scale.
        /// </summary>
        /// <param name="timeScale">New time scale.</param>
        public static void SetTimeScale(float timeScale)
        {
            TimeMultiplier = timeScale;
        }

        /// <summary>
        ///     Check if specified time mode ignores time scale.
        /// </summary>
        public static bool IgnoresTimeScale(this UpdateTime updateMode)
        {
            return updateMode != UpdateTime.DeltaTime;
        }

        /// <summary>
        ///     Check if specified time mode ignores pause.
        /// </summary>
        public static bool IgnoresPause(this UpdateTime updateMode)
        {
            return updateMode != UpdateTime.DeltaTime;
        }
    }
}