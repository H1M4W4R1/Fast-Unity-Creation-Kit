using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Economy;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Context;
using FastUnityCreationKit.Economy.Context.Internal;
using FastUnityCreationKit.Economy.Events;
using FastUnityCreationKit.Economy.Events.Data;
using FastUnityCreationKit.Tests.Economy.Data;
using FastUnityCreationKit.Tests.Economy.Data.Context;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Economy
{
    [TestFixture]
    public class GlobalResourceTests : TestFixtureBase
    {
        [TearDown]
        public void TearDown()
        {
            EconomyAPI.SetGlobalResource<ExampleCoinsGlobalResource>(0);
            EconomyAPI.SetGlobalResource<ExampleDiamondsGlobalResource>(0);
        }

        [Test]
        public void EconomyAPI_GetGlobalResource_WorksCorrectly()
        {
            // Act
            ExampleCoinsGlobalResource actual =
                EconomyAPI.GetGlobalResource<ExampleCoinsGlobalResource>();

            // Assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public void EconomyAPI_GetGlobalResource_ReturnsSameInstance()
        {
            // Act
            ExampleCoinsGlobalResource actual1 =
                EconomyAPI.GetGlobalResource<ExampleCoinsGlobalResource>();
            ExampleCoinsGlobalResource actual2 =
                EconomyAPI.GetGlobalResource<ExampleCoinsGlobalResource>();

            // Assert
            Assert.AreSame(actual1, actual2);
        }

        [Test]
        public void EconomyAPI_AddGlobalResource_WorksCorrectly()
        {
            // Act
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);
            ExampleCoinsGlobalResource actual =
                EconomyAPI.GetGlobalResource<ExampleCoinsGlobalResource>();

            // Assert
            Assert.AreEqual(10, actual.Amount);
        }

        [Test]
        public void EconomyAPI_TakeGlobalResource_WorksCorrectly()
        {
            // Arrange
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);

            // Act
            EconomyAPI.TakeGlobalResource<ExampleCoinsGlobalResource>(5);
            ExampleCoinsGlobalResource actual =
                EconomyAPI.GetGlobalResource<ExampleCoinsGlobalResource>();

            // Assert
            Assert.AreEqual(5, actual.Amount);
        }

        [Test]
        public void EconomyAPI_SetGlobalResource_WorksCorrectly()
        {
            // Act
            EconomyAPI.SetGlobalResource<ExampleCoinsGlobalResource>(10);
            ExampleCoinsGlobalResource actual =
                EconomyAPI.GetGlobalResource<ExampleCoinsGlobalResource>();

            // Assert
            Assert.AreEqual(10, actual.Amount);
        }

        [Test]
        public void EconomyAPI_TryTakeGlobalResource_WorksCorrectly()
        {
            // Arrange
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);

            // Act
            bool result = EconomyAPI.TryTakeGlobalResource<ExampleCoinsGlobalResource>(5);
            ExampleCoinsGlobalResource actual =
                EconomyAPI.GetGlobalResource<ExampleCoinsGlobalResource>();

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(5, actual.Amount);
        }

        [Test]
        public void EconomyAPI_TryTakeGlobalResource_ReturnsFalse_IfResourceAmountIsTooLow()
        {
            // Arrange
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);

            // Act
            bool result = EconomyAPI.TryTakeGlobalResource<ExampleCoinsGlobalResource>(15);
            ExampleCoinsGlobalResource actual =
                EconomyAPI.GetGlobalResource<ExampleCoinsGlobalResource>();

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(10, actual.Amount);
        }

        [Test]
        public void EconomyAPI_HasEnoughGlobalResource_ReturnsTrue_IfResourceAmountIsEnough()
        {
            // Arrange
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);

            // Act
            bool result = EconomyAPI.HasEnoughGlobalResource<ExampleCoinsGlobalResource>(5);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EconomyAPI_HasEnoughGlobalResource_ReturnsFalse_IfResourceAmountIsTooLow()
        {
            // Arrange
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);

            // Act
            bool result = EconomyAPI.HasEnoughGlobalResource<ExampleCoinsGlobalResource>(15);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EconomyAPI_HasEnoughGlobalResource_ReturnsTrue_IfResourceAmountIsEqual()
        {
            // Arrange
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);

            // Act
            bool result = EconomyAPI.HasEnoughGlobalResource<ExampleCoinsGlobalResource>(10);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EconomyAPI_AddResource_TriggersEvents()
        {
            float valueAdded = 0f;
            float valueChanged = 0f;

            // Arrange
            OnGlobalResourceAddedEvent<ExampleCoinsGlobalResource>.RegisterEventListener(OnGlobalResourceAdded);
            OnGlobalResourceChangedEvent<ExampleCoinsGlobalResource>.RegisterEventListener(OnGlobalResourceChanged);

            // Act
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);

            // Assert
            Assert.AreEqual(10f, valueAdded);
            Assert.AreEqual(10f, valueChanged);

            // Cleanup
            OnGlobalResourceAddedEvent<ExampleCoinsGlobalResource>.UnregisterEventListener(OnGlobalResourceAdded);
            OnGlobalResourceChangedEvent<ExampleCoinsGlobalResource>.UnregisterEventListener(OnGlobalResourceChanged);
            return;

            // Event handler
            UniTask OnGlobalResourceAdded(GlobalResourceEventData<ExampleCoinsGlobalResource> data)
            {
                valueAdded = data.context!.Amount;
                
                return UniTask.CompletedTask;
            }

            UniTask OnGlobalResourceChanged(GlobalResourceEventData<ExampleCoinsGlobalResource> data)
            {
                valueChanged = data.context!.Amount;
                
                return UniTask.CompletedTask;
            }
        }

        [Test]
        public void EconomyAPI_TakeResource_TriggersEvents()
        {
            float valueTaken = 0f;
            float valueChanged = 0f;

            // Arrange
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);
            OnGlobalResourceTakenEvent<ExampleCoinsGlobalResource>.RegisterEventListener(OnGlobalResourceTaken);
            OnGlobalResourceChangedEvent<ExampleCoinsGlobalResource>.RegisterEventListener(OnGlobalResourceChanged);

            // Act
            EconomyAPI.TakeGlobalResource<ExampleCoinsGlobalResource>(5);

            // Assert
            Assert.AreEqual(5f, valueTaken);
            Assert.AreEqual(5f, valueChanged);

            // Cleanup
            OnGlobalResourceTakenEvent<ExampleCoinsGlobalResource>.UnregisterEventListener(OnGlobalResourceTaken);
            OnGlobalResourceChangedEvent<ExampleCoinsGlobalResource>.UnregisterEventListener(OnGlobalResourceChanged);
            return;

            // Event handler
            UniTask OnGlobalResourceTaken(GlobalResourceEventData<ExampleCoinsGlobalResource> data)
            {
                valueTaken = data.context!.Amount;
                
                return UniTask.CompletedTask;
            }

            UniTask OnGlobalResourceChanged(GlobalResourceEventData<ExampleCoinsGlobalResource> data)
            {
                valueChanged = -data.context!.Amount;
                
                return UniTask.CompletedTask;
            }
        }
        
        [Test]
        public void EconomyAPI_SetResource_TriggersEvents()
        {
            float valueChanged = 0f;

            // Arrange
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);
            OnGlobalResourceChangedEvent<ExampleCoinsGlobalResource>.RegisterEventListener(OnGlobalResourceChanged);

            // Act
            EconomyAPI.SetGlobalResource<ExampleCoinsGlobalResource>(5);

            // Assert
            Assert.AreEqual(-5f, valueChanged); // Amount is set to 5, so the change is -5

            // Cleanup
            OnGlobalResourceChangedEvent<ExampleCoinsGlobalResource>.UnregisterEventListener(OnGlobalResourceChanged);
            return;

            // Event handler
            UniTask OnGlobalResourceChanged(GlobalResourceEventData<ExampleCoinsGlobalResource> data)
            {
                valueChanged = data.context!.Amount;
                
                return UniTask.CompletedTask;
            }
        }
        
        [Test]
        public void EconomyAPI_SetResource_DoesNotTriggerEvent_IfResourceAmountIsTheSame()
        {
            float valueChanged = 0f;

            // Arrange
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);
            OnGlobalResourceChangedEvent<ExampleCoinsGlobalResource>.RegisterEventListener(OnGlobalResourceChanged);

            // Act
            EconomyAPI.SetGlobalResource<ExampleCoinsGlobalResource>(10);

            // Assert
            Assert.AreEqual(0f, valueChanged);

            // Cleanup
            OnGlobalResourceChangedEvent<ExampleCoinsGlobalResource>.UnregisterEventListener(OnGlobalResourceChanged);
            return;

            // Event handler
            UniTask OnGlobalResourceChanged(GlobalResourceEventData<ExampleCoinsGlobalResource> data)
            {
                valueChanged = data.context!.Amount;
                
                return UniTask.CompletedTask;
            }
        }
        
        [Test]
        public void EconomyAPI_TryTakeResource_TriggersEvents()
        {
            float valueTaken = 0f;
            float valueChanged = 0f;

            // Arrange
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);
            OnGlobalResourceTakenEvent<ExampleCoinsGlobalResource>.RegisterEventListener(OnGlobalResourceTaken);
            OnGlobalResourceChangedEvent<ExampleCoinsGlobalResource>.RegisterEventListener(OnGlobalResourceChanged);

            // Act
            bool result = EconomyAPI.TryTakeGlobalResource<ExampleCoinsGlobalResource>(5);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(5f, valueTaken);
            Assert.AreEqual(5f, valueChanged);

            // Cleanup
            OnGlobalResourceTakenEvent<ExampleCoinsGlobalResource>.UnregisterEventListener(OnGlobalResourceTaken);
            OnGlobalResourceChangedEvent<ExampleCoinsGlobalResource>.UnregisterEventListener(OnGlobalResourceChanged);
            return;

            // Event handler
            UniTask OnGlobalResourceTaken(GlobalResourceEventData<ExampleCoinsGlobalResource> data)
            {
                valueTaken = data.context!.Amount;
                
                return UniTask.CompletedTask;
            }

            UniTask OnGlobalResourceChanged(GlobalResourceEventData<ExampleCoinsGlobalResource> data)
            {
                valueChanged = -data.context!.Amount;
                
                return UniTask.CompletedTask;
            }
        }
        
        [Test]
        public void EconomyAPI_TryTakeResource_DoesNotTriggerEvents_IfResourceAmountIsTooLow()
        {
            float valueTaken = 0f;
            float valueChanged = 0f;
            
            // Arrange
            EconomyAPI.AddGlobalResource<ExampleCoinsGlobalResource>(10);
            OnGlobalResourceTakenEvent<ExampleCoinsGlobalResource>.RegisterEventListener(OnGlobalResourceTaken);
            OnGlobalResourceChangedEvent<ExampleCoinsGlobalResource>.RegisterEventListener(OnGlobalResourceChanged);

            // Act
            bool result = EconomyAPI.TryTakeGlobalResource<ExampleCoinsGlobalResource>(15);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0f, valueTaken);
            Assert.AreEqual(0f, valueChanged);

            // Cleanup
            OnGlobalResourceTakenEvent<ExampleCoinsGlobalResource>.UnregisterEventListener(OnGlobalResourceTaken);
            OnGlobalResourceChangedEvent<ExampleCoinsGlobalResource>.UnregisterEventListener(OnGlobalResourceChanged);
            return;

            // Event handler
            UniTask OnGlobalResourceTaken(GlobalResourceEventData<ExampleCoinsGlobalResource> data)
            {
                valueTaken = data.context!.Amount;
                
                return UniTask.CompletedTask;
            }

            UniTask OnGlobalResourceChanged(GlobalResourceEventData<ExampleCoinsGlobalResource> data)
            {
                valueChanged = -data.context!.Amount;
                
                return UniTask.CompletedTask;
            }
        }

        [Test]
        public void EconomyAPI_AddResource_DoesNotTriggerEvent_IfMaxResourceLimit_IsReached()
        {
            float valueAdded = 0f;
            float valueChanged = 0f;
            
            // Arrange
            EconomyAPI.SetGlobalResource<ExampleDiamondsGlobalResource>(100);
            
            OnGlobalResourceAddedEvent<ExampleDiamondsGlobalResource>.RegisterEventListener(OnGlobalResourceAdded);
            OnGlobalResourceChangedEvent<ExampleDiamondsGlobalResource>.RegisterEventListener(OnGlobalResourceChanged);
            
            // Act
            EconomyAPI.AddGlobalResource<ExampleDiamondsGlobalResource>(5);
            
            // Assert
            Assert.AreEqual(0f, valueAdded);
            Assert.AreEqual(0f, valueChanged);
            
            // Cleanup
            OnGlobalResourceAddedEvent<ExampleDiamondsGlobalResource>.UnregisterEventListener(OnGlobalResourceAdded);
            OnGlobalResourceChangedEvent<ExampleDiamondsGlobalResource>.UnregisterEventListener(OnGlobalResourceChanged);
            return;
            
            // Event handler
            UniTask OnGlobalResourceAdded(GlobalResourceEventData<ExampleDiamondsGlobalResource> data)
            {
                valueAdded = data.context!.Amount;
                
                return UniTask.CompletedTask;   
            }
            
            UniTask OnGlobalResourceChanged(GlobalResourceEventData<ExampleDiamondsGlobalResource> data)
            {
                valueChanged = data.context!.Amount;
                
                return UniTask.CompletedTask;
            }
        } 
        
        [Test]
        public void EconomyAPI_TakeResource_DoesNotTriggerEvent_IfMinResourceLimit_IsReached()
        {
            float valueTaken = 0f;
            float valueChanged = 0f;
            
            // Arrange
            EconomyAPI.SetGlobalResource<ExampleDiamondsGlobalResource>(0);
            OnGlobalResourceTakenEvent<ExampleDiamondsGlobalResource>.RegisterEventListener(OnGlobalResourceTaken);
            OnGlobalResourceChangedEvent<ExampleDiamondsGlobalResource>.RegisterEventListener(OnGlobalResourceChanged);
            
            // Act
            EconomyAPI.TakeGlobalResource<ExampleDiamondsGlobalResource>(15);
            
            // Assert
            Assert.AreEqual(0f, valueTaken);
            Assert.AreEqual(0f, valueChanged);
            
            // Cleanup
            OnGlobalResourceTakenEvent<ExampleDiamondsGlobalResource>.UnregisterEventListener(OnGlobalResourceTaken);
            OnGlobalResourceChangedEvent<ExampleDiamondsGlobalResource>.UnregisterEventListener(OnGlobalResourceChanged);
            return;
            
            // Event handler
            UniTask OnGlobalResourceTaken(GlobalResourceEventData<ExampleDiamondsGlobalResource> data)
            {
                valueTaken = data.context!.Amount;
                
                return UniTask.CompletedTask;
            }
            
            UniTask OnGlobalResourceChanged(GlobalResourceEventData<ExampleDiamondsGlobalResource> data)
            {
                valueChanged = -data.context!.Amount;
                
                return UniTask.CompletedTask;
            }
        }

        // TODO: Copy this test for all other methods, should be fine tho as they're using same logic
        [Test]
        public void EconomyAPI_AddResource_TriggersEventWithSameContextAsUsed()
        {
            float valueAdded = 0f;
            
            // Create context
            AddFifteenDiamondsAsRewardContext context = new();
            
            // Arrange
            EconomyAPI.SetGlobalResource<ExampleDiamondsGlobalResource>(0);
            OnGlobalResourceAddedEvent<ExampleDiamondsGlobalResource>.RegisterEventListener(OnGlobalResourceAdded);
            
            // Act (add 15 diamonds)
            EconomyAPI.AddGlobalResource<ExampleDiamondsGlobalResource>(context);
            
            // Assert
            Assert.AreEqual(15f, valueAdded);
            
            // Cleanup
            OnGlobalResourceAddedEvent<ExampleDiamondsGlobalResource>.UnregisterEventListener(OnGlobalResourceAdded);
            return;
            
            // Event handler
            UniTask OnGlobalResourceAdded(GlobalResourceEventData<ExampleDiamondsGlobalResource> data)
            {
                valueAdded = data.context!.Amount;
                
                // Assert that context are the same
                Assert.AreSame(context, data.context);
                
                return UniTask.CompletedTask;
            }
        }
    }
}