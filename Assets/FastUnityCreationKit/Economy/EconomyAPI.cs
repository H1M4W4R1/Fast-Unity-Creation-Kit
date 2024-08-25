using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Abstract;
using UnityEngine;

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
        public static GlobalResource<TResource, TNumberType> GetGlobalResource<TResource, TNumberType>()
            where TResource : GlobalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            return GlobalResource<TResource, TNumberType>.Instance;
        }
        
        /// <summary>
        /// Returns true if object supports local economy.
        /// </summary>
        public static bool SupportsLocalEconomy(this object obj) => obj is ILocalEconomy;

        /// <summary>
        /// Checks if object has local resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public static bool HasLocalResource<TResource, TNumberType>(this object obj)
            where TResource : ResourceBase<TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            return obj is ILocalEconomy localEconomy && localEconomy.HasResource<TResource, TNumberType>();
        }
        
        /// <summary>
        /// Gets local resource from object if object supports local economy.
        /// </summary>
        public static bool TryGetLocalResource<TResource, TNumberType>(this object obj, out TResource resource)
            where TResource : ResourceBase<TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            // Check if object supports local economy
            if (obj is ILocalEconomy localEconomy)
                return localEconomy.TryGetResource<TResource, TNumberType>(out resource);
            
            // Log error
            Debug.LogError($"Object {obj} does not support local economy. " +
                           $"If it is global resource, use Instance method from the resource class.");
            
            resource = default;
            return false;
        }




    }
}