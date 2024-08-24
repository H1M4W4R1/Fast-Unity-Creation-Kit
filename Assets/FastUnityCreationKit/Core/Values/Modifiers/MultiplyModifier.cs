using System;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.PrioritySystem.Abstract;
using FastUnityCreationKit.Core.Values.Abstract;

namespace FastUnityCreationKit.Core.Values.Modifiers
{
    /// <summary>
    /// Represents a modifier that multiplies the value by an amount.
    /// Also known as percentage modifier and executed between flat and regular add modifiers.
    /// </summary>
    public abstract class MultiplyModifier<TNumber> : IModifier
        where TNumber : INumber
    {
        public uint Priority => 1_073_741_824;

        /// <summary>
        /// Amount to add to the value.
        /// </summary>
        public readonly TNumber amount;

        public MultiplyModifier(TNumber amount)
        {
            this.amount = amount;
        }

        public void Apply<TNumberType>(IModifiableValue<TNumberType> value)
            where TNumberType : struct, INumber
        {
            if (amount is TNumberType multiplier0)
                value.Multiply(multiplier0);
            else if (amount is float32 multiplier1)
                value.Multiply(multiplier1);
            else if (amount is float64 multiplier2)
                value.Multiply(multiplier2);
            else
                throw new NotSupportedException("Number type of the amount is not supported.");
        }

        public void Remove<TNumberType>(IModifiableValue<TNumberType> value)
            where TNumberType : struct, INumber
        {
            if (amount is TNumberType multiplier0)
                value.Divide(multiplier0);
            else if (amount is float32 multiplier1)
                value.Divide(multiplier1);
            else if (amount is float64 multiplier2)
                value.Divide(multiplier2);
            else
                throw new NotSupportedException("Number type of the amount is not supported.");
        }

        public int CompareTo(IPrioritySupport other) => Priority.CompareTo(other.Priority);
    }
}