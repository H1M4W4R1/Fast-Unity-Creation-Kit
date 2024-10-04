using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Inventory;
using FastUnityCreationKit.Inventory.Abstract;
using FastUnityCreationKit.Inventory.Context;
using FastUnityCreationKit.Inventory.Stacking;

namespace FastUnityCreationKit.Tests.Inventory.Data
{
    public sealed class ExampleWearableItem : InventoryItem<SingleItemStack>, IEquippableItem
    {
        /// <summary>
        /// Wrapper for <see cref="IEquippableItem.IsEquipped"/>.
        /// </summary>
        public bool IsEquipped => ((IEquippableItem) this).IsEquipped;

        /// <summary>
        /// Determines if the item is equipped.
        /// </summary>
        bool IEquippableItem.IsEquipped { get; set; }

        public int timesEquipped = 0;
        public int timesUnequipped = 0;

        public bool IsItemEquippableInContext(IEquipItemContext interactionContext)
        {
            return true;
        }

        public bool IsItemUnequippableInContext(IUnequipItemContext interactionContext)
        {
            return true;
        }

        public UniTask OnEquippedAsync(IEquipItemContext interactionContext)
        {
            timesEquipped++;
            return UniTask.CompletedTask;
        }

        public UniTask OnUnequippedAsync(IUnequipItemContext interactionContext)
        {
            timesUnequipped++;
            return UniTask.CompletedTask;
        }
    }
}