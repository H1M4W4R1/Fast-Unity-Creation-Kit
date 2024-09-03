using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Utility.Singleton;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Events;
using FastUnityCreationKit.Economy.Events.Data;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy
{
    /// <summary>
    /// Represents a global resource.
    ///
    /// Global resources are resources that are shared across different objects.
    /// By default, most resources will be global - an example of a non-global resource
    /// could be entity's health, which is unique to each entity. 
    /// </summary>
    public abstract class GlobalResource<TSelf, TNumberType> : ResourceBase<TSelf, TNumberType>, IGlobalResource<TSelf>,
        ISingleton<TSelf>
        where TSelf : GlobalResource<TSelf, TNumberType>, IGlobalResource<TSelf>, new()
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// Instance of the global resource, use this to access the resource.
        /// </summary>
        [NotNull] public static TSelf Instance => ISingleton<TSelf>.GetInstance();
        
        /// <inheritdoc />
        public TSelf GetGlobalReference() => Instance;

        internal override void OnResourceAdded(IWithLocalEconomy economyReference, float amount) =>
            OnGlobalResourceAddedEvent<TSelf>.TriggerEvent(new GlobalResourceEventData<TSelf>(amount));
        
        internal override void OnResourceTaken(IWithLocalEconomy economyReference, float amount) =>
            OnGlobalResourceTakenEvent<TSelf>.TriggerEvent(new GlobalResourceEventData<TSelf>(amount));
        
        internal override void OnResourceChanged(IWithLocalEconomy economyReference, float amount) =>
            OnGlobalResourceChangedEvent<TSelf>.TriggerEvent(new GlobalResourceEventData<TSelf>(amount));
    }
}