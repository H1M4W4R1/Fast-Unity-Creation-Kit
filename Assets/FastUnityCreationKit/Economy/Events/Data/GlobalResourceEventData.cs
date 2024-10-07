using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Context;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Events.Data
{
    /// <summary>
    /// Represents the data of the global resource event.
    /// </summary>
    public readonly struct GlobalResourceEventData<TGlobalResource> : IEventChannelData
        where TGlobalResource : IGlobalResource, new()
    {
        [CanBeNull] public readonly IModifyResourceContext context;

        public GlobalResourceEventData([CanBeNull] IModifyResourceContext context)
        {
            this.context = context;
        }
    }
}