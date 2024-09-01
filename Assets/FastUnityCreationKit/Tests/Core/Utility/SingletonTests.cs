using FastUnityCreationKit.Core.Testing;
using FastUnityCreationKit.Core.Utility.Singleton;
using FastUnityCreationKit.Tests.Core.Utility.Data;
using NUnit.Framework;
using UnityEngine;

namespace FastUnityCreationKit.Tests.Core.Utility
{
    [TestFixture]
    public sealed class SingletonTests : TestFixtureBase
    {

        [Test]
        public void GetInstance_CreatesInstanceOfCSharpClass_IfSingletonInstance_IsNull()
        {
            // Act
            RegularClassSingleton singleton = ISingleton<RegularClassSingleton>.GetInstance();
            
            // Assert
            Assert.IsNotNull(singleton);
        }
        
        [Test]
        public void GetInstance_CreatesInstanceOfMonoBehaviourClass_IfSingletonInstance_IsNull()
        {
            // Act
            ExampleMonoBehaviourClassSingleton singleton = IMonoBehaviourSingleton<ExampleMonoBehaviourClassSingleton>.GetInstance();
            
            // Assert
            Assert.IsNotNull(singleton);
            
            // Cleanup
            Object.DestroyImmediate(singleton.gameObject);
        }
        
        [Test]
        public void GetInstance_ReturnsSameInstanceOfCSharpClass_IfSingletonInstance_IsNotNull()
        {
            // Arrange
            RegularClassSingleton singleton = ISingleton<RegularClassSingleton>.GetInstance();
            
            // Act
            RegularClassSingleton newSingleton = ISingleton<RegularClassSingleton>.GetInstance();
            
            // Assert
            Assert.AreSame(singleton, newSingleton);
        }
        
        [Test]
        public void GetInstance_ReturnsSameInstanceOfMonoBehaviourClass_IfSingletonInstance_IsNotNull()
        {
            // Arrange
            ExampleMonoBehaviourClassSingleton singleton = IMonoBehaviourSingleton<ExampleMonoBehaviourClassSingleton>.GetInstance();
            
            // Act
            ExampleMonoBehaviourClassSingleton newSingleton = IMonoBehaviourSingleton<ExampleMonoBehaviourClassSingleton>.GetInstance();
            
            // Assert
            Assert.AreSame(singleton, newSingleton);
            
            // Cleanup
            Object.DestroyImmediate(singleton.gameObject);
        }

        [Test]
        public void GetInstance_CreatesNewInstanceOfSingleton_IfGameObjectWasDestroyed_InTheMeanwhile()
        {
            // Arrange
            ExampleMonoBehaviourClassSingleton singleton = IMonoBehaviourSingleton<ExampleMonoBehaviourClassSingleton>.GetInstance();
            Object.DestroyImmediate(singleton.gameObject);
            
            // Act
            ExampleMonoBehaviourClassSingleton newSingleton = IMonoBehaviourSingleton<ExampleMonoBehaviourClassSingleton>.GetInstance();
            
            // Assert
            Assert.IsNotNull(newSingleton);
            Assert.AreNotSame(singleton, newSingleton);
            
            // Cleanup
            Object.DestroyImmediate(newSingleton.gameObject);
        }
        
    }
}