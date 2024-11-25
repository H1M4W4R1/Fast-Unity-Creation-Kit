using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;
using JetBrains.Annotations;
using Unity.Mathematics;

namespace FastUnityCreationKit.Inventory
{
    /// <summary>
    /// Represents a container for items - this can be an inventory, a chest, a shop, etc.
    /// </summary>
    public abstract class InventoryContainer
    {
        /// <summary>
        /// Returns the items in the container.
        /// </summary>
        [ItemNotNull] [NotNull] protected List<InventoryItem> inventoryItems = new List<InventoryItem>();
        
        /// <summary>
        /// Maximum amount of items that can be stored in the container.
        /// </summary>
        public int MaxItemsInContainer => (this is IWithMaxLimit<int32> maxLimit) ? maxLimit.MaxLimit : int.MaxValue;
        
        /// <summary>
        /// Current amount of items in the container.
        /// </summary>
        public int CurrentItemsInContainer => inventoryItems.Count;
        
        /// <summary>
        /// Current free space in the container (for new item stacks).
        /// Also known as: how many different item types that are not yet in the container can be added to it.
        /// </summary>
        public int FreeSpaceInContainer => MaxItemsInContainer - CurrentItemsInContainer;
        
        /// <summary>
        /// Adds an item to the container.
        /// </summary>
        protected void AddItem(InventoryItem item)
        {
            // Prevent adding the same item twice
            if(HasItem(item)) return; 
            
            // Add the item to the container
            inventoryItems.Add(item);
        }
        
        /// <summary>
        /// Removes an item from the container.
        /// </summary>
        protected void RemoveItem(InventoryItem item)
        {
            // Check if the item is in the container
            if(!HasItem(item)) return;
            
            // Remove the item from the container
            inventoryItems.Remove(item);
        }
        
        /// <summary>
        /// Checks if the container has an item.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected bool HasItem(InventoryItem item) => inventoryItems.Contains(item);

        /// <summary>
        /// Get list of all items of a specific type.
        /// </summary>
        internal List<InventoryItem> GetItemsByType<TItemType>() where TItemType : InventoryItem
        {
            List<InventoryItem> items = new List<InventoryItem>();
            for (int index = 0; index < inventoryItems.Count; index++)
            {
                InventoryItem inventoryItem = inventoryItems[index];
                if (inventoryItem is TItemType)
                    items.Add(inventoryItem);
            }

            return items;
        }
 
        /// <summary>
        /// Check if the container has an item of a specific type with a specific amount.
        /// </summary>
        public bool HasItem<TItemType>(int amount = 1) where TItemType : InventoryItem
        {
            // Check if the amount is valid
            if (amount <= 0) return true;
            
            // Get the amount of items in the container
            int itemCount = 0;
            for (int index = 0; index < inventoryItems.Count; index++)
            {
                // Check if the item is of the required type and
                // add the amount of items in the stack to the count
                InventoryItem inventoryItem = inventoryItems[index];
                if (inventoryItem is TItemType)
                    itemCount += inventoryItem.AmountInStack;
            }

            // Check if the amount of items is greater than the required amount
            return itemCount >= amount;
        }
        
        /// <summary>
        /// Count the amount of items of a specific type in the container.
        /// </summary>
        public int CountItem<TItemType>() where TItemType : InventoryItem
        {
            int itemCount = 0;
            for (int index = 0; index < inventoryItems.Count; index++)
            {
                InventoryItem inventoryItem = inventoryItems[index];
                if (inventoryItem is TItemType)
                    itemCount += inventoryItem.AmountInStack;
            }

            return itemCount;
        }
        
        /// <summary>
        /// Check if the container has space for a specific amount of items of a specific type.
        /// </summary>
        public bool HasSpaceForItem<TItemType>(int amount = 1) where TItemType : InventoryItem, new()
        {
            // Check if the amount is valid
            if (amount <= 0) return true;
            
            // Get the amount of items in the container
            int freeSpace = 0;
            for (int index = 0; index < inventoryItems.Count; index++)
            {
                // Check if the item is of the required type and
                // add the amount of items in the stack to the count
                InventoryItem inventoryItem = inventoryItems[index];
                if (inventoryItem is TItemType)
                {
                    // Add free space in the stack to the count
                    freeSpace += inventoryItem.MaxStack - inventoryItem.AmountInStack;
                }
            }
            
            // Add free space in the container to the count
            freeSpace += FreeSpaceInContainer * InventoryItem.GetMaxStack<TItemType>();

            // Check if the amount of items is greater than the required amount
            return freeSpace >= amount;
        }
        
