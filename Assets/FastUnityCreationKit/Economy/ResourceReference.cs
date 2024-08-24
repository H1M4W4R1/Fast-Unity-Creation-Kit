using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Economy
{
    /// <summary>
    /// Represents a reference to an economy resource.
    /// This is used as a utility to easily pass resources around.
    /// </summary>
    public struct ResourceReference<TResource, TNumberType> 
        where TResource : ResourceBase<TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// Pointer to the resource object.
        /// </summary>
        public readonly TResource refPointer;
        
        public ResourceReference(TResource resource)
        {
            refPointer = resource;
        }
        
        public static implicit operator TResource(ResourceReference<TResource, TNumberType> reference)
        {
            return reference.refPointer;
        }
        
        public static implicit operator ResourceReference<TResource, TNumberType>(TResource resource)
        {
            return new ResourceReference<TResource, TNumberType>(resource);
        }
        
        public static implicit operator TNumberType(ResourceReference<TResource, TNumberType> reference)
        {
            return reference.refPointer?.Amount ?? default;
        }
        
        /// <summary>
        /// Converts a global resource to a resource reference using type of the global resource.
        /// </summary>
        public static ResourceReference<TGlobalResource, TNumberType> FromGlobalResource<TGlobalResource>()
            where TGlobalResource : GlobalResource<TGlobalResource, TNumberType>, TResource, new()
        {
            // Create virtual instance to acquire global reference
            GlobalResource<TGlobalResource, TNumberType> resource = new TGlobalResource();
            return new ResourceReference<TGlobalResource, TNumberType>(resource.GetGlobalReference());
        }
    }
}