using System;
using FastUnityCreationKit.Annotations.Attributes.Interfaces;

namespace FastUnityCreationKit.Annotations.Attributes
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