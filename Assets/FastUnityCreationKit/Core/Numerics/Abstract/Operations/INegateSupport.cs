namespace FastUnityCreationKit.Core.Numerics.Abstract.Operations
{
    /// <summary>
    /// Represents a type that supports negation.
    /// Also requires <see cref="ISignedNumber"/>
    /// </summary>
    public interface INegateSupport<out TNumber> where TNumber : INumber, ISignedNumber
    {
        public TNumber Negate();
    }
}