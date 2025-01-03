using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Time;
using FastUnityCreationKit.Unity.Time.Enums;

namespace FastUnityCreationKit.UI.Abstract
{
    public abstract class UIBehaviour : FastMonoBehaviour
    {
#region UPDATE_CONFIGURATION

        // UI objects are always updated (even when disabled or when time is paused) and
        // they are updated using unscaled delta time - to prevent UI from being dependent on time scale.
        public override UpdateTime UpdateTimeConfig => UpdateTime.UnscaledDeltaTime;
        public override UpdateMode UpdateMode => UpdateMode.UpdateWhenDisabled | UpdateMode.UpdateWhenTimePaused;

#endregion
    }
}