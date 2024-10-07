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
    /// TODO: Add HAL methods with context as parameter
    public static class EconomyAPI
    {
        /// <summary>
        /// Gets global resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public static TResource GetGlobalResource<TResource>()
            where TResource : IGlobalResource, ISingleton<TResource>, new()
        {
            // Acquire reference via internal instance method
            return ISingleton<TResource>.GetInstance();
        }

        /// <summary>
        /// Adds a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void AddGlobalResource<TResource, TContextType>(int32 amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
            where TContextType : IAddResourceContext, new() =>
            AddGlobalResource<TResource>(new TContextType {Amount = amount});
        

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
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
        {
            TResource resource = ISingleton<TResource>.GetInstance();
            resource.Add(context);
        }

        /// <summary>
        /// Takes a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void TakeGlobalResource<TResource, TContextType>(int32 amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
            where TContextType : ITakeResourceContext, new() =>
            TakeGlobalResource<TResource>(new TContextType {Amount = amount});
        
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
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
        {
            TResource resource = ISingleton<TResource>.GetInstance();
            resource.Take(context);
        }

        /// <summary>
        /// Sets a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void SetGlobalResource<TResource, TContextType>(int32 amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
            where TContextType : IModifyResourceContext, new() =>
            SetGlobalResource<TResource>(new TContextType {Amount = amount});
        
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
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
        {
            TResource resource = ISingleton<TResource>.GetInstance();
            resource.SetAmount(context);
        }

        /// <summary>
        /// Checks if the global resource of type <typeparamref name="TResource"/> has enough of the specified amount.
        /// </summary>
        public static bool HasEnoughGlobalResource<TResource, TContextType>(int32 amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
            where TContextType : ICompareResourceContext, new() =>
            HasEnoughGlobalResource<TResource>(new TContextType {Amount = amount});
        
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
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
        {
            TResource resource = ISingleton<TResource>.GetInstance();
            return resource.HasEnough(context);
        }

        /// <summary>
        /// Tries to take a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static bool TryTakeGlobalResource<TResource, TContextType>(int32 amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
            where TContextType : ITakeResourceContext, new() =>
            TryTakeGlobalResource<TResource>(new TContextType {Amount = amount});
        
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
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
        {
            TResource resource = ISingleton<TResource>.GetInstance();
            return resource.TryTake(context);
        }
    }
}