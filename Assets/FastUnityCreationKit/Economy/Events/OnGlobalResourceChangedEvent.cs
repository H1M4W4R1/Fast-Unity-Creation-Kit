using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Events.Data;

namespace FastUnityCreationKit.Economy.Events
{
    /// <summary>
    /// Event called when a global resource has been changed.
    /// </summary>
    public sealed class OnGlobalResourceChangedEvent<TResource> : 
        GlobalEventChannel<OnGlobalResourceChangedEvent<TResource>, GlobalResourceEventData<TResource>>
        where TResource : IGlobalResource, new()
    {
        
    }
}