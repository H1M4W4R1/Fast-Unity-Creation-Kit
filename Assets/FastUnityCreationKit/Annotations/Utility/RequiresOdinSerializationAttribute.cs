using System;

namespace FastUnityCreationKit.Annotations.Utility
{
    /// <summary>
    /// Indicates that value requires to be serialized by Odin Serializer.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class RequiresOdinSerializationAttribute : Attribute
    {
        
    }
}