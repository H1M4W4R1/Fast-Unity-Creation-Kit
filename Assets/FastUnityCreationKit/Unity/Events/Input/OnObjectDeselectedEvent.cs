using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events.Input
{
    /// <summary>
    /// Represents an event that is triggered when an object is deselected.
    /// </summary>
    public sealed class OnObjectDeselectedEvent<TFastMonoBehaviour> : GlobalEventChannel<OnObjectDeselectedEvent<TFastMonoBehaviour>, FastMonoBehaviourSelectionPointerEventData<TFastMonoBehaviour>>
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        
    }
}