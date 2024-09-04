using FastUnityCreationKit.Core.Numerics.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Abstract
{
    // TODO: 
    // Add overrides that use IWithLocalEconomy to support
    // event callbacks for resource changes.
    public interface IResource<in TNumberType> : IResource
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// Adds the specified amount to the resource.
        /// </summary>
        internal void Add(TNumberType amount) => Add(null, amount);

        /// <summary>
        /// Adds the specified amount to the resource.
        /// </summary>
        internal void Add([CanBeNull] IWithLocalEconomy withLocalEconomy, TNumberType amount);

        /// <summary>
        /// Takes the specified amount from the resource.
        /// </summary>
        internal void Take(TNumberType amount) => Take(null, amount);

        /// <summary>
        /// Takes the specified amount from the resource.
        /// </summary>
        internal void Take([CanBeNull] IWithLocalEconomy withLocalEconomy, TNumberType amount);

        /// <summary>
        /// Tries to take the specified amount from the resource.
        /// </summary>
        internal bool TryTake(TNumberType amount) => TryTake(null, amount);

        /// <summary>
        /// Tries to take the specified amount from the resource.
        /// </summary>
        internal bool TryTake([CanBeNull] IWithLocalEconomy withLocalEconomy, TNumberType amount);

        /// <summary>
        /// Checks if the resource has specified amount available.
        /// </summary>
        internal bool HasEnough(TNumberType amount) => HasEnough(null, amount);

        /// <summary>
        /// Checks if the resource has specified amount available.
        /// </summary>
        internal bool HasEnough([CanBeNull] IWithLocalEconomy withLocalEconomy, TNumberType amount);

        /// <summary>
        /// Sets the resource to the specified amount.
        /// </summary>
        internal void SetAmount([CanBeNull] IWithLocalEconomy withLocalEconomy, TNumberType amount);

        /// <summary>
        /// Sets the resource to the specified amount.
        /// </summary>
        internal void SetAmount(TNumberType amount) => SetAmount(null, amount);

        void IResource.AddValue<TBaseNumberType>(IWithLocalEconomy localEconomy, TBaseNumberType amount)
        {
            // Convert the number to the specified type via float conversion
            float convertedAmount = amount.ToFloat();
            TNumberType convertedNumber = new TNumberType();
            convertedNumber = convertedNumber.FromFloat(convertedAmount);

            Add(localEconomy, convertedNumber);
        }

        void IResource.TakeValue<TBaseNumberType>(IWithLocalEconomy localEconomy, TBaseNumberType amount)
        {
            // Convert the number to the specified type via float conversion
            float convertedAmount = amount.ToFloat();
            TNumberType convertedNumber = new TNumberType();
            convertedNumber = convertedNumber.FromFloat(convertedAmount);

            Take(localEconomy, convertedNumber);
        }

        bool IResource.TryTakeValue<TBaseNumberType>(IWithLocalEconomy localEconomy, TBaseNumberType amount)
        {
            // Convert the number to the specified type via float conversion
            float convertedAmount = amount.ToFloat();
            TNumberType convertedNumber = new TNumberType();
            convertedNumber = convertedNumber.FromFloat(convertedAmount);

            return TryTake(localEconomy, convertedNumber);
        }

        bool IResource.HasEnoughValue<TBaseNumberType>(IWithLocalEconomy localEconomy, TBaseNumberType amount)
        {
            // Convert the number to the specified type via float conversion
            float convertedAmount = amount.ToFloat();
            TNumberType convertedNumber = new TNumberType();
            convertedNumber = convertedNumber.FromFloat(convertedAmount);

            return HasEnough(localEconomy, convertedNumber);
        }

        void IResource.SetValue<TBaseNumberType>(IWithLocalEconomy localEconomy, TBaseNumberType amount)
        {
            // Convert the number to the specified type via float conversion
            float convertedAmount = amount.ToFloat();
            TNumberType convertedNumber = new TNumberType();
            convertedNumber = convertedNumber.FromFloat(convertedAmount);

            SetAmount(localEconomy, convertedNumber);
        }

        void IResource.AddValue(IWithLocalEconomy localEconomy, float amount) =>
            Add(localEconomy, new TNumberType().FromFloat(amount));

        void IResource.TakeValue(IWithLocalEconomy localEconomy, float amount) => Take(
            localEconomy,new TNumberType().FromFloat(amount));

        bool IResource.TryTakeValue(IWithLocalEconomy localEconomy, float amount) =>
            TryTake(localEconomy,new TNumberType().FromFloat(amount));

        bool IResource.HasEnoughValue(IWithLocalEconomy localEconomy, float amount) =>
            HasEnough(localEconomy, new TNumberType().FromFloat(amount));

        void IResource.SetValue(IWithLocalEconomy localEconomy, float amount) 
            => SetAmount(localEconomy, new TNumberType().FromFloat(amount));
    }

    /// <summary>
    /// Represents a resource - a resource is a value that can be added and taken from.
    /// </summary>
    public interface IResource
    {
        internal void AddValue([CanBeNull] IWithLocalEconomy localEconomy, float amount);

        internal void AddValue<TNumberType>([CanBeNull] IWithLocalEconomy localEconomy, TNumberType amount)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;

        internal void TakeValue(IWithLocalEconomy localEconomy, float amount);

        internal void TakeValue<TNumberType>([CanBeNull] IWithLocalEconomy localEconomy, TNumberType amount)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;

        internal bool TryTakeValue(IWithLocalEconomy localEconomy, float amount);

        internal bool TryTakeValue<TNumberType>([CanBeNull] IWithLocalEconomy localEconomy, TNumberType amount)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;

        internal bool HasEnoughValue(IWithLocalEconomy localEconomy, float amount);

        internal bool HasEnoughValue<TNumberType>([CanBeNull] IWithLocalEconomy localEconomy, TNumberType amount)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;

        internal void SetValue(IWithLocalEconomy localEconomy, float amount);

        internal void SetValue<TNumberType>([CanBeNull] IWithLocalEconomy localEconomy, TNumberType amount)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;
    }
}