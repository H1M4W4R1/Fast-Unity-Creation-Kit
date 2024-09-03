using FastUnityCreationKit.Core.Utility.Properties;
using FastUnityCreationKit.Core.Utility.Properties.Data;
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
        
        [CanBeNull]
        public static TLock GetLock<TWithLock, TLock>([NotNull] this TWithLock obj)
            where TWithLock : IWithLock<TLock> 
            where TLock : class, ILock, new() => obj.LockRepresentation;
        
        [CanBeNull]
        public static string GetObjectName<TUsageContext>([NotNull] this object obj)
            where TUsageContext : IUsageContext =>
            obj is IWithName name ? name.GetName<TUsageContext>() : null;
        
        [CanBeNull]
        public static LocalizedString GetObjectLocalizedName<TUsageContext>([NotNull] this object obj)
            where TUsageContext : IUsageContext =>
            obj is IWithLocalizedName name ? name.GetLocalizedName<TUsageContext>() : null;
        
        [CanBeNull]
        public static string GetObjectDescription<TUsageContext>([NotNull] this object obj)
            where TUsageContext : IUsageContext =>
            obj is IWithDescription description ? description.GetDescription<TUsageContext>() : null;
        
        [CanBeNull]
        public static LocalizedString GetObjectLocalizedDescription<TUsageContext>([NotNull] this object obj)
            where TUsageContext : IUsageContext =>
            obj is IWithLocalizedDescription description ? description.GetLocalizedDescription<TUsageContext>() : null;
        
        [CanBeNull]
        public static TPrefabType GetObjectPrefab<TPrefabType, TPrefabUsage>([NotNull] this object obj)
            where TPrefabUsage : IUsageContext
            where TPrefabType : Object =>
            obj is IWithPrefab prefab ? prefab.GetPrefab<TPrefabType, TPrefabUsage>() : null;

        [CanBeNull]
        public static Sprite GetObjectIcon<TIconUsage>([NotNull] this object obj)
            where TIconUsage : IUsageContext =>
            obj is IWithIcon icon ? icon.GetIcon<TIconUsage>() : null;
        
        [CanBeNull]
        public static TConfiguration GetObjectConfiguration<TConfiguration, TUsageContext>([NotNull] this object obj)
            where TConfiguration : ScriptableObject
            where TUsageContext : IUsageContext =>
            obj is IWithConfiguration configuration ? configuration.GetConfiguration<TConfiguration, TUsageContext>() : null;
        
        [CanBeNull]
        public static AssetReferenceT<TAssetType> GetObjectAssetReference<TAssetType, TUsageContext>([NotNull] this object obj)
            where TAssetType : Object
            where TUsageContext : IUsageContext =>
            obj is IWithAssetReference assetReference ? assetReference.GetAssetReference<TAssetType, TUsageContext>() : null;
        
        [CanBeNull]
        public static TLock GetLock<TLock>([NotNull] this object obj)
            where TLock : class, ILock, new() =>
            obj is IWithLock<TLock> lockable ? lockable.LockRepresentation : null;
    }
}