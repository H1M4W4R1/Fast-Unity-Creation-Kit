using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Events.Data;

namespace FastUnityCreationKit.Economy.Events
{
    /// <summary>
    /// This event is triggered when a local resource is changed.
    /// </summary>
    /// <typeparam name="TLocalResource"></typeparam>
    public sealed class OnLocalResourceChangedEvent<TLocalResource> : 
        GlobalEventChannel<OnLocalResourceChangedEvent<TLocalResource>, LocalResourceEventData<TLocalResource>>
        where TLocalResource : ILocalResource
    {
        
    }
}