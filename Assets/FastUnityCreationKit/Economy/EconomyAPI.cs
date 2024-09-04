using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Abstract;

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
            where TResource : IGlobalResource, new()
        {
            TResource resource = new();

            // Acquire reference via internal instance method
            return resource.GetGlobalResourceReference<TResource>();
        }

        /// <summary>
        /// Adds a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void AddGlobalResource<TResource>(float amount)
            where TResource : IResource, IGlobalResource, new()
        {
            TResource resource = new();
            TResource reference = resource.GetGlobalResourceReference<TResource>();

            IResource resourceInterface = reference;
            resourceInterface!.AddValue(null, amount);
        }

        /// <summary>
        /// Adds a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void AddGlobalResource<TResource, TNumberType>(TNumberType amount)
            where TResource : GlobalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            // Acquire reference via internal instance method
            TResource resource = new();
            TResource reference = resource.GetGlobalReference();

            // Convert the number to interface
            IResource<TNumberType> resourceInterface = reference;
            resourceInterface.Add(amount);
        }

        /// <summary>
        /// Takes a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void TakeGlobalResource<TResource>(float amount)
            where TResource : IResource, IGlobalResource, new()
        {
            TResource resource = new();
            TResource reference = resource.GetGlobalResourceReference<TResource>();

            IResource resourceInterface = reference;
            resourceInterface!.TakeValue(null, amount);
        }
        
        /// <summary>
        /// Takes a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void TakeGlobalResource<TResource, TNumberType>(TNumberType amount)
            where TResource : GlobalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            // Acquire reference via internal instance method
            TResource resource = new();
            TResource reference = resource.GetGlobalReference();

            // Convert the number to interface
            IResource<TNumberType> resourceInterface = reference;
            resourceInterface.Take(amount);
        }
        
        /// <summary>
        /// Sets a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void SetGlobalResource<TResource>(float amount)
            where TResource : IResource, IGlobalResource, new()
        {
            TResource resource = new();
            TResource reference = resource.GetGlobalResourceReference<TResource>();

            IResource resourceInterface = reference;
            resourceInterface!.SetValue(null, amount);
        }

        /// <summary>
        /// Sets a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static void SetGlobalResource<TResource, TNumberType>(TNumberType amount)
            where TResource : GlobalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            // Acquire reference via internal instance method
            TResource resource = new();
            TResource reference = resource.GetGlobalReference();
            
            // Convert the number to interface
            IResource<TNumberType> resourceInterface = reference;
            resourceInterface.SetAmount(amount);
        }

        /// <summary>
        /// Checks if the global resource of type <typeparamref name="TResource"/> has enough of the specified amount.
        /// </summary>
        public static bool HasEnoughGlobalResource<TResource>(float amount)
            where TResource : IResource, IGlobalResource, new()
        {
            TResource resource = new();
            TResource reference = resource.GetGlobalResourceReference<TResource>();

            IResource resourceInterface = reference;
            return resourceInterface!.HasEnoughValue(null, amount);
        }
        
        /// <summary>
        /// Checks if the global resource of type <typeparamref name="TResource"/> has enough of the specified amount.
        /// </summary>
        public static bool HasEnoughGlobalResource<TResource, TNumberType>(TNumberType amount)
            where TResource : GlobalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            // Acquire reference via internal instance method
            TResource resource = new();
            TResource reference = resource.GetGlobalReference();

            // Convert the number to interface
            IResource<TNumberType> resourceInterface = reference;
            return resourceInterface.HasEnough(amount);
        }

        /// <summary>
        /// Tries to take a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static bool TryTakeGlobalResource<TResource>(float amount)
            where TResource : IResource, IGlobalResource, new()
        {
            TResource resource = new();
            TResource reference = resource.GetGlobalResourceReference<TResource>();

            IResource resourceInterface = reference;
            return resourceInterface!.TryTakeValue(null, amount);
        }
        
        /// <summary>
        /// Tries to take a global resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public static bool TryTakeGlobalResource<TResource, TNumberType>(TNumberType amount)
            where TResource : GlobalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            // Acquire reference via internal instance method
            TResource resource = new();
            TResource reference = resource.GetGlobalReference();

            // Convert the number to interface
            IResource<TNumberType> resourceInterface = reference;
            return resourceInterface.TryTake(amount);
        }
    }
}