using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Economy
{
    /// <summary>
    /// Represents a global resource.
    ///
    /// Global resources are resources that are shared across different objects.
    /// By default, most resources will be global - an example of a non-global resource
    /// could be entity's health, which is unique to each entity. 
    /// </summary>
    public abstract class GlobalResource<TSelf, TNumberType> : ResourceBase<TNumberType>, IGlobalResource<TSelf>
        where TSelf : GlobalResource<TSelf, TNumberType>, IGlobalResource<TSelf>, new()
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// Instance of the global resource, use this to access the resource.
        /// </summary>
        public static TSelf Instance { get; } = new TSelf();

        
        /// <inheritdoc />
        public TSelf GetGlobalReference() => Instance;
    }
}