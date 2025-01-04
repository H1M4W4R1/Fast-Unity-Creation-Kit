namespace FastUnityCreationKit.Core.Serialization.Interfaces
{
    /// <summary>
    /// Represents a type that can be converted to another type.
    /// </summary>
    /// <remarks>
    /// To support bi-directional conversion, you must have this interface
    /// on both types, one implementing this and second one that
    /// this type can be converted to.
    /// </remarks>
    public interface IConvertibleTo<out TTargetType>
    {
        /// <summary>
        /// Implement this method to convert the current instance to the target type.
        /// Requires explicit implementation.
        /// </summary>
        TTargetType Convert();
    }
}