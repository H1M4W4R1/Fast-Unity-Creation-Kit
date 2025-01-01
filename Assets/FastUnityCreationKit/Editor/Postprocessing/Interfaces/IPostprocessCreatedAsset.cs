namespace FastUnityCreationKit.Editor.Postprocessing.Interfaces
{
    public interface IPostprocessCreatedAsset
    {
        internal void _PostprocessCreatedAsset(string assetPath);
        public void PostprocessCreatedAsset(string assetPath);
    }
}