        /// <summary>
        /// Removes an item of a specific type with a specific amount from the container.
        /// Returns true if the item was removed, false otherwise (e.g. not enough items in the container).
        /// </summary>
        public bool TryTakeItem<TItemType>(int amount = 1) where TItemType : InventoryItem
        {
            // Check if amount is valid
            if (amount <= 0) return true;
            
            // Check if the container has the required amount of items
            if (!HasItem<TItemType>(amount)) return false;
            
            // Get all items of the required type
            List<InventoryItem> items = GetItemsByType<TItemType>();
            
            // Reorder the items by stack size
            items.Sort((item1, item2) => item1.AmountInStack.CompareTo(item2.AmountInStack));
            
            // Start removing items
            while (amount > 0)
            {
                // Get the item with the smallest stack size
                InventoryItem item = items[0];
                
                // Get the amount of items to remove
                int amountToRemove = math.min(amount, item.AmountInStack);
                
                // Remove the items
                item.ReduceStack(amountToRemove);
                
                // Update the amount of items left to remove
                amount -= amountToRemove;
                
                // Remove the item from the list if the stack is empty
                if (item.AmountInStack == 0) items.Remove(item);
            }
            
            // Cleanup items with empty stacks
            CleanupItemsWithEmptyStacks();
            return true;
        }
        
        /// <summary>
        /// Try to add an item of a specific type with a specific amount to the container.
        /// Returns true if the item was added successfully, false otherwise.
        /// </summary>
        public bool TryAddItem<TItemType>(int amount = 1) where TItemType : InventoryItem, new()
        {
            // Check if amount is valid
            if (amount <= 0) return true;
            
            // Check if the container has space for the required amount of items
            if (!HasSpaceForItem<TItemType>(amount)) return false;
            
            // Get all items of the required type
            List<InventoryItem> items = GetItemsByType<TItemType>();
            
            // Reorder the items by stack size
            items.Sort((item1, item2) => item1.AmountInStack.CompareTo(item2.AmountInStack));
            
            // Start adding items - increase stacks to full before adding new items
            // to avoid having multiple stacks of the same item
            while (amount > 0 && items.Count > 0)
            {
                // Get the item with the smallest stack size
                InventoryItem item = items[0];
                
                // Get the amount of items to add
                int amountToAdd = math.min(amount, item.MaxStack - item.AmountInStack);
                
                // Add the items
                item.IncreaseStack(amountToAdd);
                
                // Update the amount of items left to add
                amount -= amountToAdd;
                
                // Remove the item from the list if the stack is full
                if (item.AmountInStack == item.MaxStack) items.Remove(item);
            }
            
            // Add new items if there are still items left to add
            while (amount > 0)
            {
                // Create a new item
                InventoryItem item = new TItemType();
                
                // Get the amount of items to add
                int amountToAdd = math.min(amount, item.MaxStack);
                
                // Add the items
                item.IncreaseStack(amountToAdd);
                
                // Update the amount of items left to add
                amount -= amountToAdd;
                
                // Add the item to the container
                AddItem(item);
            }
            
            // Cleanup items with empty stacks (just in case something went wrong)
            CleanupItemsWithEmptyStacks();
            return true;
        }
        
        /// <summary>
        /// Removes all items of a specific type from the container.
        /// It is recommended to use this method only for cleanup purposes.
        /// </summary>
        public bool ClearItem<TItemType>() where TItemType : InventoryItem
        {
            // Loop through all items in the container
            for (int index = inventoryItems.Count - 1; index >= 0; index--)
            {
                // Check if the item is of the required type
                InventoryItem inventoryItem = inventoryItems[index];
                if (inventoryItem is TItemType)
                {
                    // Remove the item from the container
                    RemoveItem(inventoryItem);
                }
            }
            
            // Cleanup items with empty stacks
            CleanupItemsWithEmptyStacks();
            return true;
        }

        /// <summary>
        /// Clears the container - removes all items from it.
        /// </summary>
        public void Clear()
        {
            inventoryItems.Clear();
        }
        
        /// <summary>
        /// Removes all items that have an empty stack from the container (if item stack counter is 0).
        /// </summary>
        private void CleanupItemsWithEmptyStacks()
        {
            // Reverse loop to avoid issues with removing items
            for (int index = inventoryItems.Count - 1; index >= 0; index--)
            {
                InventoryItem inventoryItem = inventoryItems[index];
                if (inventoryItem.AmountInStack == 0)
                    RemoveItem(inventoryItem);
            }
        }
    }
}