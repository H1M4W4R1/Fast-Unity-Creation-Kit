using FastUnityCreationKit.Inventory;
using FastUnityCreationKit.Inventory.Abstract;
using FastUnityCreationKit.Inventory.Data;
using FastUnityCreationKit.Inventory.Stacking;

namespace FastUnityCreationKit.Tests.Inventory.Data
{
    public sealed class ExampleNonWearableItem : InventoryItem<SingleItemStack>, IEquippableItem
    {
        /// <summary>
        /// Wrapper for <see cref="IEquippableItem.IsEquipped"/>.
        /// </summary>
        public bool IsEquipped => ((IEquippableItem)this).IsEquipped;
        
        /// <summary>
        /// Determines if the item is equipped.
        /// </summary>
        bool IEquippableItem.IsEquipped { get; set; }
        
        public int timesEquipped = 0;
        public int timesUnequipped = 0;

        public bool IsItemEquippableInContext(IItemInteractionContext interactionContext)
        {
            return false;
        }

        public bool IsItemUnequippableInContext(IItemInteractionContext interactionContext)
        {
            return false;
        }

        public void OnEquipped(IItemInteractionContext interactionContext)
        {
            timesEquipped++;
        }

        public void OnUnequipped(IItemInteractionContext interactionContext)
        {
            timesUnequipped++;
        }
    }
}