using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Inventory.Abstract;
using FastUnityCreationKit.Inventory.Context;
using FastUnityCreationKit.Inventory.Stacking;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Inventory
{
    /// <summary>
    /// Represents an item in the inventory, this is the base class for all items' logic.
    /// In-game item representations should have no logic, only data/visuals.
    /// <br/><br/>
    /// If you don't want to stack your items <see cref="SingleItemStack"/>
    /// </summary>
    public abstract class InventoryItem<TStackType> : InventoryItem
        where TStackType : ItemStackCounterValue, new()
    {
        /// <summary>
        /// Stack counter for the item.
        /// </summary>
        public TStackType StackCounter { get; protected set; } = new TStackType();
       
        /// <summary>
        /// Maximum amount of items that can be stacked.
        /// </summary>
        public sealed override int MaxStack => StackCounter.MaxLimit;

        /// <summary>
        /// Gets the amount of items in the stack.
        /// </summary>
        public sealed override int AmountInStack => StackCounter.CurrentValue;

        public sealed override void ReduceStack(int amountToRemove)
        {
            // Check if the amount to remove is valid
            if (amountToRemove <= 0) return;
            
            // Check if the stack has enough items
            if (StackCounter.CurrentValue < amountToRemove)
            {
                // If not log an error
                Debug.LogError($"Tried to remove {amountToRemove} items from the stack, but there are only {StackCounter.CurrentValue} items in the stack.");
                
                // Remove all items
                StackCounter.Subtract(StackCounter.CurrentValue);
            }
            else
            {
                // Reduce the stack
                StackCounter.Subtract(amountToRemove);
            }
        }

        public sealed override void IncreaseStack(int amountToAdd)
        {
            // Check if the amount to add is valid
            if (amountToAdd <= 0) return;
            
            // Check if the stack has enough space
            if (StackCounter.CurrentValue + amountToAdd > StackCounter.MaxLimit)
            {
                // If not log an error
                Debug.LogError($"Tried to add {amountToAdd} items to the stack, but there is only space for {StackCounter.MaxLimit - StackCounter.CurrentValue} items.");
                
                // Fill the stack
                StackCounter.Add(StackCounter.MaxLimit - StackCounter.CurrentValue);
            }
            else
            {
                // Increase the stack
                StackCounter.Add(amountToAdd);
            }
        }
    }

    /// <summary>
    /// Represents an item in the inventory, this is the base class for all items' logic.
    /// </summary>
    /// <remarks>
    /// Do not use directly, use <see cref="InventoryItem{TStackType}"/> instead.
    /// </remarks>
    public abstract class InventoryItem
    {
        /// <summary>
        /// Checks if the item is usable.
        /// </summary>
        public bool IsUsable => this is IUsableItem;
        
        /// <summary>
        /// Checks if the item is equippable.
        /// </summary>
        public bool IsEquippable => this is IEquippableItem;

        /// <summary>
        /// Uses the item if it's usable.
        /// Returns true if the item was used.
        /// </summary>
        /// <param name="interactionContext"></param>
        public bool Use([NotNull] IUseItemContext interactionContext)
            => UseAsync(interactionContext).GetAwaiter().GetResult();
        
        /// <summary>
        /// Uses the item if it's usable.
        /// Returns true if the item was used.
        /// </summary>
        public async UniTask<bool> UseAsync([NotNull] IUseItemContext interactionContext)
        {
            if(this is not IUsableItem usableItem) return false;
            return await usableItem.UseItemAsync(interactionContext);
        }
        
        /// <summary>
        /// Checks if the item can be used.
        /// </summary>
        public bool CanBeUsed([NotNull] IUseItemContext interactionContext) 
            => (this as IUsableItem)?.IsItemUsableInContext(interactionContext) ?? true;

        /// <summary>
        /// Equips the item if it's equippable.
        /// Returns true if the item was equipped.
        /// </summary>
        public bool Equip([NotNull] IEquipItemContext interactionContext)
            => EquipAsync(interactionContext).GetAwaiter().GetResult();
        
        /// <summary>
        /// Equips the item if it's equippable.
        /// Returns true if the item was equipped.
        /// </summary>
        public async UniTask<bool> EquipAsync([NotNull] IEquipItemContext interactionContext)
        {
            if(this is not IEquippableItem equippableItem) return false;
            return await equippableItem.EquipItemAsync(interactionContext);
        }
        
        /// <summary>
        /// Checks if the item can be equipped.
        /// </summary>
        public bool CanBeEquipped([NotNull] IEquipItemContext interactionContext) 
            => (this as IEquippableItem)?.IsItemEquippableInContext(interactionContext) ?? false;

        /// <summary>
        /// Unequips the item if it's equippable.
        /// Returns true if the item was unequipped.
        /// </summary>
        public bool Unequip([NotNull] IUnequipItemContext interactionContext)
            => UnequipAsync(interactionContext).GetAwaiter().GetResult();
        
        /// <summary>
        /// Un-equips the item if it's equippable.
        /// Returns true if the item was unequipped.
        /// </summary>
        public async UniTask<bool> UnequipAsync([NotNull] IUnequipItemContext interactionContext)
        {
            if(this is not IEquippableItem equippableItem) return false;
            return await equippableItem.UnequipItemAsync(interactionContext);
        }
        
        /// <summary>
        /// Checks if the item can be unequipped.
        /// </summary>
        public bool CanBeUnequipped([NotNull] IUnequipItemContext interactionContext) 
            => (this as IEquippableItem)?.IsItemUnequippableInContext(interactionContext) ?? false;
        
        /// <summary>
        /// Called when the item is picked up.
        /// </summary>
        protected virtual void OnPickedUp([NotNull] IPickupItemContext interactionContext)
        {
            // Implement your logic here
        }
        
        /// <summary>
        /// Called when the item is dropped.
        /// </summary>
        protected virtual void OnDropped([NotNull] IDropItemContext interactionContext)
        {
            // Implement your logic here
        }
        
        /// <summary>
        /// Amount of items in the stack.
        /// </summary>
        public abstract int AmountInStack { get; }

        /// <summary>
        /// Maximum amount of items that can be stacked.
        /// </summary>
        public abstract int MaxStack { get; }
        
        /// <summary>
        /// Reduces the stack by the specified amount.
        /// </summary>
        public abstract void ReduceStack(int amountToRemove);
        
        /// <summary>
        /// Increases the stack by the specified amount.
        /// </summary>
        public abstract void IncreaseStack(int amountToAdd);

        /// <summary>
        /// Gets the maximum stack size for the specified item type.
        /// </summary>
        public static int GetMaxStack<TItemType>() where TItemType : InventoryItem, new()
        {
            // Create a temporary item reference (unfortunately this is required to get the max stack as
            // Unity version of C# does not support static abstract members)
            TItemType tempItemReference = new TItemType();
            return tempItemReference.MaxStack;
        }
    }
}