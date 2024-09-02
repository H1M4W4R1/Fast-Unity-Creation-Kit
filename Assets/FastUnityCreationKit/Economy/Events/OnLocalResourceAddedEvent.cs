using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Events.Data;

namespace FastUnityCreationKit.Economy.Events
{
    /// <summary>
    /// This event is triggered when a local resource is added.
    /// </summary>
    public sealed class OnLocalResourceAddedEvent<TLocalResource> : 
        GlobalEventChannel<OnLocalResourceAddedEvent<TLocalResource>, LocalResourceEventData<TLocalResource>>
        where TLocalResource : ILocalResource
    {
        
    }
}