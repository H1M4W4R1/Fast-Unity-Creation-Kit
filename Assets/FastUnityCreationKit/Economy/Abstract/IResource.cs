using System;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Context;
using FastUnityCreationKit.Economy.Context.Internal;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Abstract
{
    // TODO: 
    // Add overrides that use IWithLocalEconomy to support
    // event callbacks for resource changes.
    public interface IResource<TNumberType> : IResource
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// Adds the specified amount to the resource.
        /// </summary>
        internal void Add(TNumberType amount)
            => Add(new GenericAddResourceContext<TNumberType>(null, amount));

        /// <summary>
        /// Adds the specified amount to the resource.
        /// </summary>
        internal void Add(IAddResourceContext<TNumberType> context);

        /// <summary>
        /// Takes the specified amount from the resource.
        /// </summary>
        internal void Take(TNumberType amount)
            => Take(new GenericTakeResourceContext<TNumberType>(null, amount));

        /// <summary>
        /// Takes the specified amount from the resource.
        /// </summary>
        internal void Take(ITakeResourceContext<TNumberType> context);

        /// <summary>
        /// Tries to take the specified amount from the resource.
        /// </summary>
        internal bool TryTake(TNumberType amount)
            => TryTake(new GenericTakeResourceContext<TNumberType>(null, amount));

        /// <summary>
        /// Tries to take the specified amount from the resource.
        /// </summary>
        internal bool TryTake(ITakeResourceContext<TNumberType> context);

        /// <summary>
        /// Checks if the resource has specified amount available.
        /// </summary>
        internal bool HasEnough(TNumberType amount) =>
            HasEnough(new GenericCompareResourceContext<TNumberType>(null, amount));

        /// <summary>
        /// Checks if the resource has specified amount available.
        /// </summary>
        internal bool HasEnough(ICompareResourceContext<TNumberType> context);

        /// <summary>
        /// Sets the resource to the specified amount.
        /// </summary>
        internal void SetAmount(IModifyResourceContext<TNumberType> context);

        /// <summary>
        /// Sets the resource to the specified amount.
        /// </summary>
        internal void SetAmount(TNumberType amount) =>
            SetAmount(new GenericTakeResourceContext<TNumberType>(null, amount));

        void IResource.AddValue<TBaseNumberType>(IAddResourceContext<TBaseNumberType> context)
        {
            // Check if context matches the resource
            if (context is IAddResourceContext<TNumberType> typedContext) Add(typedContext);
            else throw new InvalidCastException("The context does not match the resource.");
        }

        void IResource.TakeValue<TBaseNumberType>(ITakeResourceContext<TBaseNumberType> context)
        {
            // Check if context matches the resource
            if (context is ITakeResourceContext<TNumberType> typedContext) Take(typedContext);
            else throw new InvalidCastException("The context does not match the resource.");
        }

        bool IResource.TryTakeValue<TBaseNumberType>(ITakeResourceContext<TBaseNumberType> context)
        {
            // Check if context matches the resource
            if (context is ITakeResourceContext<TNumberType> typedContext) return TryTake(typedContext);
            throw new InvalidCastException("The context does not match the resource.");
        }

        bool IResource.HasEnoughValue<TBaseNumberType>(ICompareResourceContext<TBaseNumberType> context)
        {
            // Check if context matches the resource
            if (context is ICompareResourceContext<TNumberType> typedContext) return HasEnough(typedContext);
            throw new InvalidCastException("The context does not match the resource.");
        }

        void IResource.SetValue<TBaseNumberType>(IModifyResourceContext<TBaseNumberType> context)
        {
            // Check if context matches the resource
            if (context is IModifyResourceContext<TNumberType> typedContext) SetAmount(typedContext);
            else throw new InvalidCastException("The context does not match the resource.");
        }

        void IResource.AddValue(float amount) =>
            Add(new GenericAddResourceContext<TNumberType>(null, new TNumberType().FromFloat(amount)));

        void IResource.TakeValue(float amount) =>
            Take(new GenericTakeResourceContext<TNumberType>(null, new TNumberType().FromFloat(amount)));

        bool IResource.TryTakeValue(float amount) =>
            TryTake(new GenericTakeResourceContext<TNumberType>(null, new TNumberType().FromFloat(amount)));

        bool IResource.HasEnoughValue(float amount) =>
            HasEnough(new GenericCompareResourceContext<TNumberType>(null, new TNumberType().FromFloat(amount)));

        void IResource.SetValue(float amount)
            => SetAmount(new GenericModifyResourceContext<TNumberType>(null, new TNumberType().FromFloat(amount)));
        
        void IResource.AddValue(IWithLocalEconomy economy, float amount) =>
            Add(new GenericAddResourceContext<TNumberType>(economy, new TNumberType().FromFloat(amount)));
        
        void IResource.TakeValue(IWithLocalEconomy economy, float amount) =>
            Take(new GenericTakeResourceContext<TNumberType>(economy, new TNumberType().FromFloat(amount)));
        
        bool IResource.TryTakeValue(IWithLocalEconomy economy, float amount) =>
            TryTake(new GenericTakeResourceContext<TNumberType>(economy, new TNumberType().FromFloat(amount)));
        
        bool IResource.HasEnoughValue(IWithLocalEconomy economy, float amount) =>
            HasEnough(new GenericCompareResourceContext<TNumberType>(economy, new TNumberType().FromFloat(amount)));
        
        void IResource.SetValue(IWithLocalEconomy economy, float amount) =>
            SetAmount(new GenericModifyResourceContext<TNumberType>(economy, new TNumberType().FromFloat(amount)));
    }

    /// <summary>
    /// Represents a resource - a resource is a value that can be added and taken from.
    /// </summary>
    public interface IResource
    {
        internal void AddValue(float amount);
        internal void TakeValue(float amount);
        internal bool TryTakeValue(float amount);
        internal bool HasEnoughValue(float amount);
        internal void SetValue(float amount);
        
        internal void AddValue(IWithLocalEconomy economy, float amount);
        internal void TakeValue(IWithLocalEconomy economy, float amount);
        internal bool TryTakeValue(IWithLocalEconomy economy, float amount);
        internal bool HasEnoughValue(IWithLocalEconomy economy, float amount);
        internal void SetValue(IWithLocalEconomy economy, float amount);

        internal void AddValue<TNumberType>([NotNull] IAddResourceContext<TNumberType> context)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;

        internal void TakeValue<TNumberType>([NotNull] ITakeResourceContext<TNumberType> context)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;

        internal bool TryTakeValue<TNumberType>([NotNull] ITakeResourceContext<TNumberType> context)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;

        internal bool HasEnoughValue<TNumberType>([NotNull] ICompareResourceContext<TNumberType> context)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;

        internal void SetValue<TNumberType>([NotNull] IModifyResourceContext<TNumberType> context)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>;
    }
}