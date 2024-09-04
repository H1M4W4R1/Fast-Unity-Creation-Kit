using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;
using FastUnityCreationKit.Inventory;

namespace FastUnityCreationKit.Tests.Inventory.Data
{
    public class ExampleTwoItemsContainer : InventoryContainer, IWithMaxLimit<int32>
    {
        public int32 MaxLimit => 2;
    }
}