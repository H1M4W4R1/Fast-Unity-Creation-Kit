using FastUnityCreationKit.Status;
using FastUnityCreationKit.Status.Context;
using FastUnityCreationKit.Tests.Status.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Status
{
	[TestFixture]
    public sealed class GenericStatusContextTests
    {
	    [Test]
	    public void Constructor_WithStatus_AssignsSameStatus()
	    {
		    // Arrange
		    EntityWithStatus entity = new();
		    RegularStatus status = new();
		    
		    // Act
		    GenericStatusContext<RegularStatus> context = new(entity, status);
		    
		    // Assert
		    Assert.AreSame(status, context.Status);
	    }

	    [Test]
	    public void Constructor_WithoutStatus_CreatesNewStatus_IfEntityDoesNotHaveIt()
	    {
		    // Arrange
		    EntityWithStatus entity = new();

		    // Act
		    GenericStatusContext<RegularStatus> context = new(entity);
		    
		    // Assert
		    Assert.IsNotNull(context.Status);
	    }
	    
	    [Test]
	    public void Constructor_WithoutStatus_UsesExistingStatus_IfEntityHasIt()
	    {
		    // Arrange
		    EntityWithStatus entity = new();
		    GenericStatusContext<RegularStatus> startContext = new(entity);
		    
		    IObjectWithStatus objectWithStatus = entity;
		    objectWithStatus.AddStatus(startContext);

		    // Create a new context
		    RegularStatus status = new();
		    
		    // Act
		    GenericStatusContext<RegularStatus> context = new(entity, status);
		    
		    RegularStatus entityStatus = objectWithStatus.GetStatus<RegularStatus>();
		    
		    // Assert
		    Assert.IsNotNull(entityStatus);
		    Assert.AreSame(startContext.Status, entityStatus);
		    
		    Assert.AreNotSame(status, context.Status);
		    Assert.AreNotSame(status, entityStatus);
	    }
        
    }
}