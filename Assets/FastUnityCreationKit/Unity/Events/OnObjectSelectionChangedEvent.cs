using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events
{
    /// <summary>
    /// Represents an event that is triggered when an object selection has changed.
    /// </summary>
    public sealed class OnObjectSelectionChangedEvent<TFastMonoBehaviour> : 
        GlobalEventChannel<OnObjectSelectionChangedEvent<TFastMonoBehaviour>, FastMonoBehaviourPointerEventData<TFastMonoBehaviour>>
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        
    }
}