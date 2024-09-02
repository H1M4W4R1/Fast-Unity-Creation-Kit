using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Events.Data;

namespace FastUnityCreationKit.Economy.Events
{
    /// <summary>
    /// This event is triggered when a local resource is taken.
    /// </summary>
    public sealed class OnLocalResourceTakenEvent<TLocalResource> :
        GlobalEventChannel<OnLocalResourceTakenEvent<TLocalResource>, LocalResourceEventData<TLocalResource>>
        where TLocalResource : ILocalResource
    {
    }

}