using FastUnityCreationKit.Status;
using FastUnityCreationKit.Status.Context;
using FastUnityCreationKit.Tests.Status.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Status
{
    public class StackableStatusTests : TestFixtureBase
    {
        [Test]
        public void AddStatus_AddsFirstStack_ToStackableStatus()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context);

            // Assert
            Assert.AreEqual(1, status.wasStatusAdded);
            Assert.AreEqual(1, status.CurrentStack);
            Assert.AreEqual(1, status.wasStackCountChanged);
        }

        [Test]
        public void AddStatus_AddsSecondStack_ToStackableStatus()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context);
            objectWithStatus.AddStatus(context);

            // Assert
            Assert.AreEqual(2, status.CurrentStack);
            Assert.AreEqual(2, status.wasStackCountChanged);
        }

        [Test]
        public void RemoveStatus_RemovesAllStacks_FromStackableStatus()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context);
            objectWithStatus.RemoveStatus<StackableStatus>(context);

            // Assert
            Assert.AreEqual(0, status.CurrentStack);

            // Assert that the status was removed from the list once
            Assert.AreEqual(1, status.wasStatusRemoved);

            // Assert that the stack count was decreased
            Assert.AreEqual(0, objectWithStatus.GetAmountOfTimesStatusIsAdded<StackableStatus>());

            // Assert that the status was removed from the list
            Assert.IsFalse(objectWithStatus.HasStatus<StackableStatus>());
        }

        [Test]
        public void IncreaseStackCount_IncreasesStackCount_ByOne()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context);
            objectWithStatus.IncreaseStatusStackCountAsync<StackableStatus>(context);

            // Assert
            Assert.AreEqual(2, status.CurrentStack);
            Assert.AreEqual(1, status.wasStatusAdded);
            Assert.AreEqual(2, status.wasStackCountChanged);
        }

        [Test]
        public void IncreaseStackCount_IncreasesStackCount_ByCorrectValue()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context); // +1
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context, 2); // +2

            // Assert
            Assert.AreEqual(3, status.CurrentStack);

            // Assert that the stack count was increased
            // The stack count is increased by 2 so check for 2 events
            Assert.AreEqual(3, status.wasStackCountChanged);

            // Assert that the status was added once
            Assert.AreEqual(1, status.wasStatusAdded);
        }

        [Test]
        public void IncreaseStackCount_AddsStatus_IfNotAlreadyAdded()
        {
            // Arrange
            EntityWithStatus entity = new();
            GenericStatusContext<StackableStatus> context = new(entity);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context);
            StackableStatus status = objectWithStatus.GetStatus<StackableStatus>();

            // Assert
            Assert.AreEqual(1, status!.CurrentStack);
            Assert.AreEqual(1, status.wasStatusAdded);
        }

        [Test]
        public void IncreaseStackCount_DoesNotCallMaxStackReachedEvent_IfStackCountIsNotChanged()
        {
            // Arrange
            EntityWithStatus entity = new();
            GenericStatusContext<StackableStatus> context = new(entity);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context);

            // Acquire the status
            StackableStatus status = objectWithStatus.GetStatus<StackableStatus>();

            // Assert
            Assert.AreEqual(3, status!.CurrentStack);
            Assert.AreEqual(3, status.wasStackCountChanged);

            // Assert that the status was added once
            Assert.AreEqual(1, status.wasStatusAdded);

            // Assert that the max stack count was reached once
            Assert.AreEqual(1, status.wasMaxStackCountReached);
        }

        [Test]
        public void IncreaseStackCount_CallsMaxStackReachedEvent_EveryTime_IfEveryTimeNotificationSetting_IsUsed()
        {
            // Arrange
            EntityWithStatus entity = new();
            GenericStatusContext<MultipleNotificationStackableStatus> context = new(entity);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.IncreaseStatusStackCount<MultipleNotificationStackableStatus>(context, 5);

            // Acquire the status
            MultipleNotificationStackableStatus status =
                objectWithStatus.GetStatus<MultipleNotificationStackableStatus>();

            // Assert
            Assert.AreEqual(3, status!.CurrentStack);
            Assert.AreEqual(3, status.wasStackCountChanged);

            // Assert that the status was added once
            Assert.AreEqual(1, status.wasStatusAdded);

            // Assert that the max stack count was reached 3 times
            Assert.AreEqual(3, status.wasMaxStackCountReached);
        }

        [Test]
        public void DecreaseStackCount_DecreasesStackCount_ByOne()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context); // +1
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context, 2); // +2
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context); // -1

            // Assert
            Assert.AreEqual(2, status.CurrentStack);
            Assert.AreEqual(2, status.wasStackCountChanged);
        }

        [Test]
        public void DecreaseStackCount_DecreasesStackCount_ByCorrectValue()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context); // +1
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context, 3); // +3 (+2 due to limit)
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context, 2); // -2

            // Assert
            Assert.AreEqual(1, status.CurrentStack);

            // Assert that the stack count was decreased 
            Assert.AreEqual(1, status.wasStackCountChanged);

            // Assert that the status was added
            Assert.AreEqual(1, status.wasStatusAdded);
        }

        [Test]
        public void DecreaseStackCount_RemovesStatus_IfStackCountIsZero()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context);
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context);

            // Assert
            Assert.AreEqual(0, status.CurrentStack);
            Assert.AreEqual(1, status.wasStatusRemoved);
        }

        [Test]
        public void DecreaseStackCount_DoesNotRemoveStatus_IfStackCountIsNotZero()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context);
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context);

            // Assert
            Assert.AreEqual(1, status.CurrentStack);
            Assert.AreEqual(0, status.wasStatusRemoved);
        }

        [Test]
        public void DecreaseStackCount_SupportsNegativeValues()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context);
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context, 3);
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context, 4);

            // Assert
            Assert.AreEqual(-1, status.CurrentStack);
        }

        [Test]
        public void DecreaseStackCount_DoesNotCallMinStackReachedEvent_IfStackCountIsNotChanged()
        {
            // Arrange
            EntityWithStatus entity = new();
            GenericStatusContext<StackableStatus> context = new(entity);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context);

            // Acquire the status
            StackableStatus status = objectWithStatus.GetStatus<StackableStatus>();

            // Assert
            Assert.AreEqual(-3, status!.CurrentStack);
            Assert.AreEqual(-3, status.wasStackCountChanged);

            // Assert that the status was added once
            Assert.AreEqual(1, status.wasStatusAdded);

            // Assert that the min stack count was reached once
            Assert.AreEqual(1, status.wasMinStackCountReached);
        }

        [Test]
        public void DecreaseStackCount_CallsMinStackReachedEvent_EveryTime_IfEveryTimeNotificationSetting_IsUsed()
        {
            // Arrange
            EntityWithStatus entity = new();
            GenericStatusContext<MultipleNotificationStackableStatus> context = new(entity);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.DecreaseStatusStackCount<MultipleNotificationStackableStatus>(
                context, 5);

            // Acquire the status
            MultipleNotificationStackableStatus status =
                objectWithStatus.GetStatus<MultipleNotificationStackableStatus>();

            // Assert
            Assert.AreEqual(-3, status!.CurrentStack);
            Assert.AreEqual(-3, status.wasStackCountChanged);

            // Assert that the status was added once
            Assert.AreEqual(1, status.wasStatusAdded);

            // Assert that the min stack count was reached 3 times
            Assert.AreEqual(3, status.wasMinStackCountReached);
        }

        [Test]
        public void MinLimit_IsRespected_WhenDecreasingStackCount()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context);
            objectWithStatus.DecreaseStatusStackCount<StackableStatus>(context, 40);

            // Assert
            Assert.AreEqual(-3, status.CurrentStack);

            // Assert that the min stack count was reached
            Assert.AreEqual(1, status.wasMinStackCountReached);
        }

        [Test]
        public void MaxLimit_IsRespected_WhenIncreasingStackCount()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context);
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context, 40);

            // Assert
            Assert.AreEqual(3, status.CurrentStack);

            // Assert that the max stack count was reached
            Assert.AreEqual(1, status.wasMaxStackCountReached);
        }

        [Test]
        public void GetStatusStackCount_ReturnsCorrectValue()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            objectWithStatus.AddStatus(context);
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context, 2);

            // Assert
            Assert.AreEqual(3, objectWithStatus.GetStatusStackCount<StackableStatus>());
        }

        [Test]
        public void GetStatusStackCount_ReturnsZero_IfStatusIsNotAdded()
        {
            // Arrange
            EntityWithStatus entity = new();

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            // Act
            int stackCount = objectWithStatus.GetStatusStackCount<StackableStatus>();

            // Assert
            Assert.AreEqual(0, stackCount);
        }

        [Test]
        public void GetStatusStackCount_ReturnsZero_IfStatusIsRemoved()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            objectWithStatus.AddStatus(context);
            objectWithStatus.IncreaseStatusStackCount<StackableStatus>(context);
            objectWithStatus.RemoveStatus<StackableStatus>(context);

            // Act
            int stackCount = objectWithStatus.GetStatusStackCount<StackableStatus>();

            // Assert
            Assert.AreEqual(0, stackCount);
        }

        [Test]
        public void GetAmountOfTimesStatusIsAdded_ReturnsCorrectValue()
        {
            // Arrange
            EntityWithStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;

            objectWithStatus.AddStatus(context);
            objectWithStatus.AddStatus(context);

            // Act
            int amountOfTimesStatusIsAdded = objectWithStatus.GetAmountOfTimesStatusIsAdded<StackableStatus>();

            // Assert
            Assert.AreEqual(2, amountOfTimesStatusIsAdded);
        }
    }
}