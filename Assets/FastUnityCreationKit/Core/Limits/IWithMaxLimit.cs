using System;

namespace FastUnityCreationKit.Core.Limits
{
    /// <summary>
    /// Represents a maximum limit for a number.
    /// Used on an object with a numeric context - for example a status effect that reduces health by a certain amount.
    /// </summary>
    /// <remarks>
    /// If TNumber is not numeric type a lot of f-up will happen.
    /// </remarks>
    public interface IWithMaxLimit<out TNumber> : IWithMaxLimit
        where TNumber : notnull
    {
        /// <summary>
        /// Maximum limit for the number.
        /// </summary>
        public new TNumber MaxLimit { get; }
        
        float IWithMaxLimit.MaxLimit => Convert.ToSingle(MaxLimit);
    }
    
    public interface IWithMaxLimit : ILimited
    {
        public float MaxLimit { get; }
    }
}