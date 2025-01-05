namespace FastUnityCreationKit.Annotations.Utility
{
    /// <summary>
    ///     Represents a polymorphic type.
    ///     TODO: Add validator to ensure that polymorphic type has one of the following:
    ///         - [RequiresOdinSerialization] attribute to ensure that error is thrown if Odin is not present
    ///         - ISerializationCallbackReceiver interface to ensure that type has custom serialization
    /// </summary>
    public sealed class PolymorphAttribute : System.Attribute
    {
        
    }
}