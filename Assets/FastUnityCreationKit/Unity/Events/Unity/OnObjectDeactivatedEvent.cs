using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    /// This event is invoked after object was disabled.
    /// </summary>
    public sealed class OnObjectDeactivatedEvent<TFastCreationKitObject> :
        GlobalEventChannel<OnObjectDestroyedEvent<TFastCreationKitObject>, FastMonoBehaviourEventData<TFastCreationKitObject>>
        where TFastCreationKitObject : FastMonoBehaviour, new()
    {
        
    }
}