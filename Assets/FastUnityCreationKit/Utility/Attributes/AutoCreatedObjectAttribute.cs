using System;

namespace FastUnityCreationKit.Utility.Attributes
{
    public sealed class AutoCreatedObjectAttribute : Attribute
    {
        public string SubDirectory { get; }

        public AutoCreatedObjectAttribute(string subDirectory)
        {
            SubDirectory = subDirectory;
        }
    }
}