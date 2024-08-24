using FastUnityCreationKit.Core.Numerics.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Abstract
{
    /// <summary>
    /// Represents that object supports local resource.
    /// Automatically supports local economy.
    /// </summary>
    public interface ILocalResource<out TResource, TNumberType> : ILocalResource, ILocalEconomy
        where TResource : ResourceBase<TNumberType>, new()
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// Storage for resource. 
        /// </summary>
        /// <remarks>
        /// Always assign to new instance of <see cref="ResourceBase{TNumberType}"/>.
        /// </remarks>
        [NotNull] public TResource ResourceStorage { get; }
    }
    
    public interface ILocalResource
    {
        
    }
}