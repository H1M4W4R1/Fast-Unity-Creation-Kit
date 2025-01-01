namespace FastUnityCreationKit.Editor.Postprocessing.Interfaces
{
    public interface IPostprocessAllAssets
    {
        void PostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths);
    }
}