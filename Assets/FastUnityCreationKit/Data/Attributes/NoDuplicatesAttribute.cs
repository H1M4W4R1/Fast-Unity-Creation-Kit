using System;

namespace FastUnityCreationKit.Data.Attributes
{
    /// <summary>
    /// Secures container from having duplicates.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class NoDuplicatesAttribute : Attribute
    {
        
    }
}