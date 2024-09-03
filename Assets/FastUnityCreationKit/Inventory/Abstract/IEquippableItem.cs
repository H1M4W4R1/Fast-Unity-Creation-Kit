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
        public bool CanBeEquipped { get; }
        
        /// <summary>
        /// Checks if the item can be removed.
        /// </summary>
        public bool CanBeUnequipped { get; }

        /// <summary>
        /// Equips the item.
        /// </summary>
        public bool EquipItem()
        {
            // If the item can't be equipped or it's already equipped, return false.
            if (!CanBeEquipped || IsEquipped) return false;
            
            IsEquipped = true;
            OnEquipped();
            return true;

        }
        
        /// <summary>
        /// Removes the item.
        /// </summary>
        public bool UnequipItem()
        {
            // If the item can't be unequipped or it's not equipped, return false.
            if (!CanBeUnequipped || !IsEquipped)
                return false;
            
            IsEquipped = false;
            OnUnequipped();
            return true;
        }
        
        /// <summary>
        /// Called when the item is equipped.
        /// </summary>
        public void OnEquipped();
        
        /// <summary>
        /// Called when the item is removed from worn slot.
        /// </summary>
        public void OnUnequipped();
    }
}