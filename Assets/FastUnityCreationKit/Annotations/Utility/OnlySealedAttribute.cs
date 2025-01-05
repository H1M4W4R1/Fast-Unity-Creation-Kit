using System;

namespace FastUnityCreationKit.Annotations.Utility
{
    /// <summary>
    ///     Indicates that the polymorphic serialization should not be used for the target class.
    ///     Also known as "only sealed classes can be added to behaviours".
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class OnlySealedAttribute : Attribute
    {
        
    }
}