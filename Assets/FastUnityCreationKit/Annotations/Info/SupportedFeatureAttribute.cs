using System;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Annotations.Info
{
    /// <summary>
    ///     Marks a feature as supported by the FastUnityCreationKit.
    ///     Used to handle the mess of interfaces to check which ones are
    ///     supported by on specific object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class SupportedFeatureAttribute : Attribute
    {
        public SupportedFeatureAttribute(Type featureType)
        {
            FeatureType = featureType;
        }

        public Type FeatureType { [UsedImplicitly] get; }
    }
}