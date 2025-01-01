using System;

namespace FastUnityCreationKit.Data.Attributes
{
    /// <summary>
    /// Automatically registers class in specified database. 
    /// </summary>
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