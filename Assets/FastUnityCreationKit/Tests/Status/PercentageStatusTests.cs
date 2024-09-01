using FastUnityCreationKit.Status;
using FastUnityCreationKit.Tests.Status.Data;
using NUnit.Framework;
using Unity.Mathematics;

namespace FastUnityCreationKit.Tests.Status
{
    [TestFixture]
    public class PercentageStatusTests : TestFixtureBase
    {
        [Test]
        public void AddStatus_SetsMaxPercentage()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            PercentageStatus status = new PercentageStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(status);
            
            // Assert
            float percentage = status.GetPercentage();
            
            // Assert that the percentage is 100%
            Assert.GreaterOrEqual(percentage, 1f - math.EPSILON);
            Assert.LessOrEqual(percentage, 1f + math.EPSILON);
            
            // Assert that the max percentage event was triggered once
            Assert.AreEqual(1, status.wasMaxPercentageReached);
        }

        [Test]
        public void AddStatus_SetsPercentageToFull_WhenStatusExists()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            PercentageStatus status = new PercentageStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(0.5f);
            objectWithStatus.AddStatus(status);
            
            // Assert
            float percentage = status.GetPercentage();
            
            // Assert that the percentage is 100%
            Assert.GreaterOrEqual(percentage, 1f - math.EPSILON);
            Assert.LessOrEqual(percentage, 1f + math.EPSILON);
            
            // Assert that the max percentage event was triggered
            Assert.AreEqual(1, status.wasMaxPercentageReached);
            
