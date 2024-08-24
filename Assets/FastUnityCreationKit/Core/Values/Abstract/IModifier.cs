using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.PrioritySystem.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.Values.Abstract
{
    /// <summary>
    /// Represents a modifier that can be applied to a value.
    /// Modifiers are used to modify values in a way that can be changed during the game.
    /// </summary>
    /// <remarks>
    /// Modifiers should be designed as classes to make it easy to create custom modifiers
    /// with nice naming syntax like 'AddFlatHealthModifier' or 'AddFlatFireDamageModifier'.
    /// This would easily allow developer to handle checking if modifier of specified type
    /// exists on desired value or if value even supports it.
    /// </remarks>
    public interface IModifier : IPrioritySupport
    {
        /// <summary>
        /// Apply this modifier to the value.
        /// </summary>
        public void Apply<TNumberType>([NotNull] IModifiableValue<TNumberType> value)
            where TNumberType : struct, INumber;

        /// <summary>
        /// Remove this modifier from the value.
        /// </summary>
        public void Remove<TNumberType>([NotNull] IModifiableValue<TNumberType> value)
            where TNumberType : struct, INumber;
    }
}