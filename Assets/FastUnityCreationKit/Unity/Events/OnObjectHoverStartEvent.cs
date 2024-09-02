using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Unity.Events.Data;
using FastUnityCreationKit.Unity.Interfaces;

namespace FastUnityCreationKit.Unity.Events
{
    /// <summary>
    /// Represents an event that is triggered when an object is hovered.
    /// </summary>
    public sealed class OnObjectHoverStartEvent<TFastMonoBehaviour> : GlobalEventChannel<OnObjectHoverStartEvent<TFastMonoBehaviour>, FastMonoBehaviourPointerEventData<TFastMonoBehaviour>>
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {

    }
}