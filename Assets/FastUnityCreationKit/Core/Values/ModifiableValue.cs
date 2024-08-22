using System;
using System.Collections.Generic;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Numerics.Abstract.Operations;
using FastUnityCreationKit.Core.Values.Abstract;
using FastUnityCreationKit.Core.Values.Abstract.Modifiers;
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
        private List<IModifier> _appliedModifiers;

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
        
        private void RecalculateValue()
        {
            // Reset the value to the base value
            currentValue = baseValue;
            
            List<IModifier> earlyModifiers = new List<IModifier>();
            List<IModifier> commonModifiers = new List<IModifier>();
            List<IModifier> lateModifiers = new List<IModifier>();
            
            // Loop through all modifiers and separate them into early, common and late modifiers
            for (int index = 0; index < _appliedModifiers.Count; index++)
            {
                IModifier modifier = _appliedModifiers[index];
                switch (modifier)
                {
                    case IEarlyModifier:
                        earlyModifiers.Add(modifier);
                        break;
                    case ILateModifier:
                        lateModifiers.Add(modifier);
                        break;
                    default:
                        commonModifiers.Add(modifier);
                        break;
                }
            }
            
            // Apply all modifiers
            ApplyModifiers(earlyModifiers);
            ApplyModifiers(commonModifiers);
            ApplyModifiers(lateModifiers);
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
            
            _appliedModifiers ??= new List<IModifier>();
            currentValue = baseValue;
            _isInitialized = true;
        }
    }
}