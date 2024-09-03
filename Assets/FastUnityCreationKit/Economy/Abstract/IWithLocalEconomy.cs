using FastUnityCreationKit.Core.Numerics.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Economy.Abstract
{
    /// <summary>
    /// Represents that object supports local economy.
    /// </summary>
    public interface IWithLocalEconomy
    {
        /// <summary>
        /// Checks if object has resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public bool HasResource<TResource>()
            where TResource : ILocalResource, new()
        {
            return this is IWithWithLocalResource<TResource>;
        }
        
        /// <summary>
        /// Tries to get resource from object (if object supports local resource return true).
        /// If object does not support local resource, logs error and returns false.
        /// </summary>
        public bool TryGetResource<TResource>([CanBeNull] out TResource resource)
            where TResource : ILocalResource, new()
        {
            if (this is IWithWithLocalResource<TResource> localResource)
            {
                resource = localResource.ResourceStorage;
                return true;
            }

            // Log error
            Debug.LogError($"Object {this} does not support local resource of type {nameof(TResource)}.");
            
            resource = default;
            return false;
        }
        
        /// <summary>
        /// Adds resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public void AddResource<TResource, TNumberType>(TNumberType amount)
            where TResource : LocalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            if (this is IWithWithLocalResource<TResource> localResource)
            {
                // Get resource
                TResource resource = localResource.ResourceStorage;
                
                // Add resource
                resource.Add(this, amount);
            }
            else
            {
                // Log error
                Debug.LogError($"Object {this} does not support local resource of type {nameof(TResource)}.");
            }
        }
        
        /// <summary>
        /// Takes resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public void TakeResource<TResource, TNumberType>(TNumberType amount)
            where TResource : LocalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            if (this is IWithWithLocalResource<TResource> localResource)
            {
                // Get resource
                TResource resource = localResource.ResourceStorage;
                
                // Take resource
                resource.Take(this, amount);
            }
            else
            {
                // Log error
                Debug.LogError($"Object {this} does not support local resource of type {nameof(TResource)}.");
            }
        }
        
        /// <summary>
        /// Sets resource of type <typeparamref name="TResource"/> with the specified amount.
        /// </summary>
        public void SetResource<TResource, TNumberType>(TNumberType amount)
            where TResource : LocalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            if (this is IWithWithLocalResource<TResource> localResource)
            {
                // Get resource
                TResource resource = localResource.ResourceStorage;
                
                // Set resource
                resource.SetAmount(this, amount);
            }
            else
            {
                // Log error
                Debug.LogError($"Object {this} does not support local resource of type {nameof(TResource)}.");
            }
        }
        
        /// <summary>
        /// Checks if object has enough resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public bool HasEnoughResource<TResource, TNumberType>(TNumberType amount)
            where TResource : LocalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            if (this is IWithWithLocalResource<TResource> localResource)
            {
                // Get resource
                TResource resource = localResource.ResourceStorage;
                
                // Check if resource has enough
                return resource.HasEnough(amount);
            }
            
            // Log error
            Debug.LogError($"Object {this} does not support local resource of type {nameof(TResource)}.");
            
            return false;
        }
        
        /// <summary>
        /// Tries to take resource from object 
        /// </summary>
        public bool TryTakeResource<TResource, TNumberType>(TNumberType amount)
            where TResource : LocalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            if (this is IWithWithLocalResource<TResource> localResource)
            {
                // Get resource
                TResource resource = localResource.ResourceStorage;
                
                // Try take resource
                return resource.TryTake(this, amount);
            }
            else
            {
                // Log error
                Debug.LogError($"Object {this} does not support local resource of type {nameof(TResource)}.");

                return false;
            }
        }
    }
}