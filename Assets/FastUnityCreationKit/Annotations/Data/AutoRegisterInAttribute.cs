using System;

namespace FastUnityCreationKit.Annotations.Data
{
    /// <summary>
    /// Automatically registers class in specified type. 
    /// </summary>
    /// <remarks>
    /// May cause issues with Addressables if used on Addressable that has no
    /// [AddressableGroup] attribute.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AutoRegisterInAttribute : Attribute
    {
        public Type Type { get; }

        public AutoRegisterInAttribute(Type type)
        {
            Type = type;
        }
    }
}