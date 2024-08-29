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
        public static bool SupportsLocalEconomy(this object obj) => obj is ILocalEconomy;

        /// <summary>
        /// Checks if object has local resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public static bool HasLocalResource<TResource, TNumberType>(this ILocalEconomy localEconomy)
            where TResource : ResourceBase<TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            return localEconomy.HasResource<TResource, TNumberType>();
        }

        /// <summary>
        /// Gets local resource from object if object supports local economy.
        /// </summary>
        public static bool TryGetLocalResource<TResource, TNumberType>([NotNull] this ILocalEconomy localEconomy,
            [CanBeNull] out TResource resource)
            where TResource : ResourceBase<TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            return localEconomy.TryGetResource<TResource, TNumberType>(out resource);
        }
    }
}