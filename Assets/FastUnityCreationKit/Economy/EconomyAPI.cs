using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Utility.Singleton;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Context;
using NUnit.Framework.Interfaces;

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
            where TResource : IGlobalResource, ISingleton<TResource>, new()
        {
            // Acquire reference via internal instance method
            return ISingleton<TResource>.GetInstance();
        }

        /// <summary>
        /// Adds a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void AddGlobalResource<TResource>(float amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
        {
            TResource resource = ISingleton<TResource>.GetInstance();
            resource.AddValue(amount);
        }

        /// <summary>
        /// Takes a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void TakeGlobalResource<TResource>(float amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
        {
            TResource resource = ISingleton<TResource>.GetInstance();
            resource.TakeValue(amount);
        }
        
        /// <summary>
        /// Sets a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void SetGlobalResource<TResource>(float amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
        {
            TResource resource = ISingleton<TResource>.GetInstance();
            resource.SetValue(amount);
        }

        /// <summary>
        /// Checks if the global resource of type <typeparamref name="TResource"/> has enough of the specified amount.
        /// </summary>
        public static bool HasEnoughGlobalResource<TResource>(float amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
        {
            TResource resource = ISingleton<TResource>.GetInstance();
            return resource.HasEnoughValue(amount);
        }

        /// <summary>
        /// Tries to take a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static bool TryTakeGlobalResource<TResource>(float amount)
            where TResource : IResource, IGlobalResource, ISingleton<TResource>, new()
        {
            TResource resource = ISingleton<TResource>.GetInstance();
            return resource.TryTakeValue(amount);
        }
    }
}