            // Assert that only one status was added
            Assert.AreEqual(1, objectWithStatus.GetAmountOfTimesStatusIsAdded<PercentageStatus>());
        }

        [Test]
        public void RemoveStatus_RemovesStatus_AndCallsMinPercentageReached()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            PercentageStatus status = new PercentageStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(status);
            objectWithStatus.RemoveStatus<PercentageStatus>();
            
            // Assert
            float percentage = status.GetPercentage();
            
            // Assert that the percentage is 0%
            Assert.GreaterOrEqual(percentage, 0f - math.EPSILON);
            Assert.LessOrEqual(percentage, 0f + math.EPSILON);
            
            // Assert that the min percentage event was triggered
            Assert.AreEqual(1, status.wasMinPercentageReached);
            
            // Assert that status no longer exists
            Assert.IsFalse(objectWithStatus.HasStatus<PercentageStatus>());
        }
        
        [Test]
        public void IncreasePercentage_AddsStatus_WithCorrectPercentage()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(0.5f);
            
            // Assert
            PercentageStatus status = objectWithStatus.GetStatus<PercentageStatus>();
            float percentage = status!.GetPercentage();
            
            // Assert that the percentage is 50%
            Assert.GreaterOrEqual(percentage, 0.5f - math.EPSILON);
            Assert.LessOrEqual(percentage, 0.5f + math.EPSILON);
            
            // Assert that the max percentage event was not triggered
            Assert.AreEqual(0, status.wasMaxPercentageReached);
        }

        [Test]
        public void IncreasePercentage_IncreasesPercentage_WhenStatusExists()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(0.5f);
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(0.5f);
            
            // Assert
            PercentageStatus status = objectWithStatus.GetStatus<PercentageStatus>();
            float percentage = status!.GetPercentage();
            
            // Assert that the percentage is 100%
            Assert.GreaterOrEqual(percentage, 1f - math.EPSILON);
            Assert.LessOrEqual(percentage, 1f + math.EPSILON);
            
            // Assert that the max percentage event was triggered
            Assert.AreEqual(1, status.wasMaxPercentageReached);
        }

        [Test]
        public void IncreasePercentage_DecreasesPercentage_WhenNegativeValueIsUsed()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(0.5f);
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(-0.25f);
            
            // Assert
            PercentageStatus status = objectWithStatus.GetStatus<PercentageStatus>();
            float percentage = status!.GetPercentage();
            
            // Assert that the percentage is 25%
            Assert.GreaterOrEqual(percentage, 0.25f - math.EPSILON);
            Assert.LessOrEqual(percentage, 0.25f + math.EPSILON);
          
            // Assert that the min percentage event was not triggered
            Assert.AreEqual(0, status.wasMinPercentageReached);
            Assert.AreEqual(0, status.wasMaxPercentageReached);
            
            // Assert that status was not removed
            Assert.AreEqual(0, status.wasStatusRemoved);
        }

        [Test]
        public void IncreasePercentage_IncreasesStatusPercentage_Once_IfLargeValueIsUsed()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            PercentageStatus status = new PercentageStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(status);
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(1000f);
            
            // Assert
            float percentage = status.GetPercentage();
            
            // Assert that the percentage is 100%
            Assert.GreaterOrEqual(percentage, 1f - math.EPSILON);
            Assert.LessOrEqual(percentage, 1f + math.EPSILON);
            
            // Assert that the max percentage event was triggered
            Assert.AreEqual(1, status.wasMaxPercentageReached);
        }
        
        [Test]
        public void DecreasePercentage_DecreasesPercentage_WhenStatusExists()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(0.5f);
            objectWithStatus.DecreaseStatusPercentage<PercentageStatus>(0.25f);
            
            // Assert
            PercentageStatus status = objectWithStatus.GetStatus<PercentageStatus>();
            float percentage = status!.GetPercentage();
            
            // Assert that the percentage is 25%
            Assert.GreaterOrEqual(percentage, 0.25f - math.EPSILON);
            Assert.LessOrEqual(percentage, 0.25f + math.EPSILON);
            
            // Assert that the min percentage event was not triggered
            Assert.AreEqual(0, status.wasMinPercentageReached);
            Assert.AreEqual(0, status.wasMaxPercentageReached);
            
            // Assert that status was not removed
            Assert.AreEqual(0, status.wasStatusRemoved);
        }
        
        [Test]
        public void DecreasePercentage_IncreasesPercentage_WhenNegativeValueIsUsed()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(0.5f);
            objectWithStatus.DecreaseStatusPercentage<PercentageStatus>(-0.5f);
            
            // Assert
            PercentageStatus status = objectWithStatus.GetStatus<PercentageStatus>();
            float percentage = status!.GetPercentage();
            
            // Assert that the percentage is 100%
            Assert.GreaterOrEqual(percentage, 1f - math.EPSILON);
            Assert.LessOrEqual(percentage, 1f + math.EPSILON);
            
            // Assert that the max percentage event was triggered
            Assert.AreEqual(1, status.wasMaxPercentageReached);
            Assert.AreEqual(0, status.wasMinPercentageReached);
        }

        [Test]
        public void DecreasePercentage_DecreasesPercentage_Once_IfLargeValueIsUsed()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            PercentageStatus status = new PercentageStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(status);
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(0.5f);
            objectWithStatus.DecreaseStatusPercentage<PercentageStatus>(1000f);
            
            // Assert
            float percentage = status.GetPercentage();
            
            // Assert that the percentage is 0%
            Assert.GreaterOrEqual(percentage, 0f - math.EPSILON);
            Assert.LessOrEqual(percentage, 0f + math.EPSILON);
            
            // Assert that the min percentage event was triggered
            Assert.AreEqual(1, status.wasMinPercentageReached);
        }
        
        [Test]
        public void GetPercentage_ReturnsCorrectValue()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(0.5f);
            
            // Assert
            float percentage = objectWithStatus.GetStatusPercentage<PercentageStatus>();
                
            
            // Assert that the percentage is 50%
            Assert.GreaterOrEqual(percentage, 0.5f - math.EPSILON);
            Assert.LessOrEqual(percentage, 0.5f + math.EPSILON);
        }
        
        [Test]
        public void GetPercentage_ReturnsCorrectValue_WhenStatusDoesNotExist()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            float percentage = objectWithStatus.GetStatusPercentage<PercentageStatus>();
            
            // Assert
            // Assert that the percentage is 0%
            Assert.GreaterOrEqual(percentage, 0f - math.EPSILON);
            Assert.LessOrEqual(percentage, 0f + math.EPSILON);
        }
        
        [Test]
        public void GetAmountOfTimesStatusIsAdded_ReturnsCorrectValue()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            PercentageStatus status = new PercentageStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(status);
            
            // Assert
            int amount = objectWithStatus.GetAmountOfTimesStatusIsAdded<PercentageStatus>();
            
            // Assert that the status was added once
            Assert.AreEqual(1, amount);
        }

        [Test]
        public void IncreasePercentage_DoesNotTrigger_MaxAmountReachedEvent_MultipleTimes()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(1f);
            objectWithStatus.IncreaseStatusPercentage<PercentageStatus>(1f);
            
            // Assert
            PercentageStatus status = objectWithStatus.GetStatus<PercentageStatus>();
            
            // Assert that the max percentage event was triggered once
            Assert.AreEqual(1, status!.wasMaxPercentageReached);
            
            // Assert that the percentage is 100%
            Assert.GreaterOrEqual(status.GetPercentage(), 1f - math.EPSILON);
            Assert.LessOrEqual(status.GetPercentage(), 1f + math.EPSILON);
            
            // Assert that the status was not removed
            Assert.AreEqual(0, status.wasStatusRemoved);
        }

        [Test]
        public void DecreasePercentage_DoesNotTrigger_MinAmountReachedEvent_WhenStatusDoesNotExist()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.DecreaseStatusPercentage<PercentageStatus>(1f);
            
            // Assert

            // Assert that the min percentage event was not triggered
            Assert.AreEqual(0, entity.statusPercentageReachedZeroTimes);
            
            // Assert that status does not exist on the object
            Assert.IsFalse(objectWithStatus.HasStatus<PercentageStatus>());
        }
        
    }
}