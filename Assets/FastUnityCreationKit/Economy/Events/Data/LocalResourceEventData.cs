using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Economy.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Events.Data
{
    /// <summary>
    /// Data for events related to local resources.
    /// </summary>
    public readonly struct LocalResourceEventData<TResource> : IEventChannelData
        where TResource : ILocalResource
    {
        public readonly float amount;
        
        public LocalResourceEventData(float amount)
        {
            this.amount = amount;
        }
    }
}