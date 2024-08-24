using System;
using System.Collections.Generic;
using FastUnityCreationKit.Core.Initialization;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.PrioritySystem.Tools;
using FastUnityCreationKit.Core.Values.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Core.Values
{
    /// <summary>
    /// Represents a dynamic value. This is a class to allow being passed by reference.
    /// For static values, see <see cref="StaticValue{TNumberType}"/>.
    ///
    /// Dynamic value is computed using modifiers - this allows it to support different modifications
    /// that may happen during the game.
    /// </summary>
    /// <remarks>
    /// Applied modifiers are not serialized by Unity and thus are lost when the game is restarted.
    /// Your save system should support polymorphic serialization to save and load modifiers.
    /// <br/> <br/>
    /// If you use any math operations on the value and then add/remove modifiers, the value will be recalculated
    /// and thus the operations you did will be lost (if you want to keep them, you should use modifiers).
    /// </remarks>
    [Serializable]
    public abstract class ModifiableValue<TNumberType> : IModifiableValue<TNumberType>, IInitializable
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <inheritdoc/>
        bool IInitializable.InternalInitializationStatusStorage { get; set; } 
        
        /// <summary>
        /// Base value of the dynamic value.
        /// Value is equal to this when no modifiers are applied.
        /// </summary>
        [SerializeField] private TNumberType baseValue;
        
        /// <summary>
        /// Current value of the dynamic value.
        /// Stores value after applying all modifiers.
        ///
        /// Used as cache to avoid recalculating the value every time it is accessed.
        /// </summary>
        [SerializeField] private TNumberType currentValue;
        
        /// <summary>
        /// List of modifiers that are currently applied to the value.
        /// </summary>
        [NotNull] [ItemNotNull] private PrioritizedList<IModifier> _appliedModifiers = new();

        /// <inheritdoc/>
        public TNumberType CurrentValue
        {
            get
            {
                (this as IInitializable).EnsureInitialized();
                return currentValue;
            }
        }
        
        /// <inheritdoc/>
        public void SetCurrentValue(TNumberType value)
        {
            (this as IInitializable).EnsureInitialized();
            currentValue = value;
        }

        /// <inheritdoc/>
        public void SetBaseValue(TNumberType value)
        {
            (this as IInitializable).EnsureInitialized();
            baseValue = value;
            RecalculateValue();
        }

        /// <inheritdoc/>
        public void ApplyModifier(IModifier modifier)
        {
            (this as IInitializable).EnsureInitialized();
            _appliedModifiers.Add(modifier);
            RecalculateValue();
        }
        
        /// <inheritdoc/>
        public void RemoveModifier(IModifier modifier)
        {
            (this as IInitializable).EnsureInitialized();
            _appliedModifiers.RemoveAll(obj => ReferenceEquals(obj, modifier));
            RecalculateValue();
        }
        
        /// <inheritdoc/>
        public void Add(TNumberType amount)
        {
            (this as IInitializable).EnsureInitialized();
            
            // Convert values to floating point numbers
            float floatValue = currentValue.ToFloat();
            float amountFloat = amount.ToFloat();
            
            // Add the amount to the current value
            floatValue += amountFloat;
            
            // Convert the result back to the current number type
            currentValue = currentValue.FromFloat(floatValue);
        }
        
        /// <inheritdoc/>
        public void Subtract(TNumberType amount)
        {
            (this as IInitializable).EnsureInitialized();
            
            // Convert values to floating point numbers
            float floatValue = currentValue.ToFloat();
            float amountFloat = amount.ToFloat();
            
            // Subtract the amount from the current value
            floatValue -= amountFloat;
            
            // Convert the result back to the current number type
            currentValue = currentValue.FromFloat(floatValue);
        }
        
        /// <inheritdoc/>
        public void Multiply(TNumberType amount)
        {
            (this as IInitializable).EnsureInitialized();
            
            // Convert values to floating point numbers
            float floatValue = currentValue.ToFloat();
            float amountFloat = amount.ToFloat();
            
            // Multiply the current value by the amount
            floatValue *= amountFloat;
            
            // Convert the result back to the current number type
            currentValue = currentValue.FromFloat(floatValue);
        }
        
        
        /// <inheritdoc/>
        public void Multiply(float32 amount)
        {
            (this as IInitializable).EnsureInitialized();
            
            // Convert values to floating point numbers
            float floatValue = currentValue.ToFloat();
            
            // Multiply the current value by the amount
            floatValue *= amount;
            
            // Convert the result back to the current number type
            currentValue = currentValue.FromFloat(floatValue);

        }
        
        /// <inheritdoc/>
        public void Multiply(float64 amount)
        {
            (this as IInitializable).EnsureInitialized();
            
            // Convert values to floating point numbers
            double floatValue = currentValue.ToDouble();
            
            // Multiply the current value by the amount
            floatValue *= amount;
            
            // Convert the result back to the current number type
            currentValue = currentValue.FromDouble(floatValue);
        }
        
        /// <inheritdoc/>
        public void Divide(TNumberType amount)
        {
            (this as IInitializable).EnsureInitialized();
            
            // Convert values to floating point numbers
            float floatValue = currentValue.ToFloat();
            float amountFloat = amount.ToFloat();
            
            // Divide the current value by the amount
            floatValue /= amountFloat;
            
            // Convert the result back to the current number type
            currentValue = currentValue.FromFloat(floatValue);
        }

        /// <inheritdoc/>
        public void Divide(float32 amount)
        {
            (this as IInitializable).EnsureInitialized();
            
            // Convert values to floating point numbers
            float floatValue = currentValue.ToFloat();
            
            // Divide the current value by the amount
            floatValue /= amount;
            
            // Convert the result back to the current number type
            currentValue = currentValue.FromFloat(floatValue);
        }

        /// <inheritdoc/>
        public void Divide(float64 amount)
        {
            (this as IInitializable).EnsureInitialized();
            
            // Convert values to floating point numbers
            double floatValue = currentValue.ToDouble();
            
            // Divide the current value by the amount
            floatValue /= amount;
            
            // Convert the result back to the current number type
            currentValue = currentValue.FromDouble(floatValue);
        }
        
        private void RecalculateValue()
        {
            // Reset the value to the base value
            currentValue = baseValue;
            
            // Apply all modifiers
            // Modifiers are prioritized list, so they are applied in order
            // of their priority (from the lowest priority value to the highest)
            ApplyModifiers(_appliedModifiers);
        }
        
        /// <summary>
        /// Applies all modifiers in the list to the value.
        /// </summary>
        private void ApplyModifiers([NotNull] [ItemNotNull] IReadOnlyList<IModifier> modifiers)
        {
            for (int index = 0; index < modifiers.Count; index++)
            {
                IModifier modifier = modifiers[index];
                modifier.Apply(this);
            }
        }

        /// <summary>
        /// Internal initialization method.
        /// </summary>
        void IInitializable._Initialize()
        {
            _appliedModifiers ??= new PrioritizedList<IModifier>();
            currentValue = baseValue;
        }
    }
}