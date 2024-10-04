using FastUnityCreationKit.Context.Abstract;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents an object that has an asset reference.
    /// Asset reference is a reference to the asset that can be loaded
    /// using Addressables system.
    /// </summary>
    public interface IWithAssetReference<TAssetType, [UsedImplicitly] TUsageContext> : IWithAssetReference,
        IWithProperty<IWithAssetReference<TAssetType, TUsageContext>, IWithAssetReference<TAssetType, AnyUsageContext>,
            AssetReferenceT<TAssetType>, TUsageContext>
        where TAssetType : Object
        where TUsageContext : IUsageContext
    {
        /// <summary>
        /// Asset reference to the asset, assign it to acquire
        /// reference to the asset from scriptable object configuration object.
        /// </summary>
        public AssetReferenceT<TAssetType> AssetReference { get; }

        AssetReferenceT<TAssetType> IWithProperty<IWithAssetReference<TAssetType, TUsageContext>,
            IWithAssetReference<TAssetType, AnyUsageContext>, AssetReferenceT<TAssetType>, TUsageContext>.Property
            => AssetReference;
    }

    public interface IWithAssetReference
    {
        /// <summary>
        /// Gets asset reference of the specified type and usage context.
        /// </summary>
        [CanBeNull]
        public AssetReferenceT<TAssetType> GetAssetReference<TAssetType, TUsageContext>()
            where TAssetType : Object
            where TUsageContext : IUsageContext
            => IWithProperty<IWithAssetReference<TAssetType, TUsageContext>,
                    IWithAssetReference<TAssetType, AnyUsageContext>, AssetReferenceT<TAssetType>, TUsageContext>
                .GetProperty(this);
    }
}