using FastUnityCreationKit.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    /// This event is invoked after object was created and initialized.
    /// </summary>
    public sealed class OnObjectCreatedEvent<TFastCreationKitObject> :
        GlobalEventChannel<OnObjectDestroyedEvent<TFastCreationKitObject>, FastMonoBehaviourEventData<TFastCreationKitObject>>
        where TFastCreationKitObject : FastMonoBehaviour, new()
    {
        
    }
}