using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Inventory;
using FastUnityCreationKit.Inventory.Abstract;
using FastUnityCreationKit.Inventory.Data;
using FastUnityCreationKit.Inventory.Stacking;

namespace FastUnityCreationKit.Tests.Inventory.Data
{
    public sealed class ExampleUsableItem : InventoryItem<SingleItemStack>, IUsableItem
    {
        public int timesUsed = 0;
        
        public bool IsItemUsableInContext(IItemInteractionContext interactionContext)
        {
            return true;
        }

        UniTask IUsableItem.OnUsedAsync(IItemInteractionContext interactionContext)
        {
            timesUsed++;
            return UniTask.CompletedTask;
        }
    }
}