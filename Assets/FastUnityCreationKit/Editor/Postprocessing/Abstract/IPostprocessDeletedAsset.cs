namespace FastUnityCreationKit.Editor.Postprocessing.Abstract
{
    public interface IPostprocessDeletedAsset
    {
        internal void _PostprocessDeletedAsset(string assetPath);
        public void PostprocessDeletedAsset(string assetPath);
    }
}