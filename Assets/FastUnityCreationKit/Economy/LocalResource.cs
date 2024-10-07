using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Context;
using FastUnityCreationKit.Economy.Events;
using FastUnityCreationKit.Economy.Events.Data;

namespace FastUnityCreationKit.Economy
{
    /// <summary>
    /// Local resources are resources that are available at the local (object) level.
    /// This can be for example an entity health.
    /// <br/><br/>
    /// For more information, see <see cref="ResourceBase{TSelf}"/>.
    /// </summary>
    public abstract class LocalResource<TSelf> : ResourceBase<TSelf>, ILocalResource
        where TSelf : LocalResource<TSelf>
    {
        internal override void OnResourceAdded(IModifyResourceContext context) =>
            OnLocalResourceAddedEvent<TSelf>.TriggerEvent(
                new LocalResourceEventData<TSelf>(context));

        internal override void OnResourceTaken(IModifyResourceContext context) =>
            OnLocalResourceTakenEvent<TSelf>.TriggerEvent(
                new LocalResourceEventData<TSelf>(context));

        internal override void OnResourceChanged(IModifyResourceContext context) =>
            OnLocalResourceChangedEvent<TSelf>.TriggerEvent(
                new LocalResourceEventData<TSelf>(context));
    }
}