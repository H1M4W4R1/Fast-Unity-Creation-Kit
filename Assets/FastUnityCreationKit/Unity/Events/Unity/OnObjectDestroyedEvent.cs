using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    /// Event channel that is triggered when a FastMonoBehaviour is destroyed.
    /// </summary>
    public sealed class OnObjectDestroyedEvent<TFastCreationKitObject> :
        GlobalEventChannel<OnObjectDestroyedEvent<TFastCreationKitObject>, FastMonoBehaviourEventData<TFastCreationKitObject>>
        where TFastCreationKitObject : FastMonoBehaviour, new()
    {
        
    }
}