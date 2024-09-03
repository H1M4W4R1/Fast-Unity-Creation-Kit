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
        
        /// <summary>
        /// Adds local resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void AddLocalResource<TResource, TNumberType>(this IWithLocalEconomy withLocalEconomy, TNumberType amount)
            where TResource : LocalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            withLocalEconomy.AddResource<TResource, TNumberType>(amount);
        }
        
        /// <summary>
        /// Takes local resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void TakeLocalResource<TResource, TNumberType>(this IWithLocalEconomy withLocalEconomy, TNumberType amount)
            where TResource : LocalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            withLocalEconomy.TakeResource<TResource, TNumberType>(amount);
        }
        
        /// <summary>
        /// Sets local resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void SetLocalResource<TResource, TNumberType>(this IWithLocalEconomy withLocalEconomy, TNumberType amount)
            where TResource : LocalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            withLocalEconomy.SetResource<TResource, TNumberType>(amount);
        }
        
        /// <summary>
        /// Try to take local resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static bool TryTakeLocalResource<TResource, TNumberType>(this IWithLocalEconomy withLocalEconomy, TNumberType amount)
            where TResource : LocalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            return withLocalEconomy.TryTakeResource<TResource, TNumberType>(amount);
        }
        
        /// <summary>
        /// Checks if object has enough local resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public static bool HasEnoughLocalResource<TResource, TNumberType>(this IWithLocalEconomy withLocalEconomy, TNumberType amount)
            where TResource : LocalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            return withLocalEconomy.HasEnoughResource<TResource, TNumberType>(amount);
        }
    }
}