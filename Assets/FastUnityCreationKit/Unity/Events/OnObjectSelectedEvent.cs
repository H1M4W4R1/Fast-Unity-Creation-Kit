using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events
{
    /// <summary>
    /// Represents an event that is triggered when an object is selected.
    /// </summary>
    public sealed class OnObjectSelectedEvent<TFastMonoBehaviour> : GlobalEventChannel<OnObjectSelectedEvent<TFastMonoBehaviour>, FastMonoBehaviourPointerEventData<TFastMonoBehaviour>>
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        
    }
}