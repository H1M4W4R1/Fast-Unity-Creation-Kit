using System;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Annotations.Editor
{
    /// <summary>
    ///     Used to represent that <see cref="byte"/> array is binary data
    ///     to provide nicely-formatted custom renderer for it.
    /// </summary>
    [UsedImplicitly] [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class BinaryDataAttribute : Attribute
    {
        
    }
}