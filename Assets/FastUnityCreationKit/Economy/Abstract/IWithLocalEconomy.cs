using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Context;
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
        public void AddResource<TResource, TNumberType>(IAddResourceContext<TNumberType> context)
            where TResource : ILocalResource, IResource<TNumberType>
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            if (this is IWithWithLocalResource<TResource> localResource)
            {
                // Get resource
                TResource resource = localResource.ResourceStorage;

                // Add resource
                resource.AddValue(context);
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
        public void TakeResource<TResource, TNumberType>(ITakeResourceContext<TNumberType> context)
            where TResource : ILocalResource, IResource<TNumberType>
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            if (this is IWithWithLocalResource<TResource> localResource)
            {
                // Get resource
                TResource resource = localResource.ResourceStorage;

                // Take resource
                resource.TakeValue(context);
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
        public void SetResource<TResource, TNumberType>(IModifyResourceContext<TNumberType> context)
            where TResource : ILocalResource, IResource<TNumberType>
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            if (this is IWithWithLocalResource<TResource> localResource)
            {
                // Get resource
                TResource resource = localResource.ResourceStorage;

                // Set resource
                resource.SetValue(context);
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
        /// <returns></returns>
        public bool HasEnoughResource<TResource, TNumberType>(ICompareResourceContext<TNumberType> context)
            where TResource : ILocalResource, IResource<TNumberType>
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            if (this is IWithWithLocalResource<TResource> localResource)
            {
                // Get resource
                TResource resource = localResource.ResourceStorage;

                // Convert resource to interface
                IResource resourceInterface = resource;
                return resourceInterface.HasEnoughValue(context);
            }

            // Log error
            Debug.LogError($"Object {this} does not support local resource of type {nameof(TResource)}.");

            return false;
        }

        /// <summary>
        /// Tries to take resource from object
        /// </summary>
        public bool TryTakeResource<TResource, TNumberType>(ITakeResourceContext<TNumberType> context)
            where TResource : ILocalResource, IResource<TNumberType>
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            if (this is IWithWithLocalResource<TResource> localResource)
            {
                // Get resource
                TResource resource = localResource.ResourceStorage;

                // Try take resource
                return resource.TryTakeValue(context);
            }

            // Log error
            Debug.LogError($"Object {this} does not support local resource of type {nameof(TResource)}.");

            return false;
        }
    }
}