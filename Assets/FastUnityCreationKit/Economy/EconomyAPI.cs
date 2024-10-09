using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Utility.Singleton;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Context;
using FastUnityCreationKit.Economy.Context.Internal;
using Unity.Mathematics;

namespace FastUnityCreationKit.Economy
{
    /// <summary>
    /// API functions and extensions for economy.
    /// </summary>
    public static class EconomyAPI
    {
        /// <summary>
        /// Gets global resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public static TResource GetGlobalResource<TResource>()
            where TResource : IGlobalResource, ISingleton<TResource>, new() =>
            ISingleton<TResource>.GetInstance();

        /// <summary>
        /// Adds a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void AddGlobalResource<TResource>(int32 amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new() =>
            AddGlobalResource<TResource>(new GenericAddResourceContext(null, amount));

        /// <summary>
        /// Adds a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void AddGlobalResource<TResource>(IAddResourceContext context)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new() =>
            ISingleton<TResource>.GetInstance().Add(context);

        /// <summary>
        /// Takes a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void TakeGlobalResource<TResource>(int32 amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new() =>
            TakeGlobalResource<TResource>(new GenericTakeResourceContext(null, amount));

        /// <summary>
        /// Takes a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void TakeGlobalResource<TResource>(ITakeResourceContext context)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new() =>
            ISingleton<TResource>.GetInstance().Take(context);

        /// <summary>
        /// Sets a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void SetGlobalResource<TResource>(int32 amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new() =>
            SetGlobalResource<TResource>(new GenericModifyResourceContext(null, amount));

        /// <summary>
        /// Sets a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void SetGlobalResource<TResource>(IModifyResourceContext context)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new() =>
            ISingleton<TResource>.GetInstance().SetAmount(context);

        /// <summary>
        /// Checks if the global resource of type <typeparamref name="TResource"/> has enough of the specified amount.
        /// </summary>
        public static bool HasEnoughGlobalResource<TResource>(int32 amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new() =>
            HasEnoughGlobalResource<TResource>(new GenericCompareResourceContext(null, amount));

        /// <summary>
        /// Checks if the global resource of type <typeparamref name="TResource"/> has enough of the specified amount.
        /// </summary>
        public static bool HasEnoughGlobalResource<TResource>(ICompareResourceContext context)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new() =>
            ISingleton<TResource>.GetInstance().HasEnough(context);

        /// <summary>
        /// Tries to take a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static bool TryTakeGlobalResource<TResource>(int32 amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new() =>
            TryTakeGlobalResource<TResource>(new GenericTakeResourceContext(null, amount));

        /// <summary>
        /// Tries to take a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static bool TryTakeGlobalResource<TResource>(ITakeResourceContext context)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new() =>
            ISingleton<TResource>.GetInstance().TryTake(context);
    }
}