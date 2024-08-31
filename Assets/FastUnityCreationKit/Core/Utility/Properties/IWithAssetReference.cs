using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    public interface IWithAssetReference<TAssetType, [UsedImplicitly] TUsageContext>
        where TAssetType : Object
        where TUsageContext : IUsageContext
    {
        /// <summary>
        /// Asset reference to the asset, assign it to acquire
        /// reference to the asset from scriptable object configuration object.
        /// </summary>
        public AssetReferenceT<TAssetType> AssetReference { get; }
    }
}