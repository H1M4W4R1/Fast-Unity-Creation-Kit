using System;

namespace FastUnityCreationKit.Unity.Time
{
    /// <summary>
    /// Defines the update mode of the FastMonoBehaviour.
    /// </summary>
    [Flags]
    public enum UpdateMode
    {
        // Regular MonoBehaviour update (only when enabled and timeScale > 0)
        MonoBehaviour = 0,
        
        // Update even when disabled
        UpdateWhenDisabled = 1,
        
        // Update even when time is paused
        // this is recommended to be set if the object is not dependent on time scale
        // as otherwise updates won't be called when time is paused.
        UpdateWhenTimePaused = 2,
        
        /// <summary>
        /// Update is forbidden, this flag overrides all other flags.
        /// If this flag is set updates will not be called.
        /// </summary>
        Forbidden = 4
    }

    /// <summary>
    /// Used to define the time used for updating the object.
    /// </summary>
    public enum UpdateTime
    {
        DeltaTime = 0,
        UnscaledDeltaTime = 1,
        RealtimeSinceStartup = 2
    }
}