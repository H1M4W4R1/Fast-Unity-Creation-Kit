using System;
using System.Collections.Generic;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Numerics.Limits;
using FastUnityCreationKit.Core.PrioritySystem.Tools;
using FastUnityCreationKit.Core.Utility.Initialization;
using FastUnityCreationKit.Core.Utility.Properties;
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
        [SerializeField]
        private TNumberType baseValue;

        /// <summary>
        /// Current value of the dynamic value.
        /// Stores value after applying all modifiers.
        ///
        /// Used as cache to avoid recalculating the value every time it is accessed.
        /// </summary>
        [SerializeField]
        private TNumberType currentValue;

        /// <summary>
        /// List of modifiers that are currently applied to the value.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        private PrioritizedList<IModifier> _appliedModifiers = new();

        /// <inheritdoc/>
        public TNumberType CurrentValue
        {
            get
            {
                // Ensure the value is initialized
                (this as IInitializable).EnsureInitialized();

                // Remove all removable modifiers
                int nCount = CheckRemovableModifiers();
                if (nCount > 0)
                {
                    // Recalculate the value if any modifiers were removed
                    RecalculateValue();
                }

                return currentValue;
            }
        }

        /// <inheritdoc/>
        public void SetCurrentValue(TNumberType value)
        {
            (this as IInitializable).EnsureInitialized();
            currentValue = value;
            EnsureLimitsAreNotExceeded();
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
            EnsureLimitsAreNotExceeded();
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
            EnsureLimitsAreNotExceeded();
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
            EnsureLimitsAreNotExceeded();
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
            EnsureLimitsAreNotExceeded();
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
            EnsureLimitsAreNotExceeded();
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
            EnsureLimitsAreNotExceeded();
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
            EnsureLimitsAreNotExceeded();
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
            EnsureLimitsAreNotExceeded();
        }

        private void RecalculateValue()
        {
            // Remove all removable modifiers
            CheckRemovableModifiers();

            // Reset the value to the base value
            currentValue = baseValue;

            // Apply all modifiers
            // Modifiers are prioritized list, so they are applied in order
            // of their priority (from the lowest priority value to the highest)
            ApplyModifiers(_appliedModifiers);
            EnsureLimitsAreNotExceeded();
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
        void IInitializable.OnInitialize()
        {
            _appliedModifiers ??= new PrioritizedList<IModifier>();

            // Check if has IWithDefaultValue interface
            // we need to avoid SetBaseValue to prevent infinite recursion
            if (this is IWithDefaultValue<TNumberType> withDefaultValue)
                baseValue = withDefaultValue.DefaultValue;

            currentValue = baseValue;
        }

        /// <summary>
        /// Checks if the value limits are not exceeded.
        /// </summary>
        private void EnsureLimitsAreNotExceeded()
        {
            // Get current value as float
            float floatValue = currentValue.ToFloat();
            
            // Check if the current value is within the lower limit
            if (this is IWithMinLimit<TNumberType> minLimit)
            {
                // Get limit as float
                float minLimitFloat = minLimit.MinLimit.ToFloat();
                
                // Check if the current value is below the limit
                if (floatValue < minLimitFloat)
                    currentValue = minLimit.MinLimit;
            }
            
            // Check if the current value is within the upper limit
            if (this is IWithMaxLimit<TNumberType> maxLimit)
            {
                // Get limit as float
                float maxLimitFloat = maxLimit.MaxLimit.ToFloat();
                
                // Check if the current value is above the limit
                if (floatValue > maxLimitFloat)
                    currentValue = maxLimit.MaxLimit;
            }
        }

        /// <summary>
        /// Checks if any modifiers can be removed.
        /// </summary>
        private int CheckRemovableModifiers()
        {
            int removedCount = 0;

            // Loop through all applied modifiers
            // In reverse order to avoid issues with removing elements
            for (int index = _appliedModifiers.Count - 1; index >= 0; index--)
            {
                // Check if the modifier is conditionally removable
                IModifier modifier = _appliedModifiers[index];
                if (modifier is not IConditionallyRemovable removable || !removable.IsRemovalConditionMet()) continue;

                // Remove the modifier
                _appliedModifiers.RemoveAt(index);
                removedCount++;
            }

            return removedCount;
        }
    }
}