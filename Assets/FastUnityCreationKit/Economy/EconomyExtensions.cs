using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy
{
    public static class Extensions
    {
        
        /// <summary>
        /// Returns true if object supports local economy.
        /// </summary>
        public static bool SupportsLocalEconomy(this object obj) => obj is IWithLocalEconomy;

        /// <summary>
        /// Checks if object has local resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public static bool HasLocalResource<TResource>(this IWithLocalEconomy withLocalEconomy)
            where TResource : ILocalResource, new()
        {
            return withLocalEconomy.HasResource<TResource>();
        }

        /// <summary>
        /// Gets local resource from object if object supports local economy.
        /// </summary>
        public static bool TryGetLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy,
            [CanBeNull] out TResource resource)
            where TResource : ILocalResource, new()
        {
            return withLocalEconomy.TryGetResource<TResource>(out resource);
        }
    }
}