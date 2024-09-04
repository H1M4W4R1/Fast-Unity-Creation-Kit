using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Abstract
{
    /// <summary>
    /// Represents that object supports local resource.
    /// Automatically supports local economy.
    /// </summary>
    public interface IWithWithLocalResource<out TResource> : IWithLocalResource, IWithLocalEconomy
        where TResource : ILocalResource
    {
        /// <summary>
        /// Storage for resource. 
        /// </summary>
        /// <remarks>
        /// Always assign to new instance of <see cref="ResourceBase{TSelf, TNumberType}"/>.
        /// </remarks>
        [NotNull] public TResource ResourceStorage { get; }

        TLocalResource IWithLocalResource.GetResourceAs<TLocalResource>()
        {
            if(ResourceStorage is TLocalResource localResource)
                return localResource;
            
            return default;
        }
    }
    
    public interface IWithLocalResource
    {
        [CanBeNull] public TResource GetResourceAs<TResource>() 
            where TResource : ILocalResource, new();
    }
}