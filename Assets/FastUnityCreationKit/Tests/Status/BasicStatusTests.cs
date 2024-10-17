using FastUnityCreationKit.Status;
using FastUnityCreationKit.Status.Context;
using FastUnityCreationKit.Tests.Status.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Status
{
    [TestFixture]
    public class BasicStatusTests : TestFixtureBase
    {
        [Test]
        public void GetAmountOfTimesStatusIsAdded_Returns_Correct_Amount()
        {
            // Arrange
            EntityWithStatus entity = new();
            RegularStatus status = new();
            GenericStatusContext<RegularStatus> context = new(entity, status);
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // First assert - no statuses added
            Assert.AreEqual(0, objectWithStatus.GetAmountOfTimesStatusIsAdded<RegularStatus>());
            
            // Act
            objectWithStatus.AddStatus(context);
            
            // Assert
            Assert.AreEqual(1, objectWithStatus.GetAmountOfTimesStatusIsAdded<RegularStatus>());
        }
        
        [Test]
        public void AddStatus_Works_Correctly()
        {
            // Arrange
            EntityWithStatus entity = new();
            RegularStatus status = new();
            GenericStatusContext<RegularStatus> context = new(entity, status);
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(context);
            
            // Assert
            Assert.IsTrue(objectWithStatus.HasStatus<RegularStatus>());
            Assert.IsTrue(status.wasStatusAdded);
        }
        
        [Test]
        public void AddStatus_Does_Not_Add_Same_Status_Twice()
        {
            // Arrange
            EntityWithStatus entity = new();
            RegularStatus status = new();
            GenericStatusContext<RegularStatus> context = new(entity, status);
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(context);
            objectWithStatus.AddStatus(context);
            
            // Assert
            Assert.IsTrue(objectWithStatus.HasStatus<RegularStatus>());
            Assert.IsTrue(status.wasStatusAdded);
            Assert.AreEqual(1, objectWithStatus.GetAmountOfTimesStatusIsAdded<RegularStatus>());
        }
        
        [Test]
        public void RemoveStatus_Works_Correctly()
        {
            // Arrange
            EntityWithStatus entity = new();
            RegularStatus status = new();
            GenericStatusContext<RegularStatus> context = new(entity, status);
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(context);
            objectWithStatus.RemoveStatus<RegularStatus>(context);
            
            // Assert
            Assert.IsFalse(objectWithStatus.HasStatus<RegularStatus>());
            Assert.IsTrue(status.wasStatusRemoved);
        }
        
        [Test]
        public void RemoveStatus_Does_Not_Remove_Status_If_Not_Exists()
        {
            // Arrange
            EntityWithStatus entity = new();
            RegularStatus status = new();
            GenericStatusContext<RegularStatus> context = new(entity, status);
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.RemoveStatus<RegularStatus>(context);
            
            // Assert
            Assert.IsFalse(objectWithStatus.HasStatus<RegularStatus>());
            Assert.IsFalse(status.wasStatusRemoved);
        }
        
        [Test]
        public void IsStatusExplicitlySupported_Returns_Correct_Value()
        {
            // Arrange
            EntityWithStatus entity = new();
            RegularStatus status = new();
            GenericStatusContext<RegularStatus> context = new(entity, status);
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(context);
            
            // Assert
            Assert.IsTrue(objectWithStatus.IsStatusExplicitlySupported<RegularStatus>());
            Assert.IsFalse(objectWithStatus.IsStatusExplicitlySupported<NotSupportedStatusMockup>());
        }
        
        [Test]
        public void IsStatusSupported_Returns_Correct_Value()
        {
            // Arrange
            EntityWithForbiddenStatus entity = new();

            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
  
            // Assert
            Assert.IsTrue(objectWithStatus.IsStatusSupported<PercentageStatus>());
            Assert.IsTrue(objectWithStatus.IsStatusSupported<NotSupportedStatusMockup>());
            Assert.IsFalse(objectWithStatus.IsStatusSupported<RegularStatus>());
        }

        [Test]
        public void IsStatusSupported_BannedStatusTakesPriority_OverSupportedStatus()
        {
            // Arrange
            EntityWithForbiddenStatus entity = new();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Assert
            Assert.IsFalse(objectWithStatus.IsStatusSupported<StackableStatus>());
        }
        
        [Test]
        public void IsStatusExplicitlySupported_BannedStatusTakesPriority_OverSupportedStatus()
        {
            // Arrange
            EntityWithForbiddenStatus entity = new();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
 
            // Assert
            Assert.IsFalse(objectWithStatus.IsStatusExplicitlySupported<StackableStatus>());
        }
        
        [Test]
        public void GetStatus_Returns_Correct_Status()
        {
            // Arrange
            EntityWithStatus entity = new();
            RegularStatus status = new();
            GenericStatusContext<RegularStatus> context = new(entity, status);
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(context);
            RegularStatus acquiredStatus = objectWithStatus.GetStatus<RegularStatus>();
            
            // Assert
            Assert.AreEqual(status,acquiredStatus );
            Assert.IsTrue(ReferenceEquals(status, acquiredStatus));
        }
        
        [Test]
        public void AddStatus_DoesNotAdd_IfIsForbidden()
        {
            // Arrange
            EntityWithForbiddenStatus entity = new();
            RegularStatus status = new();
            GenericStatusContext<RegularStatus> context = new(entity, status);
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(context);
            
            // Assert
            Assert.IsFalse(objectWithStatus.HasStatus<RegularStatus>());
            Assert.IsFalse(status.wasStatusAdded);
        }

        [Test]
        public void AddStatus_DoesNotAdd_IfIsForbidden_EvenIfSupported()
        {
            // Arrange
            EntityWithForbiddenStatus entity = new();
            StackableStatus status = new();
            GenericStatusContext<StackableStatus> context = new(entity, status);
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(context);
            
            // Assert
            Assert.IsFalse(objectWithStatus.HasStatus<StackableStatus>());
            Assert.AreEqual(0, status.wasStatusAdded);
        }
    }
    
    
}