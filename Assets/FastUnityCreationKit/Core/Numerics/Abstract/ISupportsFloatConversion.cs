namespace FastUnityCreationKit.Core.Numerics.Abstract
{
    /// <summary>
    /// Represents that numbers supports floating point conversion.
    /// Also known as conversion from float or double to the number.
    /// </summary>
    public interface ISupportsFloatConversion<TNumberType>
        where TNumberType : struct, INumber
    {
        /// <summary>
        /// Converts float to the number.
        /// </summary>
        public TNumberType FromFloat(float value);
        
        /// <summary>
        /// Converts double to the number.
        /// </summary>
        public TNumberType FromDouble(double value);
    }
}