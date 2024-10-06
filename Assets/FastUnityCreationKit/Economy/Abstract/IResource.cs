using FastUnityCreationKit.Economy.Context;
using FastUnityCreationKit.Economy.Context.Internal;

namespace FastUnityCreationKit.Economy.Abstract
{
    public interface IResource
    {
        /// <summary>
        /// Adds the specified amount to the resource.
        /// </summary>
        internal void Add(IAddResourceContext context);

        /// <summary>
        /// Adds the specified amount to the resource.
        /// </summary>
        internal void Add(int amount, IWithLocalEconomy localEconomy = null) =>
            Add(new GenericAddResourceContext(localEconomy, amount));
        
        /// <summary>
        /// Takes the specified amount from the resource.
        /// </summary>
        internal void Take(ITakeResourceContext context);
        
        /// <summary>
        /// Takes the specified amount from the resource.
        /// </summary>
        internal void Take(int amount, IWithLocalEconomy localEconomy = null) =>
            Take(new GenericTakeResourceContext(localEconomy, amount));

        /// <summary>
        /// Tries to take the specified amount from the resource.
        /// </summary>
        internal bool TryTake(ITakeResourceContext context);

        /// <summary>
        /// Tries to take the specified amount from the resource.
        /// </summary>
        internal bool TryTake(int amount, IWithLocalEconomy localEconomy = null) =>
            TryTake(new GenericTakeResourceContext(localEconomy, amount));
        
        /// <summary>
        /// Checks if the resource has specified amount available.
        /// </summary>
        internal bool HasEnough(ICompareResourceContext context);

        /// <summary>
        /// Checks if the resource has specified amount available.
        /// </summary>
        internal bool HasEnough(int amount, IWithLocalEconomy localEconomy = null) =>
            HasEnough(new GenericCompareResourceContext(localEconomy, amount));
        
        /// <summary>
        /// Sets the resource to the specified amount.
        /// </summary>
        internal void SetAmount(IModifyResourceContext context);
        
        /// <summary>
        /// Sets the resource to the specified amount.
        /// </summary>
        internal void SetAmount(int amount, IWithLocalEconomy localEconomy = null) =>
            SetAmount(new GenericModifyResourceContext(localEconomy, amount));
    }
    
}