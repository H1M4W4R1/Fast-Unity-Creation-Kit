namespace FastUnityCreationKit.Editor.Postprocessing.Abstract
{
    public interface IPostprocessAllAssets
    {
        void PostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths);
    }
}