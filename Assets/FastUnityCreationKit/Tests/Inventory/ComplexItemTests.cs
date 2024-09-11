using FastUnityCreationKit.Inventory.Abstract;
using FastUnityCreationKit.Tests.Inventory.Data;
using NUnit.Framework;
#pragma warning disable CS0184 // 'is' expression's given expression is never of the provided type

namespace FastUnityCreationKit.Tests.Inventory
{
    [TestFixture]
    public class ComplexItemTests : TestFixtureBase
    {
        
        [Test]
        public void EquipItem_EquipsItem()
        {
            // Arrange
            ExampleWearableItem wearable = new ExampleWearableItem();
            
            // Act
            wearable.EquipAsync(null);
            
            // Assert
            Assert.IsTrue(wearable.IsEquipped);
            Assert.AreEqual(wearable.timesEquipped, 1);
        }
        
        [Test]
        public void EquipItem_DoesNotEquipItem_IfNotEquippable()
        {
            // Arrange
            ExampleNonWearableItem item = new ExampleNonWearableItem();
            
            // Act
            item.EquipAsync(null);
            
            // Assert
            Assert.IsFalse(item.IsEquipped);
            Assert.AreEqual(item.timesEquipped, 0);
        }
        
        [Test]
        public void EquipItem_EquipsItem_OnlyOnce()
        {
            // Arrange
            ExampleWearableItem wearable = new ExampleWearableItem();
            
            // Act
            wearable.EquipAsync(null);
            wearable.EquipAsync(null);
            
            // Assert
            Assert.IsTrue(wearable.IsEquipped);
            Assert.AreEqual(wearable.timesEquipped, 1);
        }
        
        [Test]
        public void UnequipItem_UnequipsItem()
        {
            // Arrange
            ExampleWearableItem wearable = new ExampleWearableItem();
            wearable.EquipAsync(null);
            
            // Act
            wearable.UnequipAsync(null);
            
            // Assert
            Assert.IsFalse(wearable.IsEquipped);
            Assert.AreEqual(wearable.timesUnequipped, 1);
        }
        
        [Test]
        public void UnequipItem_DoesNotUnequipItem_IfNotEquippable()
        {
            // Arrange
            ExampleNonWearableItem item = new ExampleNonWearableItem();
            
            // Act
            item.UnequipAsync(null);
            
            // Assert
            Assert.IsFalse(item.IsEquipped);
            Assert.AreEqual(item.timesUnequipped, 0);
        }
        
        [Test]
        public void UnequipItem_UnequipsItem_OnlyOnce()
        {
            // Arrange
            ExampleWearableItem wearable = new ExampleWearableItem();
            wearable.EquipAsync(null);
            
            // Act
            wearable.UnequipAsync(null);
            wearable.UnequipAsync(null);
            
            // Assert
            Assert.IsFalse(wearable.IsEquipped);
            Assert.AreEqual(wearable.timesUnequipped, 1);
        }
        
        [Test]
        public void UseItem_UsesItem()
        {
            // Arrange
            ExampleUsableItem usableItem = new ExampleUsableItem();
            
            // Act
            usableItem.UseAsync(null);
            
            // Assert
            Assert.AreEqual(usableItem.timesUsed, 1);
        }
        
        [Test]
        public void UseItem_DoesNotUseItem_IfNotUsable()
        {
            // Arrange
            ExampleUnusableItem nonUsableItem = new ExampleUnusableItem();
            
            // Act
            nonUsableItem.UseAsync(null);
            
            // Assert
            Assert.AreEqual(nonUsableItem.timesUsed, 0);
        }
        
        [Test]
        public void EquipItem_DoesNotThrow_IfItemDoesNotImplementIEquippableItem()
        {
            Assert.DoesNotThrow(() =>
            {
                // Arrange
                ExampleSingleStackItem item = new ExampleSingleStackItem();

                // Assert
                Assert.IsFalse(item is IEquippableItem);
                
                // Act
                item.EquipAsync(null);
            });
        }
        
