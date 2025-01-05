using System;
using FastUnityCreationKit.Annotations.Interfaces;

namespace FastUnityCreationKit.Annotations.Unity
{
    /// <summary>
    ///     This attribute is used to automatically create ScriptableObjects.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AutoCreatedObjectAttribute : Attribute, ICantDeleteAssetAttribute, ICantMoveAssetAttribute
    {
        public AutoCreatedObjectAttribute(string subDirectory)
        {
            SubDirectory = subDirectory;
        }

        public string SubDirectory { get; }
    }
}