using FastUnityCreationKit.Status.Tests.Data;
using NUnit.Framework;
using Unity.Mathematics;

namespace FastUnityCreationKit.Status.Tests
{
    [TestFixture]
    public class StackablePercentageStatusTests
    {

        [Test]
        public void Add_AddsStatusWith_OneStack_And_ZeroPercentage()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            StackablePercentageStatus status = new StackablePercentageStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(status);
            
            // Assert
            Assert.AreEqual(1, (int)((IStackableStatus) status).StackCount);
            Assert.GreaterOrEqual(((IPercentageStatus) status).Percentage, 0 - math.EPSILON);
            Assert.LessOrEqual(((IPercentageStatus) status).Percentage, 0 + math.EPSILON);
            
            // Assert events was called
            Assert.AreEqual(1, status.statusWasAdded);
            Assert.AreEqual(0, status.statusWasRemoved);
            
            // Stack was increased
            Assert.AreEqual(1, status.stackCountChanged);
        }

        [Test]
        public void IncreasePercentage_AddsStatusPercentage_AndNoStacks()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>(0.5f);
            
            // Acquire the status from the entity
            StackablePercentageStatus status = objectWithStatus.GetStatus<StackablePercentageStatus>();
            float percentage = ((IPercentageStatus) status)!.Percentage;
            int stackCount = ((IStackableStatus) status)!.StackCount;
            
            
            // Assert
            Assert.AreEqual(0, stackCount);
            Assert.GreaterOrEqual(percentage, 0.5f - math.EPSILON);
            Assert.LessOrEqual(percentage, 0.5f + math.EPSILON);
            
            // Assert events was called
            Assert.AreEqual(1, status.statusWasAdded);
            Assert.AreEqual(0, status.statusWasRemoved);
            
