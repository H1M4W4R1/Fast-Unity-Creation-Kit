using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events
{
    /// <summary>
    /// Event channel that is triggered when a FastMonoBehaviour is destroyed.
    /// </summary>
    public class OnObjectDestroyedEvent<TFastCreationKitObject> :
        GlobalEventChannel<OnObjectDestroyedEvent<TFastCreationKitObject>, FastMonoBehaviourDestroyedData<TFastCreationKitObject>>
        where TFastCreationKitObject : FastMonoBehaviour, new()
    {
        
    }
}