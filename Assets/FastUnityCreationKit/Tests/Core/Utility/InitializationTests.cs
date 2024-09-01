using FastUnityCreationKit.Core.Utility.Initialization;
using FastUnityCreationKit.Tests.Core.Utility.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Utility
{
    [TestFixture]
    public sealed class InitializationTests
    {
        [Test]
        public void Initialization_EnsureInitialized_Initializes()
        {
            // Arrange
            ExampleInitializableObject exampleInitializableObject = new ExampleInitializableObject();
            IInitializable initializable = exampleInitializableObject;
            
            // Act
            initializable.EnsureInitialized();
        
            // Assert
            Assert.AreEqual(1, exampleInitializableObject.hasBeenInitialized);
        }
        
        [Test]
        public void Initialization_EnsureInitialized_InitializesOnce()
        {
            // Arrange
            ExampleInitializableObject exampleInitializableObject = new ExampleInitializableObject();
            IInitializable initializable = exampleInitializableObject;
            
            // Act
            initializable.EnsureInitialized();
            initializable.EnsureInitialized();
            initializable.EnsureInitialized();
            
            // Assert
            Assert.AreEqual(1, exampleInitializableObject.hasBeenInitialized);
        }
        
        [Test]
        public void Initialization_Initialize_Initializes()
        {
            // Arrange
            ExampleInitializableObject exampleInitializableObject = new ExampleInitializableObject();
            
            // Act
            exampleInitializableObject.Initialize();
        
            // Assert
            Assert.AreEqual(1, exampleInitializableObject.hasBeenInitialized);
        }
        
        [Test]
        public void Initialization_Initialize_InitializesOnce()
        {
            // Arrange
            ExampleInitializableObject exampleInitializableObject = new ExampleInitializableObject();
            
            // Act
            exampleInitializableObject.Initialize();
            exampleInitializableObject.Initialize();
            exampleInitializableObject.Initialize();
            
            // Assert
            Assert.AreEqual(1, exampleInitializableObject.hasBeenInitialized);
        }
    }
}