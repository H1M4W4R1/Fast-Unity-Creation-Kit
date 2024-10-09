using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Core.Numerics;
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
        [NotNull] public readonly IModifyResourceContext context;

        /// <summary>
        /// Amount of the resource that was added or taken.
        /// </summary>
        public int32 Amount => context.Amount;
        
        /// <summary>
        /// Economy context of the resource change
        /// </summary>
        [CanBeNull] public IWithLocalEconomy LocalEconomy => context.Economy;

        public GlobalResourceEventData([NotNull] IModifyResourceContext context)
        {
            this.context = context;
        }
    }
}