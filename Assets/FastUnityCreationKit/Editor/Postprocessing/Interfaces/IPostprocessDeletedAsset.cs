namespace FastUnityCreationKit.Editor.Postprocessing.Interfaces
{
    public interface IPostprocessDeletedAsset
    {
        internal void _PostprocessDeletedAsset(string assetPath);
        public void PostprocessDeletedAsset(string assetPath);
    }
}