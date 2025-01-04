using FastUnityCreationKit.Unity.Time.Enums;

namespace FastUnityCreationKit.Examples._04_FastMonoBehaviour_UpdateModes.Scripts
{
    /// <summary>
    /// You can ignore paused time by using UpdateWhenTimePaused mode.
    /// This mode is useful when you want to update your object even when the time is paused.
    /// Like UI elements that should be updated even when the game is paused, because
    /// UI should remain interactive.
    /// </summary>
    public sealed class CkMonoBehaviourUpdateWhenTimeIsPaused : CkMonoBehaviourExampleBase
    {
        public override UpdateMode UpdateMode => UpdateMode.UpdateWhenTimePaused;
    }
}