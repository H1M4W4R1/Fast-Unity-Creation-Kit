using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events
{
    /// <summary>
    /// Represents an event that is triggered when an object is initialized.
    /// It is not recommended to use this event directly as it may lead to significant code mess.
    /// </summary>
    public sealed class OnObjectInitializedEvent<TFastMonoBehaviour> : 
        GlobalEventChannel<OnObjectInitializedEvent<TFastMonoBehaviour>, FastMonoBehaviourEventData<TFastMonoBehaviour>>
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        
    }
}