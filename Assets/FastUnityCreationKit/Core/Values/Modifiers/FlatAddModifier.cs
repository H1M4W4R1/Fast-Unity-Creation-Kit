using System;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Values.Abstract;
using FastUnityCreationKit.Core.Values.Abstract.Modifiers;

namespace FastUnityCreationKit.Core.Values.Modifiers
{
    /// <summary>
    /// Represents a modifier that adds a flat amount to the value.
    /// Flat modifiers are always added before percentage modifiers and
    /// modify base value at the beginning of the calculation.
    ///
    /// Modifiers are designed as classes to make it easy to create custom modifiers
    /// with nice naming syntax like 'AddFlatHealthModifier' or 'AddFlatFireDamageModifier'.
    /// This would easily allow developer to handle checking if modifier of specified type
    /// exists on desired value or if value even supports it.
    /// </summary>
    public abstract class FlatAddModifier<TNumber> : IModifier, IEarlyModifier
        where TNumber : INumber
    {
        /// <summary>
        /// Amount to add to the value.
        /// </summary>
        public readonly TNumber amount;

        public FlatAddModifier(TNumber amount)
        {
            this.amount = amount;
        }

        public void Apply<TNumberType>(IModifiableValue<TNumberType> value) 
            where TNumberType : struct, INumber
        {
            if(amount is TNumberType valueToAdd)
                value.Add(valueToAdd);
            else
                throw new NotSupportedException("Number type of the amount is not supported.");
        }

        public void Remove<TNumberType>(IModifiableValue<TNumberType> value) 
            where TNumberType : struct, INumber
        {
            // TODO: Implement
            throw new NotImplementedException();
        }
    }
}