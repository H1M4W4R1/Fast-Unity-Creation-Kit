using FastUnityCreationKit.Events;
using FastUnityCreationKit.Input.Events.Data;

namespace FastUnityCreationKit.Input.Events
{
    /// <summary>
    ///     Called whenever a binding is changed on an input action.
    /// </summary>
    public sealed class OnBindingChangedGlobalEvent : GlobalEventChannel<OnBindingChangedGlobalEvent, BindingChangeData>
    {
        
    }
}