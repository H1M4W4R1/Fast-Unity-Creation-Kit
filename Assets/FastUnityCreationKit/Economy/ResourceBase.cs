using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Values;
using FastUnityCreationKit.Economy.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Economy
{
    /// <summary>
    /// Represents a resource in the game.
    /// <br/><br/>
    /// A resource can be for example coins, wood, stone, etc.
    /// Also, more obscure resources like mana and health can be represented by this class as
    /// in general e.g. health is a resource that can be depleted and replenished and
    /// one that player needs to manage carefully.
    /// <br/><br/>
    /// This is not intended to be used to store inventory items.
    /// </summary>
    public abstract class ResourceBase<TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        // TODO: Link resource to its scriptable object configuration file.
        
        /// <summary>
        /// Internal storage of the resource.
        /// </summary>
        private readonly ResourceStorageModifiableValue _storage = new ResourceStorageModifiableValue();

        /// <summary>
        /// Current amount of the resource.
        /// </summary>
        public TNumberType Amount => _storage.CurrentValue;
        
        /// <summary>
        /// Reinterprets resource to another type.
        /// Used for casting resource to its derived type.
        /// </summary>
        [CanBeNull]
        public TResourceType As<TResourceType>() where TResourceType : ResourceBase<TNumberType>
        {
            // If in editor, log error if resource is not of type TResourceType.
            if (this is TResourceType) return this as TResourceType;
            
            // Log error if resource is not of type TResourceType.
            Debug.LogError($"Resource is not of type {typeof(TResourceType).Name}");
            
            // Return null otherwise.
            return null;
        }
        
        /// <summary>
        /// Adds resource to the storage.
        /// </summary>
        public void Add(TNumberType amount)
        {
            _storage.Add(amount);
            ValidateResourceData();
        }

        /// <summary>
        /// Takes resource from the storage.
        /// </summary>
        public void Take(TNumberType amount)
        {
            _storage.Subtract(amount);
            ValidateResourceData();
        }
        
        /// <summary>
        /// Checks if resource storage has enough amount of resource.
        /// </summary>
        public bool HasEnough(TNumberType amount)
        {
            // Convert to floats
            double currentAmount = _storage.CurrentValue.ToFloat();
            double amountToCheck = amount.ToFloat();
            
            // Check if current amount is greater or equal to amount to check.
            return currentAmount >= amountToCheck;
        }
        
        /// <summary>
        /// Sets amount of the resource.
        /// </summary>
        public void SetAmount(TNumberType amount)
        {
            _storage.SetCurrentValue(amount);
            ValidateResourceData();
        }
        
        /// <summary>
        /// Try to take resource from the storage.
        /// </summary>
        public bool TryTake(TNumberType amount)
        {
            if (!HasEnough(amount))
                return false;
            
            Take(amount);
            return true;
        }
        
        /// <summary>
        /// Resets resource to its default amount.
        /// </summary>
        public void Reset()
        {
            if (this is IResourceWithDefaultAmount<TNumberType> defaultAmountResource)
                _storage.SetCurrentValue(defaultAmountResource.DefaultAmount);
            else
            {
                // Log warning if resource does not have default amount.
                // As this will lead to changing current value to zero it is not
                // recommended to use this method on resources that do not have default amount.
                //
                // If you need to reset resource that has default amount equal to zero,
                // you should implement this interface and return zero as default amount to
                // be explicit in your intention.
                Debug.LogWarning("Resource does not have default amount. This can lead to unexpected behavior.");
                
                // Set current value to zero.
                _storage.SetCurrentValue(default);
            }
        }
   
        /// <summary>
        /// This method validates resource limits and other data.
        /// </summary>
        private void ValidateResourceData()
        {
            // Check if resource has upper limit.
            if(this is IResourceWithMaxLimit<TNumberType> upperLimitedResource)
            {
                double upperLimit = upperLimitedResource.MaxAmount.ToFloat();
                double currentAmount = _storage.CurrentValue.ToFloat();
                
                // If current amount is greater than upper limit, set it to upper limit.
                if (currentAmount > upperLimit)
                    _storage.SetCurrentValue(upperLimitedResource.MaxAmount);
            }
            
            // Check if resource has lower limit.
            if(this is IResourceWithMinLimit<TNumberType> lowerLimitedResource)
            {
                double lowerLimit = lowerLimitedResource.MinAmount.ToFloat();
                double currentAmount = _storage.CurrentValue.ToFloat();
                
                // If current amount is less than lower limit, set it to lower limit.
                if (currentAmount < lowerLimit)
                    _storage.SetCurrentValue(lowerLimitedResource.MinAmount);
            }
        }
        
        /// <summary>
        /// Storage of the resource.
        /// </summary>
        private class ResourceStorageModifiableValue : ModifiableValue<TNumberType>
        {
            
        }
    }
}