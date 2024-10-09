using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Context;
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
            where TResource : ILocalResource, new() =>
            withLocalEconomy.HasResource<TResource>();

        /// <summary>
        /// Gets local resource from object if object supports local economy.
        /// </summary>
        public static bool TryGetLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy,
            [CanBeNull] out TResource resource)
            where TResource : ILocalResource, new() =>
            withLocalEconomy.TryGetResource(out resource);

        /// <summary>
        /// Adds local resource of type <typeparamref name="TResource"/> with the specified context.
        /// </summary>
        public static void AddLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy,
            IAddResourceContext resourceContext)
            where TResource : ILocalResource, IResource
        {
            resourceContext.Economy = withLocalEconomy;
            withLocalEconomy.AddResource<TResource>(resourceContext);
        }

        /// <summary>
        /// Adds local resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void AddLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy, int32 amount)
            where TResource : ILocalResource, IResource =>
            withLocalEconomy.AddResource<TResource>(new GenericAddResourceContext(withLocalEconomy, amount));

        /// <summary>
        /// Takes local resource of type <typeparamref name="TResource"/> with the specified context.
        /// </summary>
        public static void TakeLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy,
            ITakeResourceContext resourceContext)
            where TResource : ILocalResource, IResource
        {
            resourceContext.Economy = withLocalEconomy;
            withLocalEconomy.TakeResource<TResource>(resourceContext);
        }

        /// <summary>
        /// Takes local resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void TakeLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy, int32 amount)
            where TResource : ILocalResource, IResource =>
            withLocalEconomy.TakeResource<TResource>(new GenericTakeResourceContext(withLocalEconomy, amount));

        /// <summary>
        /// Tries to take local resource of type <typeparamref name="TResource"/> with the specified context.
        /// </summary>
        public static bool TryTakeLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy,
            ITakeResourceContext resourceContext)
            where TResource : ILocalResource, IResource
        {
            resourceContext.Economy = withLocalEconomy;
            return withLocalEconomy.TryTakeResource<TResource>(resourceContext);
        }

        /// <summary>
        /// Tries to take local resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static bool TryTakeLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy,
            int32 amount)
            where TResource : ILocalResource, IResource =>
            withLocalEconomy.TryTakeResource<TResource>(new GenericTakeResourceContext(withLocalEconomy, amount));

        /// <summary>
        /// Sets local resource of type <typeparamref name="TResource"/> with the specified context.
        /// </summary>
        public static void SetLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy,
            IModifyResourceContext resourceContext)
            where TResource : ILocalResource, IResource
        {
            resourceContext.Economy = withLocalEconomy;
            withLocalEconomy.SetResource<TResource>(resourceContext);
        }

        /// <summary>
        /// Sets local resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void SetLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy, int32 amount)
            where TResource : ILocalResource, IResource =>
            withLocalEconomy.SetResource<TResource>(new GenericModifyResourceContext(withLocalEconomy, amount));

        /// <summary>
        /// Checks if object has enough local resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public static bool HasEnoughLocalResource<TResource>(
            [NotNull] this IWithLocalEconomy withLocalEconomy,
            ICompareResourceContext resourceContext)
            where TResource : ILocalResource, IResource
        {
            resourceContext.Economy = withLocalEconomy;
            return withLocalEconomy.HasEnoughResource<TResource>(resourceContext);
        }

        /// <summary>
        /// Checks if object has enough local resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public static bool HasEnoughLocalResource<TResource>([NotNull] this IWithLocalEconomy withLocalEconomy,
            int32 amount)
            where TResource : ILocalResource, IResource =>
            withLocalEconomy.HasEnoughResource<TResource>(
                new GenericCompareResourceContext(withLocalEconomy, amount));
    }
}