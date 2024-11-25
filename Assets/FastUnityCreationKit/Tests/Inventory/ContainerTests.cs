using System.Collections.Generic;
using FastUnityCreationKit.Inventory;
using FastUnityCreationKit.Tests.Inventory.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Inventory
{
    [TestFixture]
    public class ContainerTests : TestFixtureBase
    {
        [Test]
        public void MaxItemsInContainer_ReturnsCorrectValue()
        {
            // Arrange
            ExampleSingleItemContainer firstContainer = new ExampleSingleItemContainer();
            ExampleTwoItemsContainer secondContainer = new ExampleTwoItemsContainer();
            ExampleInfiniteItemsContainer thirdContainer = new ExampleInfiniteItemsContainer();
            
            // Act
            int firstContainerMaxItems = firstContainer.MaxItemsInContainer;
            int secondContainerMaxItems = secondContainer.MaxItemsInContainer;
            int thirdContainerMaxItems = thirdContainer.MaxItemsInContainer;
            
            // Assert
            Assert.AreEqual(firstContainerMaxItems, 1);
            Assert.AreEqual(secondContainerMaxItems, 2);
            Assert.AreEqual(thirdContainerMaxItems, int.MaxValue);
        }
        
        [Test]
        public void CurrentItemsInContainer_ReturnsCorrectValue()
        {
            // Arrange
            ExampleSingleItemContainer firstContainer = new ExampleSingleItemContainer();
            ExampleTwoItemsContainer secondContainer = new ExampleTwoItemsContainer();
            ExampleInfiniteItemsContainer thirdContainer = new ExampleInfiniteItemsContainer();
            
            // Act
            int firstContainerCurrentItems = firstContainer.CurrentItemsInContainer;
            int secondContainerCurrentItems = secondContainer.CurrentItemsInContainer;
            int thirdContainerCurrentItems = thirdContainer.CurrentItemsInContainer;
            
            // Assert
            Assert.AreEqual(firstContainerCurrentItems, 0);
            Assert.AreEqual(secondContainerCurrentItems, 0);
            Assert.AreEqual(thirdContainerCurrentItems, 0);
            
            // Act
            firstContainer.TryAddItem<ExampleSingleStackItem>();
            secondContainer.TryAddItem<ExampleSingleStackItem>();
            thirdContainer.TryAddItem<ExampleSingleStackItem>();
            
            // Assert
            Assert.AreEqual(firstContainer.CurrentItemsInContainer, 1);
            Assert.AreEqual(secondContainer.CurrentItemsInContainer, 1);
            Assert.AreEqual(thirdContainer.CurrentItemsInContainer, 1);
        }
        
        [Test]
        public void AddItem_AddsItemToContainer()
        {
            // Arrange
            ExampleSingleItemContainer container = new ExampleSingleItemContainer();
            
            // Act
            bool success = container.TryAddItem<ExampleSingleStackItem>();
            
            // Assert
            Assert.IsTrue(success);
        }
        
        [Test]
        public void AddItem_DoesNotAddItemToContainer_IfFull()
        {
            // Arrange
            ExampleSingleItemContainer container = new ExampleSingleItemContainer();
            container.TryAddItem<ExampleSingleStackItem>();
            
            Assert.AreEqual(1, container.CurrentItemsInContainer);
            
            // Act
            bool success = container.TryAddItem<ExampleSingleStackItem>();
            
            // Assert
            Assert.IsFalse(success);
        }
        
        [Test]
        public void HasItem_ReturnsTrue_IfItemInContainer()
        {
            // Arrange
            ExampleSingleItemContainer container = new ExampleSingleItemContainer();
            container.TryAddItem<ExampleSingleStackItem>();
            
            // Act
            bool hasItem = container.HasItem<ExampleSingleStackItem>();
            
            // Assert
            Assert.IsTrue(hasItem);
        }
        
        [Test]
        public void HasItem_ReturnsFalse_IfItemNotInContainer()
        {
            // Arrange
            ExampleSingleItemContainer container = new ExampleSingleItemContainer();
            
            // Act
            bool hasItem = container.HasItem<ExampleSingleStackItem>();
            
            // Assert
            Assert.IsFalse(hasItem);
        }
        
        [Test]
        public void HasItem_ReturnsFalse_IfNotEnoughItemsInContainer()
        {
            // Arrange
            ExampleTwoItemsContainer container = new ExampleTwoItemsContainer();
            container.TryAddItem<ExampleSingleStackItem>();
            
            // Act
            bool hasItem = container.HasItem<ExampleSingleStackItem>(2);
            
            // Assert
            Assert.IsFalse(hasItem);
        }
        
        [Test]
        public void HasSpaceForItem_ReturnsTrue_IfEnoughSpaceInContainer()
        {
            // Arrange
            ExampleTwoItemsContainer container = new ExampleTwoItemsContainer();
            
            // Act
            bool hasSpace = container.HasSpaceForItem<ExampleSingleStackItem>();
            
            // Assert
            Assert.IsTrue(hasSpace);
        }
        
        [Test]
        public void HasSpaceForItem_ReturnsFalse_IfNotEnoughSpaceInContainer()
        {
            // Arrange
            ExampleSingleItemContainer container = new ExampleSingleItemContainer();
            container.TryAddItem<ExampleSingleStackItem>();
            
            // Act
            bool hasSpace = container.HasSpaceForItem<ExampleSingleStackItem>();
            
            // Assert
            Assert.IsFalse(hasSpace);
        }
        
        [Test]
        public void HasSpaceForItem_ReturnsTrue_IfHasSpaceForMultipleItems()
        {
            // Arrange
            ExampleTwoItemsContainer container = new ExampleTwoItemsContainer();
            
            // Act
            bool hasSpace = container.HasSpaceForItem<ExampleSingleStackItem>(2);
            
            // Assert
            Assert.IsTrue(hasSpace);
        }
        
        [Test]
        public void HasSpaceForItem_ReturnsFalse_IfNotEnoughSpaceForMultipleItems()
        {
            // Arrange
            ExampleTwoItemsContainer container = new ExampleTwoItemsContainer();
            container.TryAddItem<ExampleSingleStackItem>();
            
            // Act
            bool hasSpace = container.HasSpaceForItem<ExampleSingleStackItem>(3);
            
            // Assert
            Assert.IsFalse(hasSpace);
        }
        
        [Test]
        public void TryTakeItem_ReturnsFalse_IfItemNotInContainer()
        {
            // Arrange
            ExampleSingleItemContainer container = new ExampleSingleItemContainer();
            
            // Act
            bool success = container.TryTakeItem<ExampleSingleStackItem>();
            
            // Assert
            Assert.IsFalse(success);
        }
        
        [Test]
        public void TryTakeItem_TakesItem_AndReturnsTrue_IfItemInContainer()
        {
            // Arrange
            ExampleSingleItemContainer container = new ExampleSingleItemContainer();
            container.TryAddItem<ExampleSingleStackItem>();
            
            // Act
            bool success = container.TryTakeItem<ExampleSingleStackItem>();
            
            // Assert
            Assert.IsFalse(container.HasItem<ExampleSingleStackItem>());
            Assert.IsTrue(success);
        }
        
        [Test]
        public void TryTakeItem_TakesItem_AndReturnsTrue_IfEnoughItemsInContainer()
        {
            // Arrange
            ExampleTwoItemsContainer container = new ExampleTwoItemsContainer();
            container.TryAddItem<ExampleSingleStackItem>();
            container.TryAddItem<ExampleSingleStackItem>();
            
            // Act
            bool success = container.TryTakeItem<ExampleSingleStackItem>(2);
            
            // Assert
            Assert.IsFalse(container.HasItem<ExampleSingleStackItem>());
            Assert.IsTrue(success);
        }
        
        [Test]
        public void TryTakeItem_DoesNotTakeItem_AndReturnsFalse_IfNotEnoughItemsInContainer()
        {
            // Arrange
            ExampleTwoItemsContainer container = new ExampleTwoItemsContainer();
            container.TryAddItem<ExampleSingleStackItem>();
            
            // Act
            bool success = container.TryTakeItem<ExampleSingleStackItem>(2);
            
            // Assert
            Assert.IsTrue(container.HasItem<ExampleSingleStackItem>());
            Assert.IsFalse(success);
        }
        
        [Test]
        public void Clear_ClearsContainer()
        {
            // Arrange
            ExampleTwoItemsContainer container = new ExampleTwoItemsContainer();
            container.TryAddItem<ExampleSingleStackItem>();
            container.TryAddItem<ExampleSingleStackItem>();
            
            // Act
            container.Clear();
            
            // Assert
            Assert.AreEqual(0, container.CurrentItemsInContainer);
            Assert.IsFalse(container.HasItem<ExampleSingleStackItem>());
        }
        
        [Test]
        public void CountItems_ReturnsCorrectValue()
        {
            // Arrange
            ExampleTwoItemsContainer container = new ExampleTwoItemsContainer();
            container.TryAddItem<ExampleSingleStackItem>();
            container.TryAddItem<ExampleSingleStackItem>();
            
            // Act
            int count = container.CountItem<ExampleSingleStackItem>();
            
            // Assert
            Assert.AreEqual(2, count);
        }
        
        [Test]
        public void AddItem_IncreasesStackCount_IfItemInContainer()
        {
            // Arrange
            ExampleTwoItemsContainer container = new ExampleTwoItemsContainer();
            container.TryAddItem<ExampleStackableItem>();
            
            // Act
            container.TryAddItem<ExampleStackableItem>();
            
            // Assert
            Assert.AreEqual(2, container.CountItem<ExampleStackableItem>());
            Assert.IsTrue(container.HasSpaceForItem<ExampleSingleStackItem>());
            
            var items = container.GetItemsByType<ExampleStackableItem>();
            Assert.AreEqual(1, items.Count);
            
            ExampleStackableItem item = (ExampleStackableItem) items[0];
            Assert.AreEqual(2, item.AmountInStack);
        }
        
        [Test]
        public void TakeItem_TakesItemFromStack_IfItemInContainer()
        {
            // Arrange
            ExampleTwoItemsContainer container = new ExampleTwoItemsContainer();
            container.TryAddItem<ExampleStackableItem>();
            container.TryAddItem<ExampleStackableItem>();
            
            // Act
            container.TryTakeItem<ExampleStackableItem>();
            
            // Assert
            Assert.AreEqual(1, container.CountItem<ExampleStackableItem>());
            Assert.IsTrue(container.HasItem<ExampleStackableItem>());
            
            List<InventoryItem> items = container.GetItemsByType<ExampleStackableItem>();
            Assert.AreEqual(1, items.Count);
            
            ExampleStackableItem item = (ExampleStackableItem) items[0];
            Assert.AreEqual(1, item.AmountInStack);
        }
        
        [Test]
        public void TakeItem_TakesItemFromStack_AndRemovesStack_IfLastItemInStack()
        {
            // Arrange
            ExampleTwoItemsContainer container = new ExampleTwoItemsContainer();
            container.TryAddItem<ExampleStackableItem>(2);
            
            // Act
            container.TryTakeItem<ExampleStackableItem>();
            
            Assert.IsTrue(container.HasItem<ExampleStackableItem>());
            Assert.AreEqual(1, container.CountItem<ExampleStackableItem>());
  
            // Act
            container.TryTakeItem<ExampleStackableItem>();
            
            // Assert
            Assert.AreEqual(0, container.CountItem<ExampleStackableItem>());
            Assert.IsFalse(container.HasItem<ExampleStackableItem>());
            
            List<InventoryItem> items = container.GetItemsByType<ExampleStackableItem>();
            Assert.AreEqual(0, items.Count);
        }
        
        [Test]
        public void AddItem_IncreasesStack_AndAddsOverflowAsSecondaryItem_IfItemInContainer()
        {
            // Arrange
            ExampleTwoItemsContainer container = new ExampleTwoItemsContainer();
            container.TryAddItem<ExampleStackableItem>(32);
            
            
            // Act
            container.TryAddItem<ExampleStackableItem>(64);
            
            // Assert
            Assert.AreEqual(96, container.CountItem<ExampleStackableItem>());
            
            List<InventoryItem> items = container.GetItemsByType<ExampleStackableItem>();
            Assert.AreEqual(2, items.Count);
            
            // Check if one item has 64 and the other 32
            ExampleStackableItem firstItem = (ExampleStackableItem) items[0];
            ExampleStackableItem secondItem = (ExampleStackableItem) items[1];

            // If the first item has 64 and the second has 32, or vice versa, the test passes
            if ((firstItem.AmountInStack != 64 || secondItem.AmountInStack != 32) &&
                (firstItem.AmountInStack != 32 || secondItem.AmountInStack != 64)) Assert.Fail();
        }
    }
}