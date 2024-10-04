using FastUnityCreationKit.Core.Numerics.Abstract;

namespace FastUnityCreationKit.Economy.Context
{
    /// <summary>
    /// This context represents an explicit resource addition. Amount must be always positive.
    /// </summary>
    /// <typeparam name="TNumberType">The number type.</typeparam>
    public interface IAddResourceContext<TNumberType> : IModifyResourceContext<TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        
    }
}