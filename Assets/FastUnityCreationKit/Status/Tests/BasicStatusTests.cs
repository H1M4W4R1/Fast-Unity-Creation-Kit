using FastUnityCreationKit.Status.Tests.Data;
using NUnit.Framework;
using Unity.Mathematics;

namespace FastUnityCreationKit.Status.Tests
{
    [TestFixture]
    public class BasicStatusTests
    {
        [Test]
        public void GetAmountOfTimesStatusIsAdded_Returns_Correct_Amount()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            RegularStatus status = new RegularStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // First assert - no statuses added
            Assert.AreEqual(0, objectWithStatus.GetAmountOfTimesStatusIsAdded<RegularStatus>());
            
            // Act
            objectWithStatus.AddStatus(status);
            
            // Assert
            Assert.AreEqual(1, objectWithStatus.GetAmountOfTimesStatusIsAdded<RegularStatus>());
        }
        
        [Test]
        public void AddStatus_Works_Correctly()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            RegularStatus status = new RegularStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(status);
            
            // Assert
            Assert.IsTrue(objectWithStatus.HasStatus<RegularStatus>());
            Assert.IsTrue(status.wasStatusAdded);
        }
        
        [Test]
        public void AddStatus_Does_Not_Add_Same_Status_Twice()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            RegularStatus status = new RegularStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(status);
            objectWithStatus.AddStatus(status);
            
            // Assert
            Assert.IsTrue(objectWithStatus.HasStatus<RegularStatus>());
            Assert.IsTrue(status.wasStatusAdded);
            Assert.AreEqual(1, objectWithStatus.GetAmountOfTimesStatusIsAdded<RegularStatus>());
        }
        
        [Test]
        public void RemoveStatus_Works_Correctly()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            RegularStatus status = new RegularStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(status);
            objectWithStatus.RemoveStatus<RegularStatus>();
            
            // Assert
            Assert.IsFalse(objectWithStatus.HasStatus<RegularStatus>());
            Assert.IsTrue(status.wasStatusRemoved);
        }
        
        [Test]
        public void RemoveStatus_Does_Not_Remove_Status_If_Not_Exists()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            RegularStatus status = new RegularStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.RemoveStatus<RegularStatus>();
            
            // Assert
            Assert.IsFalse(objectWithStatus.HasStatus<RegularStatus>());
            Assert.IsFalse(status.wasStatusRemoved);
        }
        
        [Test]
        public void IsStatusSupported_Returns_Correct_Value()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            RegularStatus status = new RegularStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(status);
            
            // Assert
            Assert.IsTrue(objectWithStatus.IsStatusSupported<RegularStatus>());
            Assert.IsFalse(objectWithStatus.IsStatusSupported<NotSupportedStatusMockup>());
        }
        
        [Test]
        public void GetStatus_Returns_Correct_Status()
        {
            // Arrange
            EntityWithStatus entity = new EntityWithStatus();
            RegularStatus status = new RegularStatus();
            
            // Cast to IObjectWithStatus to access the Statuses property
            IObjectWithStatus objectWithStatus = entity;
            
            // Act
            objectWithStatus.AddStatus(status);
            RegularStatus acquiredStatus = objectWithStatus.GetStatus<RegularStatus>();
            
            // Assert
            Assert.AreEqual(status,acquiredStatus );
            Assert.IsTrue(ReferenceEquals(status, acquiredStatus));
        }
    }
}