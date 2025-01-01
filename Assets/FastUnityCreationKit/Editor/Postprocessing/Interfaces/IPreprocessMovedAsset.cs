using UnityEditor;

namespace FastUnityCreationKit.Editor.Postprocessing.Interfaces
{
    public interface IPreprocessMovedAsset
    {
        internal AssetMoveResult _PreprocessMovedAsset(string fromPath, string toPath);
        public AssetMoveResult PreprocessMovedAsset(string fromPath, string toPath);
    }
}