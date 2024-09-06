using FastUnityCreationKit.Core.Utility.Singleton;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Abstract
{
    /// <summary>
    /// Represents a global resource.
    /// </summary>
    public interface IGlobalResource<out TSelf> : IGlobalResource
        where TSelf : IGlobalResource<TSelf>
    {
    }

    /// <summary>
    /// Internal interface for global resources.
    /// </summary>
    public interface IGlobalResource
    {
        /// <summary>
        /// Used to acquire reference of specific resource
        /// </summary>
        [CanBeNull] internal static TResource GetGlobalResourceReference<TResource>()
            where TResource : IGlobalResource, ISingleton<TResource>, new()
        {
            return ISingleton<TResource>.GetInstance();
        }
    }
}