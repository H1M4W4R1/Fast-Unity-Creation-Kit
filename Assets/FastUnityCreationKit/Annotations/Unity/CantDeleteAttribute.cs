using System;
using FastUnityCreationKit.Annotations.Interfaces;

namespace FastUnityCreationKit.Annotations.Unity
{
    /// <summary>
    ///     Represents an attribute that marks a class that assets can't be deleted.
    /// </summary>
    public class CantDeleteAttribute : Attribute, ICantDeleteAssetAttribute
    {
    }
}