using FastUnityCreationKit.Core.Numerics.Abstract;

namespace FastUnityCreationKit.Core.Values.Abstract
{
    /// <summary>
    /// Represents a value.
    /// This is intended to be used as a proxy layer for game-specific item statistics.
    /// </summary>
    public interface IValue<out TNumberType>
        where TNumberType : struct, INumber
    {
        /// <summary>
        /// Returns the current value of the modifiable value.
        /// </summary>
        public TNumberType CurrentValue { get; }
    }
}