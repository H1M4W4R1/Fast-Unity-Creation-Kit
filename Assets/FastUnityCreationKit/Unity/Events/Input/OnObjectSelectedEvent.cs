using FastUnityCreationKit.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events.Input
{
    /// <summary>
    /// Represents an event that is triggered when an object is selected.
    /// </summary>
    public sealed class OnObjectSelectedEvent<TFastMonoBehaviour> : GlobalEventChannel<OnObjectSelectedEvent<TFastMonoBehaviour>, FastMonoBehaviourSelectionPointerEventData<TFastMonoBehaviour>>
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        
    }
}