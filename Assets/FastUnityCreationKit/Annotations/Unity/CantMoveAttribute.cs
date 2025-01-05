using System;
using FastUnityCreationKit.Annotations.Interfaces;

namespace FastUnityCreationKit.Annotations.Unity
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CantMoveAttribute : Attribute, ICantMoveAssetAttribute
    {
    }
}