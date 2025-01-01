namespace FastUnityCreationKit.Editor.Postprocessing.Abstract
{
    public interface IPreprocessSavedAsset
    {
        internal void _PreprocessSavedAsset(string assetPath);
        public void PreprocessSavedAsset(string assetPath);
    }

}