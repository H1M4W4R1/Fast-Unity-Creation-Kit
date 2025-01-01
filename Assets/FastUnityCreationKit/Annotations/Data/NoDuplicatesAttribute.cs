using System;

namespace FastUnityCreationKit.Annotations.Data
{
    /// <summary>
    /// Secures container from having duplicates.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class NoDuplicatesAttribute : Attribute
    {
        
    }
}