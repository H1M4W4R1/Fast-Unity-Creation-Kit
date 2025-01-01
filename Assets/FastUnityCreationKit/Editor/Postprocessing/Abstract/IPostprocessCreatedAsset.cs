namespace FastUnityCreationKit.Editor.Postprocessing.Abstract
{
    public interface IPostprocessCreatedAsset
    {
        internal void _PostprocessCreatedAsset(string assetPath);
        public void PostprocessCreatedAsset(string assetPath);
    }
}