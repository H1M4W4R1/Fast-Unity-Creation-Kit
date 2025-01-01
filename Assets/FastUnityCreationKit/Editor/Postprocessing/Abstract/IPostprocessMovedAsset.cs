namespace FastUnityCreationKit.Editor.Postprocessing.Abstract
{
    public interface IPostprocessMovedAsset
    {
        internal void _PostprocessMovedAsset(string fromPath, string toPath);
        public void PostprocessMovedAsset(string fromPath, string toPath);
    }
}