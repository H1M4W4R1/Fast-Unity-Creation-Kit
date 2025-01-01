using FastUnityCreationKit.Annotations.Interfaces;

namespace FastUnityCreationKit.Annotations.Unity
{
    /// <summary>
    /// This attribute is used to automatically create ScriptableObjects.
    /// </summary>
    public sealed class AutoCreatedObjectAttribute : System.Attribute, ICantDeleteAssetAttribute, ICantMoveAssetAttribute
    {
        public string SubDirectory { get; }

        public AutoCreatedObjectAttribute(string subDirectory)
        {
            SubDirectory = subDirectory;
        }
    }
}