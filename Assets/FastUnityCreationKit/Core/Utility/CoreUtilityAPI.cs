using FastUnityCreationKit.Core.Utility.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Core.Utility
{
    /// <summary>
    /// Represents utility functions for the core.
    /// It uses <see cref="object"/> as the input type to allow any object to use these functions.
    /// This makes it way easier to handle interface casting in case object implements multiple interfaces.
    /// </summary>
    public static class CoreUtilityAPI
    {

        /// <summary>
        /// Gets the name of the object.
        /// If the object implements <see cref="INamed"/>, returns the name of the object.
        /// Otherwise, returns an empty string.
        /// </summary>
        [NotNull] public static string GetName([CanBeNull] this object obj)
        {
            // If the object is named, return the name.
            if (obj is INamed named)
                return named.Name;

            return string.Empty;
        }
        
        /// <summary>
        /// Gets the description of the object.
        /// If the object implements <see cref="IDescribed"/>, returns the description of the object.
        /// Otherwise, returns an empty string.
        /// </summary>
        [NotNull] public static string GetDescription([CanBeNull] this object obj)
        {
            // If the object is described, return the description.
            if (obj is IDescribed described)
                return described.Description;

            return string.Empty;
        }
        
        /// <summary>
        /// Gets prefab of the object based on the prefab usage.
        /// Returns the prefab of the object if it implements <see cref="IWithPrefab{TPrefabType,TPrefabUsage}"/>.
        /// Otherwise, returns null.
        /// </summary>
        [CanBeNull] public static TPrefabType GetPrefab<TPrefabType, TPrefabUsage>([CanBeNull] this object obj)
            where TPrefabUsage : IPrefabUsage
            where TPrefabType : Object
        {
            // If the object has a prefab, return the prefab.
            if (obj is IWithPrefab<TPrefabType, TPrefabUsage> withPrefab)
                return withPrefab.Prefab;

            return null;
        }
        
        /// <summary>
        /// Gets the icon of the object.
        /// Returns the icon of the object if it implements <see cref="IWithIcon{TIconUsage}"/>.
        /// Otherwise, returns null.
        /// </summary>
        [CanBeNull] public static Sprite GetIcon<TIconUsage>([CanBeNull] this object obj)
            where TIconUsage : IIconUsage
        {
            if (obj is IWithIcon<TIconUsage> withIcon)
                return withIcon.Icon;

            return null;
        }
        
        
    }
}