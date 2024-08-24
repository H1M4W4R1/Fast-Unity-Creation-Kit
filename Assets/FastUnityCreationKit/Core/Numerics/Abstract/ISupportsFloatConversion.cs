namespace FastUnityCreationKit.Core.Numerics.Abstract
{
    /// <summary>
    /// Represents that numbers supports floating point conversion.
    /// Also known as conversion from float or double to the number.
    /// </summary>
    public interface ISupportsFloatConversion<out TNumberType> : ISupportsFloatConversion
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
        
        INumber ISupportsFloatConversion.NumberFromFloat(float value) => FromFloat(value);
        
        INumber ISupportsFloatConversion.NumberFromDouble(double value) => FromDouble(value);
      
    }
    
    public interface ISupportsFloatConversion
    {
        /// <summary>
        /// Converts the number from float.
        /// </summary>
        public INumber NumberFromFloat(float value);
        
        /// <summary>
        /// Converts the number from double.
        /// </summary>
        public INumber NumberFromDouble(double value);
        
        /// <summary>
        /// Converts the number to float.
        /// </summary>
        public float ToFloat();
        
        /// <summary>
        /// Converts the number to double.
        /// </summary>
        public double ToDouble();
    }
}