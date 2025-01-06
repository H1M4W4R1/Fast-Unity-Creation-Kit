using FastUnityCreationKit.Events;
using FastUnityCreationKit.Input.Events.Data;

namespace FastUnityCreationKit.Input.Events
{
    /// <summary>
    ///     Event called whenever a binding is reset on an input action.
    ///     Called after <see cref="OnBindingChangeCompletedGlobalEvent"/>
    /// </summary>
    public sealed class OnBindingResetGlobalEvent : GlobalEventChannel<OnBindingResetGlobalEvent, BindingChangeData>
    {
        
    }
}