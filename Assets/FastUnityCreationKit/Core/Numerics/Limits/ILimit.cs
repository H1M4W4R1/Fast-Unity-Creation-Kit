using FastUnityCreationKit.Core.Numerics.Abstract;

namespace FastUnityCreationKit.Core.Numerics.Limits
{
    /// <summary>
    /// Represents a limit for a number.
    /// </summary>
    public interface ILimit<TNumber> 
        where TNumber : struct, INumber, ISupportsFloatConversion<TNumber>
    {
        
    }
}