            // Stack was not increased
            Assert.AreEqual(0, status.stackCountChanged);
        }
        
        [Test]
        public void IncreasePercentageToFull_IncreasesStackCount_AndSetsPercentageToCorrectValue()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>(0.5f);
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>(0.6f);
            
            // Acquire the status from the entity
            StackablePercentageStatus status = objectWithStatus.GetStatus<StackablePercentageStatus>();
            float percentage = ((IPercentageStatus) status)!.Percentage;
            int stackCount = ((IStackableStatus) status)!.StackCount;
            
            // Assert
            Assert.AreEqual(1, stackCount);
            Assert.GreaterOrEqual(percentage, 0.1f - math.EPSILON);
            Assert.LessOrEqual(percentage, 0.1f + math.EPSILON);
            
            // Assert events was called
            Assert.AreEqual(1, status.statusWasAdded);
            Assert.AreEqual(0, status.statusWasRemoved);
            
            // Stack was increased once
            Assert.AreEqual(1, status.stackCountChanged);
        }

        [Test]
        public void IncreasePercentage_SetsStackCountAndPercentage_ToCorrectValue_IfLargeValueIsUsed()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>(153.12f);
            
            // Acquire the status from the entity
            StackablePercentageStatus status = objectWithStatus.GetStatus<StackablePercentageStatus>();
            
            float percentage = ((IPercentageStatus) status)!.Percentage;
            int stackCount = ((IStackableStatus) status)!.StackCount;
            
            // Assert
            Assert.AreEqual(153, stackCount);
            Assert.GreaterOrEqual(percentage, 0.12f - stackCount * math.EPSILON); // Epsilon factors by stack count
            Assert.LessOrEqual(percentage, 0.12f + stackCount * math.EPSILON);
            
            // Assert events was called
            Assert.AreEqual(1, status.statusWasAdded);
            Assert.AreEqual(0, status.statusWasRemoved);
            
            // Stack was increased 153 times
            Assert.AreEqual(153, status.stackCountChanged);
        }
        
        [Test]
        public void DecreasePercentage_ReducesStackCount_WhenGoingThroughZeroPercentage()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>();
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>(0.5f);
            
            // Quick assert
            StackablePercentageStatus status = objectWithStatus.GetStatus<StackablePercentageStatus>();
            int stackCount = ((IStackableStatus) status)!.StackCount;
            
            Assert.AreEqual(1, stackCount);
            
            // Current status percentage is 1.5f
            objectWithStatus.DecreaseStatusPercentage<StackablePercentageStatus>(0.6f);
            
            // Acquire the status from the entity
            status = objectWithStatus.GetStatus<StackablePercentageStatus>();
            float percentage = ((IPercentageStatus) status)!.Percentage;
            stackCount = ((IStackableStatus) status)!.StackCount;
            
            // Assert
            // Current status percentage is 0.9f
            Assert.AreEqual(0, stackCount);
            Assert.GreaterOrEqual(percentage, 0.9f - math.EPSILON);
            Assert.LessOrEqual(percentage, 0.9f + math.EPSILON);
            
            // Assert events was called
            Assert.AreEqual(1, status.statusWasAdded);
            Assert.AreEqual(0, status.statusWasRemoved);
            
            // Stack was added and then removed thus the stack count should be 0
            Assert.AreEqual(0, status.stackCountChanged);
        }

        [Test]
        public void DecreasePercentage_DecreasesPercentageByCorrectValue_AndSetsCorrectStack_IfLargeValueIsUsed()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>(153.12f);
            objectWithStatus.DecreaseStatusPercentage<StackablePercentageStatus>(101.06f);
            
            // Acquire the status from the entity
            StackablePercentageStatus status = objectWithStatus.GetStatus<StackablePercentageStatus>();
            
            float percentage = ((IPercentageStatus) status)!.Percentage;
            int stackCount = ((IStackableStatus) status)!.StackCount;
            
            // Assert
            Assert.AreEqual(52, stackCount);
            Assert.GreaterOrEqual(percentage, 0.06f - stackCount * math.EPSILON); // Epsilon factors by stack count
            Assert.LessOrEqual(percentage, 0.06f + stackCount * math.EPSILON);
            
            // Assert events was called
            Assert.AreEqual(1, status.statusWasAdded);
            Assert.AreEqual(0, status.statusWasRemoved);
            
            // Stack was increased 153 times and decreased 101 times
            Assert.AreEqual(52, status.stackCountChanged);
        }

        [Test]
        public void DecreasePercentage_RemovesStatus_WhenPercentageAndStackCount_AreZero()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>();
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>(0.5f);
            
            // Quick assert
            StackablePercentageStatus status = objectWithStatus.GetStatus<StackablePercentageStatus>();
            int stackCount = ((IStackableStatus) status)!.StackCount;
            
            Assert.AreEqual(1, stackCount);
            
            // Current status percentage is 1.5f
            objectWithStatus.DecreaseStatusPercentage<StackablePercentageStatus>(1.5f);
            
            stackCount = ((IStackableStatus) status)!.StackCount;
            float percentage = ((IPercentageStatus) status)!.Percentage;
            
            // Assert
            Assert.AreEqual(0, stackCount);
            Assert.GreaterOrEqual(percentage, 0 - math.EPSILON);
            Assert.LessOrEqual(percentage, 0 + math.EPSILON);
            
            // Assert events was called
            Assert.AreEqual(1, status.statusWasAdded);
            Assert.AreEqual(1, status.statusWasRemoved);
            
            // Stack was added and then removed thus the stack count should be 0
            Assert.AreEqual(0, status.stackCountChanged);
            
            // Assert that entity no longer has the status
            Assert.IsFalse(objectWithStatus.HasStatus<StackablePercentageStatus>());
        }

        [Test]
        public void RemoveStatus_CallsAllEventsCorrectly_AndRemovesTheStatus()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>();
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>(0.5f);
            
            // Quick assert
            StackablePercentageStatus status = objectWithStatus.GetStatus<StackablePercentageStatus>();
            int stackCount = ((IStackableStatus) status)!.StackCount;
            
            Assert.AreEqual(1, stackCount);
            
            // Current status percentage is 1.5f
            objectWithStatus.RemoveStatus<StackablePercentageStatus>();
            
            stackCount = ((IStackableStatus) status)!.StackCount;
            float percentage = ((IPercentageStatus) status)!.Percentage;
            
            // Assert
            Assert.AreEqual(0, stackCount);
            Assert.GreaterOrEqual(percentage, 0 - math.EPSILON);
            Assert.LessOrEqual(percentage, 0 + math.EPSILON);
            
            // Assert events was called
            Assert.AreEqual(1, status.statusWasAdded);
            Assert.AreEqual(1, status.statusWasRemoved);
            
            // Stack was added and then removed thus the stack count should be 0
            Assert.AreEqual(0, status.stackCountChanged);
            
            // Assert that entity no longer has the status
            Assert.IsFalse(objectWithStatus.HasStatus<StackablePercentageStatus>());
        }

        [Test]
        public void GetStackCount_ReturnsCorrectValue()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>();
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>(0.5f);
            
            // Assert
            int stackCount = objectWithStatus.GetStatusStackCount<StackablePercentageStatus>();
            Assert.AreEqual(1, stackCount);
        }
        
        [Test]
        public void GetPercentage_ReturnsCorrectValue()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>();
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>(0.5f);
            
            // Assert
            float percentage = objectWithStatus.GetStatusPercentage<StackablePercentageStatus>();
            Assert.GreaterOrEqual(percentage, 0.5f - math.EPSILON);
            Assert.LessOrEqual(percentage, 0.5f + math.EPSILON);
        }
        
        [Test]
        public void GetAmountOfTimesStatusIsAdded_ReturnsStackCount()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>();
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>();
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>();
            objectWithStatus.IncreaseStatusPercentage<StackablePercentageStatus>(0.33f);
            
            // Assert
            int amountOfTimesStatusIsAdded = objectWithStatus.GetAmountOfTimesStatusIsAdded<StackablePercentageStatus>();
            Assert.AreEqual(3, amountOfTimesStatusIsAdded);
        }
        
    }
}