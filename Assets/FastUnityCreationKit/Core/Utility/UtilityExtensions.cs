using FastUnityCreationKit.Core.Utility.Properties;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Core.Utility
{
    /// <summary>
    /// Represents utility functions for the core.
    /// It uses <see cref="object"/> as the input type to allow any object to use these functions.
    /// This makes it way easier to handle interface casting in case object implements multiple interfaces.
    /// </summary>
    public static class UtilityExtensions
    {
        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        [NotNull]
        public static string GetName<TNamed, TUsageContext>([NotNull] this TNamed obj)
            where TNamed : INamed<TUsageContext>
            where TUsageContext : IUsageContext => obj.Name;

        /// <summary>
        /// Gets the description of the object.
        /// </summary>
        [NotNull]
        public static string GetDescription<TDescribed, TUsageContext>([NotNull] this TDescribed obj)
            where TDescribed : IDescribed<TUsageContext>
            where TUsageContext : IUsageContext => obj.Description;

        /// <summary>
        /// Gets prefab of the object based on the prefab usage.
        /// </summary>
        [CanBeNull]
        public static TPrefabType GetPrefab<TPrefabType, TPrefabUsage, TWithPrefab>([NotNull] this TWithPrefab obj)
            where TPrefabUsage : IUsageContext
            where TPrefabType : Object
            where TWithPrefab : IWithPrefab<TPrefabType, TPrefabUsage> => obj.Prefab;

        /// <summary>
        /// Gets the icon of the object.
        /// </summary>
        [CanBeNull]
        public static Sprite GetIcon<TIconUsage, TWithIcon>([NotNull] this TWithIcon obj)
            where TIconUsage : IUsageContext
            where TWithIcon : IWithIcon<TIconUsage> => obj.Icon;
    }
}