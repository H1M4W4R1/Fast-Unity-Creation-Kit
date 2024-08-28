using FastUnityCreationKit.Status.Tests.Data;
using NUnit.Framework;
using Unity.Mathematics;

namespace FastUnityCreationKit.Status.Tests
{
    [TestFixture]
    public class PercentageStatusTests
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
            
            // Assert that the max percentage event was triggered
            Assert.IsTrue(status.wasMaxPercentageReached);
            Assert.IsFalse(status.wasMinPercentageReached);
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
            Assert.IsTrue(status.wasMaxPercentageReached);
            Assert.IsFalse(status.wasMinPercentageReached);
            
            // Assert that status was not removed
            Assert.IsFalse(status.wasStatusRemoved);
            
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
            Assert.IsTrue(status.wasMaxPercentageReached);
            Assert.IsTrue(status.wasMinPercentageReached);
            
            // Assert that status was removed
            Assert.IsTrue(status.wasStatusRemoved);
            
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
            Assert.IsFalse(status.wasMaxPercentageReached);
            Assert.IsFalse(status.wasMinPercentageReached);
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
            Assert.IsTrue(status.wasMaxPercentageReached);
            Assert.IsFalse(status.wasMinPercentageReached);
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
            Assert.IsFalse(status.wasMaxPercentageReached);
            Assert.IsFalse(status.wasMinPercentageReached);
            
            // Assert that status was not removed
            Assert.IsFalse(status.wasStatusRemoved);
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
            Assert.IsFalse(status.wasMaxPercentageReached);
            Assert.IsFalse(status.wasMinPercentageReached);
            
            // Assert that status was not removed
            Assert.IsFalse(status.wasStatusRemoved);
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
            Assert.IsTrue(status.wasMaxPercentageReached);
            Assert.IsFalse(status.wasMinPercentageReached);
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
        
        
    }
}