namespace FastUnityCreationKit.Editor.Postprocessing.Abstract
{
    public interface IPreprocessCreatedAsset
    {
        internal void _PreprocessCreatedAsset(string assetPath);
        public void PreprocessCreatedAsset(string assetPath);
    }
}