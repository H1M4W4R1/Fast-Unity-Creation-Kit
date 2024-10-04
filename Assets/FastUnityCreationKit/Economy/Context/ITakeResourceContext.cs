using FastUnityCreationKit.Core.Numerics.Abstract;

namespace FastUnityCreationKit.Economy.Context
{
	/// <summary>
	/// This context represents an explicit resource taking. Amount must be always negative.
	/// </summary>
	/// <typeparam name="TNumberType">The number type.</typeparam>
    public interface ITakeResourceContext<TNumberType> : IModifyResourceContext<TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        
    }
}