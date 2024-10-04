using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Inventory;
using FastUnityCreationKit.Inventory.Abstract;
using FastUnityCreationKit.Inventory.Context;
using FastUnityCreationKit.Inventory.Stacking;

namespace FastUnityCreationKit.Tests.Inventory.Data
{
    public sealed class ExampleUnusableItem : InventoryItem<SingleItemStack>, IUsableItem
    {
        public int timesUsed = 0;
        
        public bool IsItemUsableInContext(IUseItemContext interactionContext)
        {
            return false;
        }

        UniTask IUsableItem.OnUsedAsync(IUseItemContext interactionContext)
        {
            timesUsed++;
            return UniTask.CompletedTask;
        }
    }
}