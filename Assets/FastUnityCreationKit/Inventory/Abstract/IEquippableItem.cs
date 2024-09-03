using FastUnityCreationKit.Inventory.Data;

namespace FastUnityCreationKit.Inventory.Abstract
{
    /// <summary>
    /// Represents an item that can be equipped.
    /// </summary>
    public interface IEquippableItem 
    {
        /// <summary>
        /// Checks if the item is equipped.
        /// </summary>
        public bool IsEquipped { get; protected set; }
        
        /// <summary>
        /// Checks if the item can be equipped.
        /// </summary>
        public bool IsItemEquippableInContext(IItemInteractionContext interactionContext);
        
        /// <summary>
        /// Checks if the item can be removed.
        /// </summary>
        public bool IsItemUnequippableInContext(IItemInteractionContext interactionContext);

        /// <summary>
        /// Equips the item.
        /// </summary>
        public bool EquipItem(IItemInteractionContext interactionContext)
        {
            // If the item can't be equipped or it's already equipped, return false.
            if (!IsItemEquippableInContext(interactionContext) || IsEquipped) return false;
            
            IsEquipped = true;
            OnEquipped(interactionContext);
            return true;

        }
        
        /// <summary>
        /// Removes the item.
        /// </summary>
        public bool UnequipItem(IItemInteractionContext interactionContext)
        {
            // If the item can't be unequipped or it's not equipped, return false.
            if (!IsItemUnequippableInContext(interactionContext) || !IsEquipped)
                return false;
            
            IsEquipped = false;
            OnUnequipped(interactionContext);
            return true;
        }
        
        /// <summary>
        /// Called when the item is equipped.
        /// </summary>
        public void OnEquipped(IItemInteractionContext interactionContext);
        
        /// <summary>
        /// Called when the item is removed from worn slot.
        /// </summary>
        public void OnUnequipped(IItemInteractionContext interactionContext);
    }
}