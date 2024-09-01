using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Numerics.Limits;
using FastUnityCreationKit.Core.Utility;
using FastUnityCreationKit.Core.Utility.Properties;
using FastUnityCreationKit.Core.Values;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Economy.Abstract
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
    /// <remarks>
    /// <b>It is not recommended to use this class directly. See <see cref="LocalResource{TNumberType}"/> and <see cref="GlobalResource{TSelf, TNumberType}"/> instead.</b>
    /// </remarks>
    public abstract class ResourceBase<TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// Internal storage of the resource.
        /// </summary>
        [NotNull] private readonly ResourceStorageModifiableValue _storage = new ResourceStorageModifiableValue();

        /// <summary>
        /// Current amount of the resource.
        /// </summary>
        public TNumberType Amount => _storage.CurrentValue;
        
        /// <summary>
        /// Checks if resource is global.
        /// </summary>
        public bool IsGlobalResource => this is IGlobalResource;
        
        /// <summary>
        /// Checks if resource is local.
        /// </summary>
        public bool IsLocalResource => this is ILocalResource;
        
        /// <summary>
        /// Checks if resource has default amount.
        /// </summary>
        public bool HasDefaultAmount => this is IWithDefaultValue<TNumberType>;
        
        /// <summary>
        /// Checks if resource has upper limit.
        /// </summary>
        public bool HasMaxLimit => this is IWithMaxLimit<TNumberType>;
        
        /// <summary>
        /// Checks if resource has lower limit.
        /// </summary>
        public bool HasMinLimit => this is IWithMinLimit<TNumberType>;
        
        /// <summary>
        /// Gets default amount of the resource.
        /// If resource does not have default amount, returns default value of TNumberType.
        /// </summary>
        public TNumberType DefaultAmount => (this as IWithDefaultValue<TNumberType>)?.DefaultValue ?? default;
        
        /// <summary>
        /// Gets maximum amount of the resource.
        /// If resource does not have maximum amount, returns default value of TNumberType.
        /// </summary>
        public TNumberType MaxAmount => (this as IWithMaxLimit<TNumberType>)?.MaxLimit ?? default;
        
        /// <summary>
        /// Gets minimum amount of the resource.
        /// If resource does not have minimum amount, returns default value of TNumberType.
        /// </summary>
        public TNumberType MinAmount => (this as IWithMinLimit<TNumberType>)?.MinLimit ?? default;
        
        /// <summary>
        /// Reinterprets resource to another type.
        /// Used for casting resource to its derived type.
        /// </summary>
        [CanBeNull] public TResourceType As<TResourceType>() where TResourceType : ResourceBase<TNumberType>
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
        /// Takes resource from the storage.
        /// Alias for <see cref="Take"/>.
        /// </summary>
        public void Subtract(TNumberType amount) => Take(amount);
        
        
        /// <summary>
        /// Checks if resource storage has enough amount of resource.
        /// </summary>
        public bool HasEnough(TNumberType amount)
        {
            // Convert to floats
            double currentAmount = _storage.CurrentValue.ToFloat();
            double amountToCheck = amount.ToFloat();
            
            // Get minimum amount
            double minAmount = MinAmount.ToFloat();
            
            // Check if current amount is greater or equal to amount to check.
            return currentAmount - minAmount >= amountToCheck;
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
            // This does not use properties to avoid unnecessary checks.
            // Also, it's safer in case somebody would f-k up something in properties.
            if (this is IWithDefaultValue<TNumberType> defaultAmountResource)
                _storage.SetCurrentValue(defaultAmountResource.DefaultValue);
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
            // This does not use properties to avoid unnecessary checks.
            // Also, it's safer in case somebody would f-k up something in properties.
            
            // Check if resource has upper limit.
            if(this is IWithMaxLimit<TNumberType> upperLimitedResource)
            {
                double upperLimit = upperLimitedResource.MaxLimit.ToFloat();
                double currentAmount = _storage.CurrentValue.ToFloat();
                
                // If current amount is greater than upper limit, set it to upper limit.
                if (currentAmount > upperLimit)
                    _storage.SetCurrentValue(upperLimitedResource.MaxLimit);
            }
            
            // Check if resource has lower limit.
            if(this is IWithMinLimit<TNumberType> lowerLimitedResource)
            {
                double lowerLimit = lowerLimitedResource.MinLimit.ToFloat();
                double currentAmount = _storage.CurrentValue.ToFloat();
                
                // If current amount is less than lower limit, set it to lower limit.
                if (currentAmount < lowerLimit)
                    _storage.SetCurrentValue(lowerLimitedResource.MinLimit);
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