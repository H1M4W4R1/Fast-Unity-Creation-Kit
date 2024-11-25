using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events.Input
{
    /// <summary>
    /// Represents an event that is triggered when an object is double-clicked.
    /// </summary>
    public sealed class OnObjectDoubleClickedEvent<TFastMonoBehaviour> : GlobalEventChannel<OnObjectDoubleClickedEvent<TFastMonoBehaviour>, FastMonoBehaviourPointerEventData<TFastMonoBehaviour>>
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        
    }
}