using System;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.PrioritySystem.Abstract;
using FastUnityCreationKit.Core.Values.Abstract;

namespace FastUnityCreationKit.Core.Values.Modifiers
{
    /// <summary>
    /// Represents a modifier that adds a flat amount to the value.
    /// Flat modifiers are always added before percentage modifiers and
    /// modify base value at the beginning of the calculation.
    /// </summary>
    public abstract class FlatAddModifier<TNumber> : IModifier
        where TNumber : INumber
    {
        public uint Priority => 536_870_912;
        
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
            if(amount is TNumberType valueToAdd)
                value.Subtract(valueToAdd);
            else
                throw new NotSupportedException("Number type of the amount is not supported.");
        }

        public int CompareTo(IPrioritySupport other) => Priority.CompareTo(other.Priority);
    }
}