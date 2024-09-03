using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Events;
using FastUnityCreationKit.Economy.Events.Data;

namespace FastUnityCreationKit.Economy
{
    /// <summary>
    /// Local resources are resources that are available at the local (object) level.
    /// This can be for example an entity health.
    /// <br/><br/>
    /// For more information, see <see cref="ResourceBase{TSelf, TNumberType}"/>.
    /// </summary>
    public abstract class LocalResource<TSelf, TNumberType> : ResourceBase<TSelf, TNumberType>, ILocalResource
        where TSelf : LocalResource<TSelf, TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        // TODO: Fix this
        internal override void OnResourceAdded(IWithLocalEconomy economyReference, float amount) =>
            OnLocalResourceAddedEvent<TSelf>.TriggerEvent(
                new LocalResourceEventData<TSelf>(economyReference, amount));

        internal override void OnResourceTaken(IWithLocalEconomy economyReference, float amount) =>
            OnLocalResourceTakenEvent<TSelf>.TriggerEvent(
                new LocalResourceEventData<TSelf>(economyReference, amount));

        internal override void OnResourceChanged(IWithLocalEconomy economyReference, float amount) =>
            OnLocalResourceChangedEvent<TSelf>.TriggerEvent(
                new LocalResourceEventData<TSelf>(economyReference, amount));
    }
}