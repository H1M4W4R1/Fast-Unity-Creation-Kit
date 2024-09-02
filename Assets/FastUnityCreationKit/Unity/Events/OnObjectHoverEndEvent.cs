using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Unity.Events.Data;
using FastUnityCreationKit.Unity.Interfaces;

namespace FastUnityCreationKit.Unity.Events
{
    /// <summary>
    /// Represents an event that is triggered when an object hover ends.
    /// </summary>
    public sealed class OnObjectHoverEndEvent<TFastMonoBehaviour> : GlobalEventChannel<OnObjectHoverEndEvent<TFastMonoBehaviour>, FastMonoBehaviourPointerEventData<TFastMonoBehaviour>>
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        
    }
}