using FastUnityCreationKit.Core.Numerics.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Economy.Abstract
{
    /// <summary>
    /// Represents that object supports local economy.
    /// </summary>
    public interface ILocalEconomy
    {
        /// <summary>
        /// Checks if object has resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public bool HasResource<TResource, TNumberType>()
            where TResource : ResourceBase<TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            return this is ILocalResource<TResource, TNumberType>;
        }
        
        /// <summary>
        /// Tries to get resource from object (if object supports local resource return true).
        /// If object does not support local resource, logs error and returns false.
        /// </summary>
        public bool TryGetResource<TResource, TNumberType>([CanBeNull] out TResource resource)
            where TResource : ResourceBase<TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            if (this is ILocalResource<TResource, TNumberType> localResource)
            {
                resource = localResource.ResourceStorage;
                return true;
            }

            // Log error
            Debug.LogError($"Object {this} does not support local resource of type {nameof(TResource)}.");
            
            resource = default;
            return false;
        }
    }
}