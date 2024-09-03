using FastUnityCreationKit.Inventory.Data;

namespace FastUnityCreationKit.Inventory.Abstract
{
    /// <summary>
    /// Represents an item that can be used.
    /// </summary>
    public interface IUsableItem
    {
        /// <summary>
        /// Checks if the item can be used.
        /// </summary>
        public bool IsItemUsableInContext(IItemInteractionContext interactionContext);

        /// <summary>
        /// Uses the item.
        /// </summary>
        public bool UseItem(IItemInteractionContext interactionContext)
        {
            // If the item can't be used, return false.
            if (!IsItemUsableInContext(interactionContext)) return false;
            
            OnUsed(interactionContext);
            return true;

        }
        
        /// <summary>
        /// Called when the item is used.
        /// </summary>
        protected void OnUsed(IItemInteractionContext interactionContext);
    }
}