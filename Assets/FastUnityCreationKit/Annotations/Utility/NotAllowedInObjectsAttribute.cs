namespace FastUnityCreationKit.Annotations.Utility
{
    /// <summary>
    ///     Marks that type or member is not allowed to be used in Unity objects that are inherited from
    ///     <see cref="UnityEngine.Object"/>
    /// </summary>
    /// <remarks>
    ///     Currently this is only a marker attribute and does not have any validation functionality. 
    /// </remarks>
    /// TODO: Create a custom attribute validator to validate this attribute. Update CodeDoc afterwards.
    public sealed class NotAllowedInObjectsAttribute : System.Attribute
    {
        
    }
}