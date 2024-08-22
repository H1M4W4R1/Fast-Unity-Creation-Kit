using FastUnityCreationKit.Core.Numerics.Abstract;

namespace FastUnityCreationKit.Core.Values.Abstract
{
    /// <summary>
    /// Represents a modifiable value.
    /// Modifiable value is a value that can be modified by adding or removing modifiers.
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
    }
}