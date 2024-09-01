using FastUnityCreationKit.Core.Utility;
using FastUnityCreationKit.Core.Utility.Properties;
using FastUnityCreationKit.Tests.Core.Utility.Data;
using NUnit.Framework;
using UnityEngine;

namespace FastUnityCreationKit.Tests.Core.Utility
{
    [TestFixture]
    public sealed class PropertiesTests : TestFixtureBase
    {
        [Test]
        public void GetProperty_WorksCorrectly()
        {
            // Arrange
            var exampleObject = new ExampleObjectWithProperties();
            
            // Act
            string name = exampleObject.GetName<ExampleObjectWithProperties, ExampleUsageContext>();
            string otherName = exampleObject.GetName<ExampleObjectWithProperties, OtherExampleUsageContext>();
            string description = exampleObject.GetDescription<ExampleObjectWithProperties, ExampleUsageContext>();
            
            // Assert
            Assert.AreEqual("Wolf", name);
            Assert.AreEqual("Queen of the Night", otherName);
            Assert.AreEqual("A creature of the night", description);
        }
        
        [Test]
        public void GetProperty_WithAnyUsageContext_WorksCorrectly()
        {
            // Arrange
            ExampleObjectWithAnyProperty exampleObject = new ExampleObjectWithAnyProperty();
            
            // Act
            string name = exampleObject.GetObjectName<ExampleUsageContext>();
            string otherName = exampleObject.GetObjectName<OtherExampleUsageContext>();
            string anyName = exampleObject.GetObjectName<AnyUsageContext>();
            
            // Assert
            Assert.AreEqual("Wolf", name);
            Assert.AreEqual("WOLF_NAME_NOT_DEFINED", otherName);
            Assert.AreEqual("WOLF_NAME_NOT_DEFINED", anyName);
        }
        
        [Test]
        public void GetProperty_WithAnyUsageContext_ReturnsNull_IfObjectDoesNotSupportProperty()
        {
            // Arrange
            object exampleObject = new object();
            
            // Act
            string name = exampleObject.GetObjectName<ExampleUsageContext>();
            
            // Assert
            Assert.IsNull(name);
        }
        
        [Test]
        public void GetProperty_WithAnyUsageContext_ReturnsNull_IfObjectIsNull()
        {
            // Arrange
            object exampleObject = null;
            
            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            string name = exampleObject.GetObjectName<ExampleUsageContext>();
            
            // Assert
            Assert.IsNull(name);
        }
        
        [Test]
        public void GetIcon_WorksCorrectly()
        {
            // Arrange
            ExampleObjectWithProperties exampleObject = new ExampleObjectWithProperties();
            
            // Act
            Sprite icon = exampleObject.GetIcon<ExampleObjectWithProperties, ExampleUsageContext>();
            
            // Assert
            Assert.IsNotNull(icon);
        }
    }
}