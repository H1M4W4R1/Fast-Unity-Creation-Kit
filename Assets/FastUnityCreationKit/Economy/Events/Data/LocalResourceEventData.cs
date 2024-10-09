using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Core.Numerics;
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
        [NotNull] public readonly IModifyResourceContext context;

        /// <summary>
        /// Amount of the resource that was added or taken.
        /// </summary>
        public int32 Amount => context.Amount;

        /// <summary>
        /// Local economy context of the resource change event.
        /// </summary>
        [CanBeNull] public IWithLocalEconomy LocalEconomy => context.Economy;

        public LocalResourceEventData([NotNull] IModifyResourceContext context)
        {
            this.context = context;
        }
    }
}