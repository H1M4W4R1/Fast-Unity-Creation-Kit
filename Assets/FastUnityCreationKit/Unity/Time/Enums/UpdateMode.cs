using System;

namespace FastUnityCreationKit.Unity.Time.Enums
{
    /// <summary>
    ///     Defines the update mode of the <see cref="CKMonoBehaviour" />.
    /// </summary>
    [Flags] public enum UpdateMode
    {
        // Regular MonoBehaviour update (only when enabled and timeScale > 0)
        MonoBehaviour = 0,
        
        // Regular UI update mode, used to quickly set-up UI objects
        Always = UpdateWhenDisabled | UpdateWhenTimePaused,

        // Update even when disabled
        UpdateWhenDisabled = 1,

        // Update even when time is paused
        // this is recommended to be set if the object is not dependent on time scale
        // as otherwise updates won't be called when time is paused.
        UpdateWhenTimePaused = 2,

        /// <summary>
        ///     Update is forbidden, this flag overrides all other flags.
        ///     If this flag is set updates will not be called.
        /// </summary>
        Forbidden = 4
    }
}