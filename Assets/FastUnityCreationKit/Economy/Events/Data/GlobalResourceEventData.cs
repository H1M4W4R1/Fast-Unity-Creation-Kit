using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Economy.Events.Data
{
    /// <summary>
    /// Represents the data of the global resource event.
    /// </summary>
    public readonly struct GlobalResourceEventData<TGlobalResource> : IEventChannelData
        where TGlobalResource : IGlobalResource, new()
    {
        public readonly float amount;
        
        public GlobalResourceEventData(float amount)
        {
            this.amount = amount;
        }
    }
}