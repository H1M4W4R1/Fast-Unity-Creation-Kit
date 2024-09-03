using FastUnityCreationKit.Inventory.Abstract;
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
        /// Checks if the item can be used.
        /// If not usable, this will always return true.
        /// </summary>
        public bool CanBeUsed => IsUsable && ((this as IUsableItem)?.CanBeUsed ?? true);
        
        /// <summary>
        /// Checks if the item can be equipped.
        /// If not equippable, this will always return false.
        /// </summary>
        public bool CanBeEquipped => IsEquippable && ((this as IEquippableItem)?.CanBeEquipped ?? false);
        
        /// <summary>
        /// Checks if the item can be removed.
        /// If not equippable, this will always return false.
        /// </summary>
        public bool CanBeUnequipped => IsEquippable && ((this as IEquippableItem)?.CanBeUnequipped ?? false);
        
        /// <summary>
        /// Maximum amount of items that can be stacked.
        /// </summary>
        public int MaxStack => StackCounter.MaxLimit;
        
        /// <summary>
        /// Uses the item if it's usable.
        /// Returns true if the item was used.
        /// </summary>
        public bool Use() => (this as IUsableItem)?.UseItem() ?? false;
        
        /// <summary>
        /// Equips the item if it's equippable.
        /// Returns true if the item was equipped.
        /// </summary>
        public bool Equip() => (this as IEquippableItem)?.EquipItem() ?? false;
        
        /// <summary>
        /// Un-equips the item if it's equippable.
        /// Returns true if the item was unequipped.
        /// </summary>
        public bool Unequip() => (this as IEquippableItem)?.UnequipItem() ?? false;
        
        /// <summary>
        /// Called when the item is picked up.
        /// </summary>
        protected virtual void OnPickedUp()
        {
            // Implement your logic here
        }
        
        /// <summary>
        /// Called when the item is dropped.
        /// </summary>
        protected virtual void OnDropped()
        {
            // Implement your logic here
        }
    }
}