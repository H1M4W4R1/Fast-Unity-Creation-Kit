using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events.Input
{
    /// <summary>
    /// Represents an event that is triggered when an object selection has changed.
    /// </summary>
    public sealed class OnObjectSelectionChangedEvent<TFastMonoBehaviour> : 
        GlobalEventChannel<OnObjectSelectionChangedEvent<TFastMonoBehaviour>, FastMonoBehaviourSelectionPointerEventData<TFastMonoBehaviour>>
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        
    }
}