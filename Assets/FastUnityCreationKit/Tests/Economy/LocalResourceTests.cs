using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Economy;
using FastUnityCreationKit.Economy.Events;
using FastUnityCreationKit.Economy.Events.Data;
using FastUnityCreationKit.Tests.Economy.Data;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FastUnityCreationKit.Tests.Economy
{
    [TestFixture]
    public class LocalResourceTests : TestFixtureBase
    {
        [Test]
        public void EconomyExtensions_GetLocalResource_WorksCorrectly()
        {
            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            
            // Act
            bool found = entity.TryGetLocalResource(out ExampleHealthLocalResource actual);
            
            // Assert
            Assert.IsNotNull(actual);
        }
        
        [Test]
        public void EconomyExtensions_GetLocalResource_ReturnsSameInstance()
        {
            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            
            // Act
            entity.TryGetLocalResource(out ExampleHealthLocalResource actual1);
            entity.TryGetLocalResource(out ExampleHealthLocalResource actual2);
            
            // Assert
            Assert.AreSame(actual1, actual2);
        }
        
        [Test]
        public void EconomyExtensions_GetLocalResource_ReturnsFalse_IfResourceIsNotFound()
        {
            LogAssert.ignoreFailingMessages = true;
            
            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            
            // Act
            bool found = entity.TryGetLocalResource(out ExampleVoidLocalResource actual);
            
            // Assert
            Assert.IsFalse(found);
        }
        
        [Test]
        public void EconomyExtensions_AddLocalResource_WorksCorrectly()
        {
            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            
            // Act
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            // Assert
            Assert.IsTrue(entity.TryGetLocalResource(out ExampleHealthLocalResource actual));
            Assert.IsNotNull(actual);
            Assert.AreEqual(100, actual.Amount);
        }
        
        [Test]
        public void EconomyExtensions_TakeLocalResource_WorksCorrectly()
        {
            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            // Act
            entity.TakeLocalResource<ExampleHealthLocalResource, int32>(50);
            
            // Assert
            Assert.IsTrue(entity.TryGetLocalResource(out ExampleHealthLocalResource actual));
            Assert.IsNotNull(actual);
            Assert.AreEqual(50, actual.Amount);
        }
        
        [Test]
        public void EconomyExtensions_TryTakeLocalResource_ReturnsFalse_IfResourceAmountIsTooLow()
        {
            LogAssert.ignoreFailingMessages = true;
            
            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            // Act
            bool result = entity.TryTakeLocalResource<ExampleHealthLocalResource, int32>(150);
            
            // Assert
            Assert.IsTrue(entity.TryGetLocalResource(out ExampleHealthLocalResource actual));
            Assert.IsNotNull(actual);
            Assert.AreEqual(100, actual.Amount);
            Assert.IsFalse(result);
        }
        
        [Test]
        public void EconomyExtensions_SetLocalResource_WorksCorrectly()
        {
            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            // Act
            entity.SetLocalResource<ExampleHealthLocalResource, int32>(50);
            
            // Assert
            Assert.IsTrue(entity.TryGetLocalResource(out ExampleHealthLocalResource actual));
            Assert.IsNotNull(actual);
            Assert.AreEqual(50, actual.Amount);
        }
        
        [Test]
        public void EconomyExtensions_TakeResource_CanGetNegativeAmounts()
        {
            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            // Act
            entity.TakeLocalResource<ExampleHealthLocalResource, int32>(150);
            
            // Assert
            Assert.IsTrue(entity.TryGetLocalResource(out ExampleHealthLocalResource actual));
            Assert.IsNotNull(actual);
            Assert.AreEqual(-50, actual.Amount);
        }
        
        [Test]
        public void EconomyExtensions_HasEnoughResource_ReturnsTrue_IfResourceAmountIsEnough()
        {
            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            // Act
            bool result = entity.HasEnoughLocalResource<ExampleHealthLocalResource, int32>(50);
            
            // Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public void EconomyExtensions_HasEnoughResource_ReturnsFalse_IfResourceAmountIsTooLow()
        {
            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            // Act
            bool result = entity.HasEnoughLocalResource<ExampleHealthLocalResource, int32>(150);
            
            // Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void EconomyExtensions_HasEnoughResource_ReturnsTrue_IfResourceAmountIsEqual()
        {
            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            // Act
            bool result = entity.HasEnoughLocalResource<ExampleHealthLocalResource, int32>(100);
            
            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EconomyExtensions_AddResource_TriggersEvents()
        {
            float valueAdded = 0f;
            float valueChanged = 0f;

            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            
            OnLocalResourceAddedEvent<ExampleHealthLocalResource>.RegisterEventListener(OnLocalResourceAdded);
            OnLocalResourceChangedEvent<ExampleHealthLocalResource>.RegisterEventListener(OnLocalResourceChanged);
      
            // Act
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            // Assert
            Assert.AreEqual(100, valueAdded);
            Assert.AreEqual(100, valueChanged);

            // Cleanup
            OnLocalResourceAddedEvent<ExampleHealthLocalResource>.UnregisterEventListener(OnLocalResourceAdded);
            OnLocalResourceChangedEvent<ExampleHealthLocalResource>.UnregisterEventListener(OnLocalResourceChanged);
            return;

            // Event handler
            void OnLocalResourceAdded(LocalResourceEventData<ExampleHealthLocalResource> data)
            {
                Assert.AreEqual(data.entity, entity);
                valueAdded = data.amount;
            }
            
            void OnLocalResourceChanged(LocalResourceEventData<ExampleHealthLocalResource> data)
            {
                Assert.AreEqual(data.entity, entity);
                valueChanged = data.amount;
            }
        }
        
        [Test]
        public void EconomyExtensions_TakeResource_TriggersEvents()
        {
            float valueTaken = 0f;
            float valueChanged = 0f;

            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            OnLocalResourceTakenEvent<ExampleHealthLocalResource>.RegisterEventListener(OnLocalResourceTaken);
            OnLocalResourceChangedEvent<ExampleHealthLocalResource>.RegisterEventListener(OnLocalResourceChanged);
      
            // Act
            entity.TakeLocalResource<ExampleHealthLocalResource, int32>(50);
            
            // Assert
            Assert.AreEqual(50, valueTaken);
            Assert.AreEqual(-50, valueChanged);

            // Cleanup
            OnLocalResourceTakenEvent<ExampleHealthLocalResource>.UnregisterEventListener(OnLocalResourceTaken);
            OnLocalResourceChangedEvent<ExampleHealthLocalResource>.UnregisterEventListener(OnLocalResourceChanged);
            return;

            // Event handler
            void OnLocalResourceTaken(LocalResourceEventData<ExampleHealthLocalResource> data)
            {
                Assert.AreEqual(data.entity, entity);
                valueTaken = data.amount;
            }
            
            void OnLocalResourceChanged(LocalResourceEventData<ExampleHealthLocalResource> data)
            {
                Assert.AreEqual(data.entity, entity);
                valueChanged = data.amount;
            }
        }

        [Test]
        public void EconomyExtensions_TryTakeResource_TriggersEvents()
        {
            float valueTaken = 0f;
            float valueChanged = 0f;

            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            OnLocalResourceTakenEvent<ExampleHealthLocalResource>.RegisterEventListener(OnLocalResourceTaken);
            OnLocalResourceChangedEvent<ExampleHealthLocalResource>.RegisterEventListener(OnLocalResourceChanged);
      
            // Act
            entity.TryTakeLocalResource<ExampleHealthLocalResource, int32>(50);
            
            // Assert
            Assert.AreEqual(50, valueTaken);
            Assert.AreEqual(-50, valueChanged);

            // Cleanup
            OnLocalResourceTakenEvent<ExampleHealthLocalResource>.UnregisterEventListener(OnLocalResourceTaken);
            OnLocalResourceChangedEvent<ExampleHealthLocalResource>.UnregisterEventListener(OnLocalResourceChanged);
            return;

            // Event handler
            void OnLocalResourceTaken(LocalResourceEventData<ExampleHealthLocalResource> data)
            {
                Assert.AreEqual(data.entity, entity);
                valueTaken = data.amount;
            }
            
            void OnLocalResourceChanged(LocalResourceEventData<ExampleHealthLocalResource> data)
            {
                Assert.AreEqual(data.entity, entity);
                valueChanged = data.amount;
            }
        }
        
        [Test]
        public void EconomyExtensions_SetResource_TriggersEvents()
        {
            float valueChanged = 0f;

            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            OnLocalResourceChangedEvent<ExampleHealthLocalResource>.RegisterEventListener(OnLocalResourceChanged);
      
            // Act
            entity.SetLocalResource<ExampleHealthLocalResource, int32>(50);
            
            // Assert
            Assert.AreEqual(-50, valueChanged); // -50 because the value was changed from 100 to 50

            // Cleanup
            OnLocalResourceChangedEvent<ExampleHealthLocalResource>.UnregisterEventListener(OnLocalResourceChanged);
            return;
            
            void OnLocalResourceChanged(LocalResourceEventData<ExampleHealthLocalResource> data)
            {
                Assert.AreEqual(data.entity, entity);
                valueChanged = data.amount;
            }
        }
        
        [Test]
        public void EconomyExtensions_SetResource_DoesNotTriggerEvents_IfResourceAmountIsTheSame()
        {
            float valueChanged = 0f;

            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            OnLocalResourceChangedEvent<ExampleHealthLocalResource>.RegisterEventListener(OnLocalResourceChanged);
      
            // Act
            entity.SetLocalResource<ExampleHealthLocalResource, int32>(100);
            
            // Assert
            Assert.AreEqual(0, valueChanged);

            // Cleanup
            OnLocalResourceChangedEvent<ExampleHealthLocalResource>.UnregisterEventListener(OnLocalResourceChanged);
            return;
            
            void OnLocalResourceChanged(LocalResourceEventData<ExampleHealthLocalResource> data)
            {
                Assert.AreEqual(data.entity, entity);
                valueChanged = data.amount;
            }
        }
        
        [Test]
        public void EconomyExtensions_TryTakeResource_DoesNotTriggerEvents_IfResourceAmountIsTooLow()
        {
            float valueTaken = 0f;
            float valueChanged = 0f;

            // Arrange
            ExampleEntityWithWithLocalHealth entity = new ExampleEntityWithWithLocalHealth();
            entity.AddLocalResource<ExampleHealthLocalResource, int32>(100);
            
            OnLocalResourceTakenEvent<ExampleHealthLocalResource>.RegisterEventListener(OnLocalResourceTaken);
            OnLocalResourceChangedEvent<ExampleHealthLocalResource>.RegisterEventListener(OnLocalResourceChanged);
      
            // Act
            entity.TryTakeLocalResource<ExampleHealthLocalResource, int32>(150);
            
            // Assert
            Assert.AreEqual(0, valueTaken);
            Assert.AreEqual(0, valueChanged);

            // Cleanup
            OnLocalResourceTakenEvent<ExampleHealthLocalResource>.UnregisterEventListener(OnLocalResourceTaken);
            OnLocalResourceChangedEvent<ExampleHealthLocalResource>.UnregisterEventListener(OnLocalResourceChanged);
            return;
            
            // Event handler
            void OnLocalResourceTaken(LocalResourceEventData<ExampleHealthLocalResource> data)
            {
                Assert.AreEqual(data.entity, entity);
                valueTaken = data.amount;
            }
            
            void OnLocalResourceChanged(LocalResourceEventData<ExampleHealthLocalResource> data)
            {
                Assert.AreEqual(data.entity, entity);
                valueChanged = data.amount;
            }
        }
    }
}