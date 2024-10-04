namespace FastUnityCreationKit.Inventory.Context
{
    /// <summary>
    /// Represents the context related to equipping an item.
    /// Assumes that player equips item from inventory.
    /// </summary>
    public interface IEquipItemContext : IItemContext, IInventoryContext
    {
        
    }
}