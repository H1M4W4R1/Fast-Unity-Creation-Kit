using FastUnityCreationKit.Core.Numerics.Abstract;

namespace FastUnityCreationKit.Core.Values.Abstract
{
    /// <summary>
    /// Represents a modifier that can be applied to a value.
    /// Modifiers are used to modify values in a way that can be changed during the game.
    /// </summary>
    public interface IModifier
    {
        /// <summary>
        /// Apply this modifier to the value.
        /// </summary>
        public void Apply<TNumberType>(IModifiableValue<TNumberType> value)
            where TNumberType : struct, INumber;

        /// <summary>
        /// Remove this modifier from the value.
        /// </summary>
        public void Remove<TNumberType>(IModifiableValue<TNumberType> value)
            where TNumberType : struct, INumber;
    }
}