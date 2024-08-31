using FastUnityCreationKit.Core.Utility.Properties;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization;

namespace FastUnityCreationKit.Core.Utility
{
    /// <summary>
    /// Represents utility functions for the core.
    /// It uses <see cref="object"/> as the input type to allow any object to use these functions.
    /// This makes it way easier to handle interface casting in case object implements multiple interfaces.
    /// </summary>
    public static class UtilityExtensions
    {

        [NotNull]
        public static string GetName<TNamed, TUsageContext>([NotNull] this TNamed obj)
            where TNamed : IWithName<TUsageContext>
            where TUsageContext : IUsageContext => obj.Name;

        [NotNull]
        public static LocalizedString GetLocalizedName<TNamed, TUsageContext>([NotNull] this TNamed obj)
            where TNamed : IWithLocalizedName<TUsageContext>
            where TUsageContext : IUsageContext => obj.LocalizedName;
        
        [NotNull]
        public static string GetDescription<TDescribed, TUsageContext>([NotNull] this TDescribed obj)
            where TDescribed : IWithDescription<TUsageContext>
            where TUsageContext : IUsageContext => obj.Description;

        [NotNull]
        public static LocalizedString GetLocalizedDescription<TDescribed, TUsageContext>([NotNull] this TDescribed obj)
            where TDescribed : IWithLocalizedDescription<TUsageContext>
            where TUsageContext : IUsageContext => obj.LocalizedDescription;

        [CanBeNull]
        public static TPrefabType GetPrefab<TPrefabType, TPrefabUsage, TWithPrefab>([NotNull] this TWithPrefab obj)
            where TPrefabUsage : IUsageContext
            where TPrefabType : Object
            where TWithPrefab : IWithPrefab<TPrefabType, TPrefabUsage> => obj.Prefab;
        
        [CanBeNull]
        public static Sprite GetIcon<TWithIcon, TIconUsage>([NotNull] this TWithIcon obj)
            where TIconUsage : IUsageContext
            where TWithIcon : IWithIcon<TIconUsage> => obj.Icon;
        
        [CanBeNull]
        public static TConfiguration GetConfiguration<TConfiguration, TWithConfiguration, TUsageContext>([NotNull] this TWithConfiguration obj)
            where TWithConfiguration : IWithConfiguration<TConfiguration, TUsageContext> 
            where TConfiguration : ScriptableObject 
            where TUsageContext : IUsageContext => obj.Configuration;
        
        [CanBeNull]
        public static AssetReferenceT<TAssetType> GetAssetReference<TAssetType, TWithAssetReference, TUsageContext>([NotNull] this TWithAssetReference obj)
            where TWithAssetReference : IWithAssetReference<TAssetType, TUsageContext> 
            where TAssetType : Object 
            where TUsageContext : IUsageContext => obj.AssetReference;
    }
}