using FastUnityCreationKit.Events;
using FastUnityCreationKit.Input.Events.Data;

namespace FastUnityCreationKit.Input.Events
{
    /// <summary>
    ///     This event is fired when a binding change fails due to a duplicate binding.
    /// </summary>
    /// TODO: Add duplicate binding information into the event parameters to be used by the listeners.
    public sealed class OnBindingDuplicateFoundGlobalEvent :
        GlobalEventChannel<OnBindingDuplicateFoundGlobalEvent, BindingChangeData>
    {
        
    }
}