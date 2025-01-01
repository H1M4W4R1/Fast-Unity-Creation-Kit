namespace FastUnityCreationKit.Editor.Postprocessing.Interfaces
{
    public interface IPreprocessCreatedAsset
    {
        internal void _PreprocessCreatedAsset(string assetPath);
        public void PreprocessCreatedAsset(string assetPath);
    }
}