using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Context;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Events.Data
{
    /// <summary>
    /// Data for events related to local resources.
    /// </summary>
    public readonly struct LocalResourceEventData<TResource> : IEventChannelData
        where TResource : ILocalResource
    {
        [CanBeNull] public readonly IModifyResourceContext context;
        
        public LocalResourceEventData([CanBeNull] IModifyResourceContext context)
        {
            this.context = context;
        }
    }
}