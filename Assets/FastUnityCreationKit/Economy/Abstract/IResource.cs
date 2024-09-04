using FastUnityCreationKit.Core.Numerics.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Economy.Abstract
{
    public interface IResource<in TNumberType> : IResource
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// Adds the specified amount to the resource.
        /// </summary>
        internal void Add(TNumberType amount);

        /// <summary>
        /// Takes the specified amount from the resource.
        /// </summary>
        internal void Take(TNumberType amount);
        
        /// <summary>
        /// Tries to take the specified amount from the resource.
        /// </summary>
        internal bool TryTake(TNumberType amount);
        
        /// <summary>
        /// Checks if the resource has specified amount available.
        /// </summary>
        internal bool HasEnough(TNumberType amount);

        /// <summary>
        /// Sets the resource to the specified amount.
        /// </summary>
        internal void SetAmount(TNumberType amount);
        
        void IResource.AddValue<TBaseNumberType>(TBaseNumberType amount)
        {
            // Convert the number to the specified type via float conversion
            float convertedAmount = amount.ToFloat();
            TNumberType convertedNumber = new TNumberType();
            convertedNumber = convertedNumber.FromFloat(convertedAmount);
            
            Add(convertedNumber);
        }
        
        void IResource.TakeValue<TBaseNumberType>(TBaseNumberType amount)
        {
            // Convert the number to the specified type via float conversion
            float convertedAmount = amount.ToFloat();
            TNumberType convertedNumber = new TNumberType();
            convertedNumber = convertedNumber.FromFloat(convertedAmount);
            
            Take(convertedNumber);
        }
        
        bool IResource.TryTakeValue<TBaseNumberType>(TBaseNumberType amount)
        {
            // Convert the number to the specified type via float conversion
            float convertedAmount = amount.ToFloat();
            TNumberType convertedNumber = new TNumberType();
            convertedNumber = convertedNumber.FromFloat(convertedAmount);
            
            return TryTake(convertedNumber);
        }
        
        bool IResource.HasEnoughValue<TBaseNumberType>(TBaseNumberType amount)
        {
            // Convert the number to the specified type via float conversion
            float convertedAmount = amount.ToFloat();
            TNumberType convertedNumber = new TNumberType();
            convertedNumber = convertedNumber.FromFloat(convertedAmount);
            
            return HasEnough(convertedNumber);
        }
        
        void IResource.SetValue<TBaseNumberType>(TBaseNumberType amount)
        {
            // Convert the number to the specified type via float conversion
            float convertedAmount = amount.ToFloat();
            TNumberType convertedNumber = new TNumberType();
            convertedNumber = convertedNumber.FromFloat(convertedAmount);
            
            SetAmount(convertedNumber);
        }
        
        void IResource.AddValue(float amount) => Add(new TNumberType().FromFloat(amount));
        
        void IResource.TakeValue(float amount) => Take(new TNumberType().FromFloat(amount));
        
        bool IResource.TryTakeValue(float amount) => TryTake(new TNumberType().FromFloat(amount));
        
        bool IResource.HasEnoughValue(float amount) => HasEnough(new TNumberType().FromFloat(amount));
        
        void IResource.SetValue(float amount) => SetAmount(new TNumberType().FromFloat(amount));
    }
    
    /// <summary>
    /// Represents a resource - a resource is a value that can be added and taken from.
    /// </summary>
    public interface IResource
    {
        internal void AddValue(float amount);
        
        internal void AddValue<TNumberType>(TNumberType amount)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;
        
        internal void TakeValue(float amount);
        
        internal void TakeValue<TNumberType>(TNumberType amount)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;
        
        internal bool TryTakeValue(float amount);
        
        internal bool TryTakeValue<TNumberType>(TNumberType amount)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;
        
        internal bool HasEnoughValue(float amount);
        
        internal bool HasEnoughValue<TNumberType>(TNumberType amount)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;
        
        internal void SetValue(float amount);
        
        internal void SetValue<TNumberType>(TNumberType amount)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;
    }
}