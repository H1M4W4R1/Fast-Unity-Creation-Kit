using System;

namespace FastUnityCreationKit.Annotations.Attributes
{
    /// <summary>
    /// Secures container from having duplicates.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class NoDuplicatesAttribute : Attribute
    {
        
    }
}