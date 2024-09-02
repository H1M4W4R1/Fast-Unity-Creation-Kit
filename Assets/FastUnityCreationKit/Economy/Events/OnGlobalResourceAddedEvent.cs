using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Events.Data;

namespace FastUnityCreationKit.Economy.Events
{
    /// <summary>
    /// Event triggered when a global resource has been added.
    /// </summary>
    public sealed class OnGlobalResourceAddedEvent<TResource> : 
        GlobalEventChannel<OnGlobalResourceAddedEvent<TResource>, GlobalResourceEventData<TResource>>
        where TResource : IGlobalResource, new()
    {
        
    }
}