namespace FastUnityCreationKit.Core.Numerics.Limits
{
    /// <summary>
    /// Represents a default value for a number.
    /// Used on an object with a numeric context - for example a status effect that reduces health by a certain amount.
    /// </summary>
    public interface IWithDefaultValue<out TNumber>
    {
        /// <summary>
        /// Default value for the number.
        /// </summary>
        public TNumber DefaultValue { get; }
    }
}