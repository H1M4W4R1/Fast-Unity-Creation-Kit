namespace FastUnityCreationKit.Inventory.Data
{
    /// <summary>
    /// Represents the context of an item interaction - this is used to pass data to the item when interacting with it
    /// for example, when using an item, equipping it, etc.
    /// <br/><br/>
    /// This is an interface to provide capability of creating multiple contexts for different interactions eg.
    /// one context for using an item, another for equipping it, etc. or one context for using an item by a player,
    /// another for using it by an NPC, etc.
    /// <br/><br/>
    /// Another use possibility is to create a single context that can be used for all interactions.
    /// </summary>
    /// <remarks>
    /// This should be implemented as a struct to avoid heap allocations.
    /// </remarks>
    public interface IItemInteractionContext
    {
        
        
    }
}