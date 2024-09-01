using FastUnityCreationKit.Economy;
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
            ExampleEntityWithLocalHealth entity = new ExampleEntityWithLocalHealth();
            
            // Act
            bool found = entity.TryGetLocalResource(out ExampleHealthLocalResource actual);
            
            // Assert
            Assert.IsNotNull(actual);
        }
        
        [Test]
        public void EconomyExtensions_GetLocalResource_ReturnsSameInstance()
        {
            // Arrange
            ExampleEntityWithLocalHealth entity = new ExampleEntityWithLocalHealth();
            
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
            ExampleEntityWithLocalHealth entity = new ExampleEntityWithLocalHealth();
            
            // Act
            bool found = entity.TryGetLocalResource(out ExampleVoidLocalResource actual);
            
            // Assert
            Assert.IsFalse(found);
        }
    }
}