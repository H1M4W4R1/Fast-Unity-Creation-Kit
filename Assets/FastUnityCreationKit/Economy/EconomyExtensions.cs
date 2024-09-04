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
        public static void AddLocalResource<TResource>(this IWithLocalEconomy withLocalEconomy, float amount)
            where TResource : ILocalResource, IResource
        {
            withLocalEconomy.AddResource<TResource>(amount);
        }
        
        /// <summary>
        /// Takes local resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void TakeLocalResource<TResource>(this IWithLocalEconomy withLocalEconomy, float amount)
            where TResource : ILocalResource, IResource
        {
            withLocalEconomy.TakeResource<TResource>(amount);
        }
        
        /// <summary>
        /// Sets local resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void SetLocalResource<TResource>(this IWithLocalEconomy withLocalEconomy, float amount)
            where TResource : ILocalResource, IResource
        {
            withLocalEconomy.SetResource<TResource>(amount);
        }
        
        /// <summary>
        /// Try to take local resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static bool TryTakeLocalResource<TResource>(this IWithLocalEconomy withLocalEconomy, float amount)
            where TResource : ILocalResource, IResource
        {
            return withLocalEconomy.TryTakeResource<TResource>(amount);
        }

        /// <summary>
        /// Checks if object has enough local resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public static bool HasEnoughLocalResource<TResource>(this IWithLocalEconomy withLocalEconomy, float amount)
            where TResource : ILocalResource, IResource
        {
            return withLocalEconomy.HasEnoughResource<TResource>(amount);
        }
    }
}