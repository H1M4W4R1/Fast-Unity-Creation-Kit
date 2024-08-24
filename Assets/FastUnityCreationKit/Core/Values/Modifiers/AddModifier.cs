using System;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.PrioritySystem.Abstract;
using FastUnityCreationKit.Core.Values.Abstract;

namespace FastUnityCreationKit.Core.Values.Modifiers
{
    /// <summary>
    /// Represents a modifier that adds an amount to the value.
    /// This modifier is always added after percentage modifiers and
    /// modify result value at the end of the calculation.
    /// </summary>
    public abstract class AddModifier<TNumber> : IModifier
        where TNumber : INumber
    {
        public uint Priority => 2_147_483_648;
        
        /// <summary>
        /// Amount to add to the value.
        /// </summary>
        public readonly TNumber amount;

        public AddModifier(TNumber amount)
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