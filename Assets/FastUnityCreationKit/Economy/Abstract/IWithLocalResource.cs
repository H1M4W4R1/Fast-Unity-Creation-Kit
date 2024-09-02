using FastUnityCreationKit.Core.Numerics.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Abstract
{
    /// <summary>
    /// Represents that object supports local resource.
    /// Automatically supports local economy.
    /// </summary>
    public interface IWithLocalResource<out TResource> : IWithLocalResource, ILocalEconomy
        where TResource : ILocalResource
    {
        /// <summary>
        /// Storage for resource. 
        /// </summary>
        /// <remarks>
        /// Always assign to new instance of <see cref="ResourceBase{TSelf, TNumberType}"/>.
        /// </remarks>
        [NotNull] public TResource ResourceStorage { get; }
    }
    
    public interface IWithLocalResource
    {
        
    }
}