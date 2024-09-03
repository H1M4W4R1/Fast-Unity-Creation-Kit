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
        public bool CanBeUsed { get; }

        /// <summary>
        /// Uses the item.
        /// </summary>
        public bool UseItem()
        {
            // If the item can't be used, return false.
            if (!CanBeUsed) return false;
            
            OnUsed();
            return true;

        }
        
        /// <summary>
        /// Called when the item is used.
        /// </summary>
        protected void OnUsed();
    }
}