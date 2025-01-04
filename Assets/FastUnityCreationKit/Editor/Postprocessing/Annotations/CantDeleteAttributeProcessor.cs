using FastUnityCreationKit.Annotations.Interfaces;
using FastUnityCreationKit.Editor.Postprocessing.Abstract;
using FastUnityCreationKit.Core.Extensions;
using FastUnityCreationKit.Core.Logging;
using JetBrains.Annotations;
using UnityEditor;

namespace FastUnityCreationKit.Editor.Postprocessing.Annotations
{
    /// <summary>
    /// Objects that can't be deleted or moved.
    /// </summary>
    [UsedImplicitly]
    public sealed class CantDeleteAttributeProcessor : QuickAssetProcessor<CantDeleteAttributeProcessor>
    {
        protected override bool AssetIsRequired => false; 
 
        public override AssetDeleteResult PreprocessDeletedAsset(string assetPath, RemoveAssetOptions options)
        {
            if (CurrentAssetType == null) return AssetDeleteResult.DidNotDelete;
            if (!CurrentAssetType.HasAttribute<ICantDeleteAssetAttribute>()) return AssetDeleteResult.DidNotDelete;
            
            Guard<ValidationLogConfig>.Error($"Asset at path '{assetPath}' is not allowed to be deleted.");
            return AssetDeleteResult.FailedDelete;
        }

        public override AssetMoveResult PreprocessMovedAsset(string fromPath, string toPath)
        {
            if (CurrentAssetType == null) return AssetMoveResult.DidNotMove;
            if (!CurrentAssetType.HasAttribute<ICantMoveAssetAttribute>()) return AssetMoveResult.DidNotMove;
            
            Guard<ValidationLogConfig>.Error($"Asset at path '{fromPath}' is not allowed to be moved.");
            return AssetMoveResult.FailedMove;
        }
    }
}