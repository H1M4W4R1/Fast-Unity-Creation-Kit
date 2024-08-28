using FastUnityCreationKit.Core.Numerics.Abstract;

namespace FastUnityCreationKit.Core.Numerics.Limits
{
    /// <summary>
    /// Represents a maximum limit for a number.
    /// Used on an object with a numeric context - for example a status effect that reduces health by a certain amount.
    /// </summary>
    public interface IWithMaxLimit<TNumber> : ILimit<TNumber> 
        where TNumber : struct, INumber, ISupportsFloatConversion<TNumber>
    {
        
        /// <summary>
        /// Maximum limit for the number.
        /// </summary>
        public TNumber MaxLimit { get; }
        
    }
}