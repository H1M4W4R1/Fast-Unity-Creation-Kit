using FastUnityCreationKit.Annotations.Attributes.Interfaces;

namespace FastUnityCreationKit.Annotations.Attributes
{
    /// <summary>
    ///  Represents an attribute that marks a class that assets can't be deleted.
    /// </summary>
    public class CantDeleteAttribute : System.Attribute, ICantDeleteAssetAttribute
    {
        
    }
}