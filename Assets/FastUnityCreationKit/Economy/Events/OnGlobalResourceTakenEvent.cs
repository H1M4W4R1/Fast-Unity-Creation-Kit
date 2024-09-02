using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Events.Data;

namespace FastUnityCreationKit.Economy.Events
{
    /// <summary>
    /// Event triggered when a global resource has been taken.
    /// </summary>
    public sealed class OnGlobalResourceTakenEvent<TResource> :
        GlobalEventChannel<OnGlobalResourceTakenEvent<TResource>, GlobalResourceEventData<TResource>>
        where TResource : IGlobalResource, new()
    {
        
    }
}