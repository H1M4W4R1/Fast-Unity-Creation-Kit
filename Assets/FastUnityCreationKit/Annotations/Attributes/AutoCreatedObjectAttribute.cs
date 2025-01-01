using System;

namespace FastUnityCreationKit.Annotations.Attributes
{
    /// <summary>
    /// This attribute is used to automatically create ScriptableObjects.
    /// </summary>
    public sealed class AutoCreatedObjectAttribute : Attribute
    {
        public string SubDirectory { get; }

        public AutoCreatedObjectAttribute(string subDirectory)
        {
            SubDirectory = subDirectory;
        }
    }
}