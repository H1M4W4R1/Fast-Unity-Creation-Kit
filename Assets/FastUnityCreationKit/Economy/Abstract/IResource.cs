using FastUnityCreationKit.Core.Numerics.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Economy.Abstract
{
    public interface IResource
    {
        /// <summary>
        /// Reinterprets resource to another type.
        /// Used for casting resource to its derived type.
        /// </summary>
        [CanBeNull] public TResourceType As<TResourceType, TNumberType>()
            where TResourceType : ResourceBase<TResourceType, TNumberType>
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            // Try to convert this to base resource
            if (this is ResourceBase<TResourceType, TNumberType> resourceBase)
                return resourceBase.As<TResourceType>();

            // Log error if resource is not of type TResourceType
            Debug.LogError($"Resource is not of type {typeof(TResourceType).Name}");

            // If not in editor, return null.
            return null;
        }
    }
}