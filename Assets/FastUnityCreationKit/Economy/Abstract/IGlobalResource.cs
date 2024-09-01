using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Abstract
{
    /// <summary>
    /// Represents a global resource.
    /// </summary>
    public interface IGlobalResource<out TSelf> : IGlobalResource
        where TSelf : IGlobalResource<TSelf>
    {
        /// <summary>
        /// Get a global reference to the resource from instance point of view.
        /// </summary>
        [NotNull] public TSelf GetGlobalReference();

        ///<inheritdoc/>
        TResource IGlobalResource.GetGlobalResourceReference<TResource>()
        {
            TSelf reference = GetGlobalReference();
            if(reference is not TResource convertedResourceType) return default;
            return convertedResourceType;
        }
    }

    /// <summary>
    /// Internal interface for global resources.
    /// </summary>
    public interface IGlobalResource
    {
        /// <summary>
        /// Used to acquire reference of specific resource
        /// </summary>
        [CanBeNull] internal TResource GetGlobalResourceReference<TResource>()
            where TResource : IGlobalResource
        {
            if(this is not TResource) return default;
            return (TResource) this;
        }
    }
}