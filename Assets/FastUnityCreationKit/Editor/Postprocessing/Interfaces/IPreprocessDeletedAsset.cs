using UnityEditor;

namespace FastUnityCreationKit.Editor.Postprocessing.Interfaces
{
    public interface IPreprocessDeletedAsset
    {
        internal AssetDeleteResult _PreprocessDeletedAsset(string assetPath, RemoveAssetOptions options);
        public AssetDeleteResult PreprocessDeletedAsset(string assetPath, RemoveAssetOptions options);
    }
}