        [Test]
        public void UnequipItem_DoesNotThrow_IfItemDoesNotImplementIEquippableItem()
        {
            Assert.DoesNotThrow(() =>
            {
                // Arrange
                ExampleSingleStackItem item = new ExampleSingleStackItem();

                // Assert
                Assert.IsFalse(item is IEquippableItem);
                
                // Act
                item.UnequipAsync(null);
            });
        }

        [Test]
        public void UseItem_DoesNotThrow_IfItemDoesNotImplementIUsableItem()
        {
            Assert.DoesNotThrow(() =>
            {
                // Arrange
                ExampleSingleStackItem item = new ExampleSingleStackItem();

                // Assert
                Assert.IsFalse(item is IUsableItem);
                
                // Act
                item.UseAsync(null);
            });
        }
        
        [Test]
        public void IsUsable_ReturnsCorrectValue()
        {
            // Arrange
            ExampleUsableItem usableItem = new ExampleUsableItem();
            ExampleUnusableItem unusableItem = new ExampleUnusableItem();
            ExampleSingleStackItem singleStackItem = new ExampleSingleStackItem();
            
            // Assert
            Assert.IsTrue(usableItem.IsUsable);
            Assert.IsTrue(unusableItem.IsUsable);
            Assert.IsFalse(singleStackItem.IsUsable);
        }
        
        [Test]
        public void IsEquippable_ReturnsCorrectValue()
        {
            // Arrange
            ExampleWearableItem wearableItem = new ExampleWearableItem();
            ExampleNonWearableItem nonWearableItem = new ExampleNonWearableItem();
            ExampleSingleStackItem singleStackItem = new ExampleSingleStackItem();
            
            // Assert
            Assert.IsTrue(wearableItem.IsEquippable);
            Assert.IsTrue(nonWearableItem.IsEquippable);
            Assert.IsFalse(singleStackItem.IsEquippable);
        }
        
        [Test]
        public void CanBeUsed_ReturnsCorrectValue()
        {
            // Arrange
            ExampleUsableItem usableItem = new ExampleUsableItem();
            ExampleUnusableItem unusableItem = new ExampleUnusableItem();
            
            // Act
            bool usableItemCanBeUsed = usableItem.CanBeUsed(null);
            bool unusableItemCanBeUsed = unusableItem.CanBeUsed(null);
            
            // Assert
            Assert.IsTrue(usableItemCanBeUsed);
            Assert.IsFalse(unusableItemCanBeUsed);
        }
        
        [Test]
        public void CanBeEquipped_ReturnsCorrectValue()
        {
            // Arrange
            ExampleWearableItem wearableItem = new ExampleWearableItem();
            ExampleNonWearableItem nonWearableItem = new ExampleNonWearableItem();
            
            // Act
            bool wearableItemCanBeEquipped = wearableItem.CanBeEquipped(null);
            bool nonWearableItemCanBeEquipped = nonWearableItem.CanBeEquipped(null);
            
            // Assert
            Assert.IsTrue(wearableItemCanBeEquipped);
            Assert.IsFalse(nonWearableItemCanBeEquipped);
        }
        
        [Test]
        public void CanBeUnequipped_ReturnsCorrectValue()
        {
            // Arrange
            ExampleWearableItem wearableItem = new ExampleWearableItem();
            ExampleNonWearableItem nonWearableItem = new ExampleNonWearableItem();
            
            // Act
            bool wearableItemCanBeUnequipped = wearableItem.CanBeUnequipped(null);
            bool nonWearableItemCanBeUnequipped = nonWearableItem.CanBeUnequipped(null);
            
            // Assert
            Assert.IsTrue(wearableItemCanBeUnequipped);
            Assert.IsFalse(nonWearableItemCanBeUnequipped);
        }
    }
}