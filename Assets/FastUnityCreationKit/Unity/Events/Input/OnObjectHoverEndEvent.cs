using FastUnityCreationKit.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events.Input
{
    /// <summary>
    /// Represents an event that is triggered when an object hover ends.
    /// </summary>
    public sealed class OnObjectHoverEndEvent<TFastMonoBehaviour> : GlobalEventChannel<OnObjectHoverEndEvent<TFastMonoBehaviour>, FastMonoBehaviourPointerEventData<TFastMonoBehaviour>>
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        
    }
}