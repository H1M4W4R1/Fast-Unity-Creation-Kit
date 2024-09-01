using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Economy;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Tests.Economy.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Economy
{
    [TestFixture]
    public class GlobalResourceTests : TestFixtureBase
    {
        [TearDown]
        public void TearDown()
        {
            ExampleCoinsGlobalResource.Instance.SetAmount(0);
        }
        
        [Test]
        public void EconomyAPI_GetGlobalResource_WorksCorrectly()
        {
            // Act
            ExampleCoinsGlobalResource actual =
                EconomyAPI.GetGlobalResource<ExampleCoinsGlobalResource>();

            // Assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public void EconomyAPI_GetGlobalResource_ReturnsSameInstance()
        {
            // Act
            ExampleCoinsGlobalResource actual1 =
                EconomyAPI.GetGlobalResource<ExampleCoinsGlobalResource>();
            ExampleCoinsGlobalResource actual2 =
                EconomyAPI.GetGlobalResource<ExampleCoinsGlobalResource>();

            // Assert
            Assert.AreSame(actual1, actual2);
        }

        [Test]
        public void IGlobalResource_ReturnsCorrectReference_AndMatchesTheInstance()
        {
            // Arrange
            ExampleCoinsGlobalResource virtual0 =
                new ExampleCoinsGlobalResource();
            
            // Act
            ExampleCoinsGlobalResource actual2 = ((IGlobalResource) virtual0).GetGlobalResourceReference<ExampleCoinsGlobalResource>();
            
            ExampleCoinsGlobalResource actual3 =
                ExampleCoinsGlobalResource.Instance;

            // Assert
            Assert.AreSame(actual2, actual3);
        }
        
        
    }
}