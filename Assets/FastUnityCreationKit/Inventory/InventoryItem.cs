using FastUnityCreationKit.Inventory.Abstract;
using FastUnityCreationKit.Inventory.Data;
using FastUnityCreationKit.Inventory.Stacking;

namespace FastUnityCreationKit.Inventory
{
    /// <summary>
    /// Represents an item in the inventory, this is the base class for all items' logic.
    /// In-game item representations should have no logic, only data/visuals.
    /// <br/><br/>
    /// If you don't want to stack your items <see cref="SingleItemStack"/>
    /// </summary>
    public abstract class InventoryItem<TStackType>
        where TStackType : ItemStackCounterValue, new()
    {
        /// <summary>
        /// Stack counter for the item.
        /// </summary>
        public TStackType StackCounter { get; protected set; } = new TStackType();
        
        /// <summary>
        /// Checks if the item is usable.
        /// </summary>
        public bool IsUsable => this is IUsableItem;
        
        /// <summary>
        /// Checks if the item is equippable.
        /// </summary>
        public bool IsEquippable => this is IEquippableItem;

        /// <summary>
        /// Maximum amount of items that can be stacked.
        /// </summary>
        public int MaxStack => StackCounter.MaxLimit;
        
        /// <summary>
        /// Uses the item if it's usable.
        /// Returns true if the item was used.
        /// </summary>
        public bool Use(IItemInteractionContext interactionContext) 
            => (this as IUsableItem)?.UseItem(interactionContext) ?? false;
        
        /// <summary>
        /// Checks if the item can be used.
        /// </summary>
        public bool CanBeUsed(IItemInteractionContext interactionContext) 
            => (this as IUsableItem)?.IsItemUsableInContext(interactionContext) ?? true;
        
        /// <summary>
        /// Equips the item if it's equippable.
        /// Returns true if the item was equipped.
        /// </summary>
        public bool Equip(IItemInteractionContext interactionContext) 
            => (this as IEquippableItem)?.EquipItem(interactionContext) ?? false;
        
        /// <summary>
        /// Checks if the item can be equipped.
        /// </summary>
        public bool CanBeEquipped(IItemInteractionContext interactionContext) 
            => (this as IEquippableItem)?.IsItemEquippableInContext(interactionContext) ?? false;
        
        /// <summary>
        /// Un-equips the item if it's equippable.
        /// Returns true if the item was unequipped.
        /// </summary>
        public bool Unequip(IItemInteractionContext interactionContext) 
            => (this as IEquippableItem)?.UnequipItem(interactionContext) ?? false;
        
        /// <summary>
        /// Checks if the item can be unequipped.
        /// </summary>
        public bool CanBeUnequipped(IItemInteractionContext interactionContext) 
            => (this as IEquippableItem)?.IsItemUnequippableInContext(interactionContext) ?? false;
        
        /// <summary>
        /// Called when the item is picked up.
        /// </summary>
        protected virtual void OnPickedUp(IItemInteractionContext interactionContext)
        {
            // Implement your logic here
        }
        
        /// <summary>
        /// Called when the item is dropped.
        /// </summary>
        protected virtual void OnDropped(IItemInteractionContext interactionContext)
        {
            // Implement your logic here
        }
    }
}