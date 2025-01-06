using FastUnityCreationKit.Events;
using FastUnityCreationKit.Input.Events.Data;

namespace FastUnityCreationKit.Input.Events
{
    /// <summary>
    ///     Event called whenever a binding change is started on an input action.
    /// </summary>
    public sealed class OnBindingChangeStartedGlobalEvent : GlobalEventChannel<OnBindingChangeStartedGlobalEvent, BindingChangeData>
    {
        
    }
}