using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Context.Internal;
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
            return withLocalEconomy.TryGetResource(out resource);
        }

        public static void AddLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy, int32 amount)
            where TResource : ILocalResource, IResource
        {
            withLocalEconomy.AddResource<TResource>(new GenericAddResourceContext(withLocalEconomy, amount));
        }

        public static void TakeLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy, int32 amount)
            where TResource : ILocalResource, IResource
        {
            withLocalEconomy.TakeResource<TResource>(new GenericTakeResourceContext(withLocalEconomy, amount));
        }

        public static bool TryTakeLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy,
            int32 amount)
            where TResource : ILocalResource, IResource
        {
            return withLocalEconomy.TryTakeResource<TResource>(
                new GenericTakeResourceContext(withLocalEconomy, amount));
        }

        public static void SetLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy, int32 amount)
            where TResource : ILocalResource, IResource
        {
            withLocalEconomy.SetResource<TResource>(new GenericModifyResourceContext(withLocalEconomy, amount));
        }

        public static bool HasEnoughLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy,
            int32 amount)
            where TResource : ILocalResource, IResource
        {
            return withLocalEconomy.HasEnoughResource<TResource>(
                new GenericCompareResourceContext(withLocalEconomy, amount));
        }
    }
}