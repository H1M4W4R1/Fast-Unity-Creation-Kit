using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;

namespace FastUnityCreationKit.Core.Values.Abstract
{
    /// <summary>
    /// Represents a modifiable value.
    /// Modifiable value is a value that can be modified by adding or removing modifiers
    /// or by performing arithmetic operations on it.
    /// <br/><br/>
    /// <i>Only one of those methods should be used to modify the value as they are not compatible with each other.</i>
    /// </summary>
    public interface IModifiableValue<TNumberType> : IValue<TNumberType>
        where TNumberType : struct, INumber
    {
        /// <summary>
        /// Apply the modifier to the value.
        /// </summary>
        public void ApplyModifier(IModifier modifier);
        
        /// <summary>
        /// Remove the modifier from the value.
        /// </summary>
        public void RemoveModifier(IModifier modifier);

        /// <summary>
        /// Add the amount to the current value.
        /// </summary>
        public void Add(TNumberType amount);
        
        /// <summary>
        /// Subtract the amount from the current value.
        /// </summary>
        public void Subtract(TNumberType amount);
        
        /// <summary>
        /// Multiply the current value by the amount.
        /// </summary>
        public void Multiply(TNumberType amount);
        
        /// <summary>
        /// Multiply the current value by the amount.
        /// </summary>
        public void Multiply(float32 amount);
        
        /// <summary>
        /// Multiply the current value by the amount.
        /// </summary>
        public void Multiply(float64 amount);
        
        /// <summary>
        /// Divide the current value by the amount.
        /// </summary>
        public void Divide(TNumberType amount);
        
        /// <summary>
        /// Divide the current value by the amount.
        /// </summary>
        public void Divide(float32 amount);
        
        /// <summary>
        /// Divide the current value by the amount.
        /// </summary>
        public void Divide(float64 amount);
    }
}