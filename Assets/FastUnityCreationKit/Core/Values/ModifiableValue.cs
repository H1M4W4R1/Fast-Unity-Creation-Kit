using System;
using System.Collections.Generic;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Numerics.Abstract.Operations;
using FastUnityCreationKit.Core.PrioritySystem.Tools;
using FastUnityCreationKit.Core.Values.Abstract;
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
    public abstract class ModifiableValue<TNumberType> : IModifiableValue<TNumberType>
        where TNumberType : struct, INumber
    {
        /// <summary>
        /// Indicates whether the value is initialized.
        /// If not, the value should be initialized before accessing it.
        /// </summary>
        private bool _isInitialized;
        
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
        private PrioritizedList<IModifier> _appliedModifiers;

        /// <inheritdoc/>
        public TNumberType CurrentValue
        {
            get
            {
                EnsureValueIsInitialized();
                return currentValue;
            }
        }
        
        /// <inheritdoc/>
        public void ApplyModifier(IModifier modifier)
        {
            EnsureValueIsInitialized();
            _appliedModifiers.Add(modifier);
            RecalculateValue();
        }
        
        /// <inheritdoc/>
        public void RemoveModifier(IModifier modifier)
        {
            EnsureValueIsInitialized();
            _appliedModifiers.Remove(modifier);
            RecalculateValue();
        }
        
        /// <inheritdoc/>
        public void Add(TNumberType amount)
        {
            EnsureValueIsInitialized();
            
            // Check if number supports addition
            if (currentValue is not IAddOperationSupport<TNumberType, TNumberType> addOperationSupport) return;
            
            // Add the amount to the current value            
            currentValue = addOperationSupport.Add(amount);
        }
        
        /// <inheritdoc/>
        public void Subtract(TNumberType amount)
        {
            EnsureValueIsInitialized();
            
            // Check if number supports subtraction
            if (currentValue is not ISubtractOperationSupport<TNumberType, TNumberType> subtractOperationSupport) return;
            
            // Subtract the amount from the current value
            currentValue = subtractOperationSupport.Subtract(amount);
        }
        
        /// <inheritdoc/>
        public void Multiply(TNumberType amount)
        {
            EnsureValueIsInitialized();
            
            // Check if number supports multiplication
            if (currentValue is not IMultiplyOperationSupport<TNumberType, TNumberType> multiplyOperationSupport)
                throw new NotSupportedException("The number type does not support multiplication.");
                
            // Multiply the current value by the amount
            currentValue = multiplyOperationSupport.Multiply(amount);
        }
        
        
        /// <inheritdoc/>
        public void Multiply(float32 amount)
        {
            EnsureValueIsInitialized();
            
            // Check if number supports multiplication
            if (currentValue is not IMultiplyOperationSupport<float32, float32> multiplyOperationSupport)
                throw new NotSupportedException("The number type does not support multiplication.");
            
            // Multiply the current value by the amount
            float32 result = multiplyOperationSupport.Multiply(amount);
            
            // Check if the result can be converted to the current number type
            if (result is not ISupportsFloatConversion<TNumberType> supportsFloatConversion)
                throw new NotSupportedException("The number type does not support float conversion.");
            
            // Convert the result to the current number type
            currentValue = supportsFloatConversion.FromFloat(result);
        }
        
        /// <inheritdoc/>
        public void Multiply(float64 amount)
        {
            EnsureValueIsInitialized();
            
            // Check if number supports multiplication
            if (currentValue is not IMultiplyOperationSupport<float64, float64> multiplyOperationSupport) 
                throw new NotSupportedException("The number type does not support multiplication.");
            
            // Multiply the current value by the amount
            float64 result = multiplyOperationSupport.Multiply(amount);
            
            // Check if the result can be converted to the current number type
            if (result is not ISupportsFloatConversion<TNumberType> supportsFloatConversion)
                throw new NotSupportedException("The number type does not support float conversion.");
            
            // Convert the result to the current number type
            currentValue = supportsFloatConversion.FromDouble(result);
        }
        
        /// <inheritdoc/>
        public void Divide(TNumberType amount)
        {
            EnsureValueIsInitialized();
            
            // Check if number supports division
            if (currentValue is not IDivideOperationSupport<TNumberType, TNumberType> divideOperationSupport)
                throw new NotSupportedException("The number type does not support division.");
            
            // Divide the current value by the amount
            currentValue = divideOperationSupport.Divide(amount);
        }

        /// <inheritdoc/>
        public void Divide(float32 amount)
        {
            EnsureValueIsInitialized();
            
            // Check if number supports division
            if (currentValue is not IDivideOperationSupport<float32, float32> divideOperationSupport)
                throw new NotSupportedException("The number type does not support division.");
            
            // Divide the current value by the amount
            float32 result = divideOperationSupport.Divide(amount);
            
            // Check if the result can be converted to the current number type
            if (result is not ISupportsFloatConversion<TNumberType> supportsFloatConversion)
                throw new NotSupportedException("The number type does not support float conversion.");
            
            // Convert the result to the current number type
            currentValue = supportsFloatConversion.FromFloat(result);
        }

        /// <inheritdoc/>
        public void Divide(float64 amount)
        {
            EnsureValueIsInitialized();
            
            // Check if number supports division
            if (currentValue is not IDivideOperationSupport<float64, float64> divideOperationSupport)
                throw new NotSupportedException("The number type does not support division.");
            
            // Divide the current value by the amount
            float64 result = divideOperationSupport.Divide(amount);
            
            // Check if the result can be converted to the current number type
            if (result is not ISupportsFloatConversion<TNumberType> supportsFloatConversion)
                throw new NotSupportedException("The number type does not support float conversion.");
            
            // Convert the result to the current number type
            currentValue = supportsFloatConversion.FromDouble(result);
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
        private void ApplyModifiers(IReadOnlyList<IModifier> modifiers)
        {
            for (int index = 0; index < modifiers.Count; index++)
            {
                IModifier modifier = modifiers[index];
                modifier.Apply(this);
            }
        }

        /// <summary>
        /// Ensure that the value is initialized.
        /// Should be called before accessing the value.
        /// </summary>
        private void EnsureValueIsInitialized()
        {
            // If the value is already initialized, return
            if(_isInitialized) return;

            _appliedModifiers ??= new PrioritizedList<IModifier>();
            currentValue = baseValue;
            _isInitialized = true;
        }
    }
}