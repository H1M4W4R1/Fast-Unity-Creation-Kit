using FastUnityCreationKit.Inventory;
using FastUnityCreationKit.Tests.Inventory.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Inventory
{
    [TestFixture]
    public sealed class BasicItemTests : TestFixtureBase
    {
        
        [Test]
        public void MaxStack_ReturnsCorrectValue()
        {
            // Arrange
            ExampleSingleStackItem singleStackItem = new ExampleSingleStackItem();
            ExampleStackableItem stackableItem = new ExampleStackableItem();
            
            // Act
            int maxStack = singleStackItem.MaxStack;
            int maxStack2 = stackableItem.MaxStack;
            
            // Assert
            Assert.AreEqual(maxStack, 1);
            Assert.AreEqual(maxStack2, 64);
        }
        
        [Test]
        public void GetMaxStack_ReturnsCorrectValue()
        {
            // Act
            int maxStack = InventoryItem.GetMaxStack<ExampleSingleStackItem>();
            int maxStack2 = InventoryItem.GetMaxStack<ExampleStackableItem>();
            
            // Assert
            Assert.AreEqual(maxStack, 1);
            Assert.AreEqual(maxStack2, 64);
        }
        
        [Test]
        public void IncreaseStack_IncreasesStack()
        {
            // Arrange
            ExampleStackableItem stackableItem = new ExampleStackableItem();
            
            // Act
            stackableItem.IncreaseStack(32);
            
            // Assert
            Assert.AreEqual(stackableItem.AmountInStack, 32);
        }
        
        [Test]
        public void IncreaseStack_IncreasesStackToFull_IfAmountIsGreaterThanMaxStack()
        {
            // Arrange
            ExampleStackableItem stackableItem = new ExampleStackableItem();
            
            // Act
            stackableItem.IncreaseStack(128);
            
            // Assert
            Assert.AreEqual(stackableItem.AmountInStack, 64);
        }
        
        [Test]
        public void IncreaseStack_DoesNotIncreaseStack_IfAmountIsZero()
        {
            // Arrange
            ExampleStackableItem stackableItem = new ExampleStackableItem();
            
            // Act
            stackableItem.IncreaseStack(0);
            
            // Assert
            Assert.AreEqual(stackableItem.AmountInStack, 0);
        }
        
        [Test]
        public void DecreaseStack_DecreasesStack()
        {
            // Arrange
            ExampleStackableItem stackableItem = new ExampleStackableItem();
            stackableItem.IncreaseStack(64);
            
            // Act
            stackableItem.ReduceStack(32);
            
            // Assert
            Assert.AreEqual(stackableItem.AmountInStack, 32);
        }
        
        [Test]
        public void DecreaseStack_DecreasesStackToZero_IfAmountIsGreaterThanCurrentStack()
        {
            // Arrange
            ExampleStackableItem stackableItem = new ExampleStackableItem();
            stackableItem.IncreaseStack(32);
            
            // Act
            stackableItem.ReduceStack(64);
            
            // Assert
            Assert.AreEqual(stackableItem.AmountInStack, 0);
        }
        
        [Test]
        public void DecreaseStack_DoesNotDecreaseStack_IfAmountIsZero()
        {
            // Arrange
            ExampleStackableItem stackableItem = new ExampleStackableItem();
            stackableItem.IncreaseStack(32);
            
            // Act
            stackableItem.ReduceStack(0);
            
            // Assert
            Assert.AreEqual(stackableItem.AmountInStack, 32);
        }
        
        [Test]
        public void AmountInStack_NewItemHasNoItemsInStack()
        {
            // Arrange
            ExampleSingleStackItem singleStackItem = new ExampleSingleStackItem();
            ExampleStackableItem stackableItem = new ExampleStackableItem();
            
            // Act
            int amountInStack = singleStackItem.AmountInStack;
            int amountInStack2 = stackableItem.AmountInStack;
            
            // Assert
            Assert.AreEqual(amountInStack, 0);
            Assert.AreEqual(amountInStack2, 0);
        }
    }
}