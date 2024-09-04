using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Economy;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Tests.Economy.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Economy
{
    [TestFixture]
    public class GenericResourceTests  : TestFixtureBase
    {
        [TearDown]
        public void TearDown()
        {
            EconomyAPI.SetGlobalResource<ExampleCoinsGlobalResource>(0);
            EconomyAPI.SetGlobalResource<ExampleDiamondsGlobalResource>(10);
        }
        
        [Test]
        public void Add_AddsCorrectAmountToResource()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            IResource<int32> resource = coins;
            
            // Act
            resource.Add(10);
            
            // Assert
            Assert.AreEqual(10, coins.Amount);
        }
        
        [Test]
        public void Take_SubtractsCorrectAmountFromResource()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            IResource<int32> resource = coins;
            
            resource.Add(10);
            
            // Act
            resource.Take(5);
            
            // Assert
            Assert.AreEqual(5, coins.Amount);
        }
        
        [Test]
        public void HasEnough_ReturnsTrue_IfEnoughResource()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            IResource<int32> resource = coins;
            
            resource.Add(10);
            
            // Act
            bool hasEnough = resource.HasEnough(5);
            
            // Assert
            Assert.IsTrue(hasEnough);
        }
        
        [Test]
        public void HasEnough_ReturnsFalse_IfNotEnoughResource()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            IResource<int32> resource = coins;
            
            // Act
            bool hasEnough = resource.HasEnough(15);
            
            // Assert
            Assert.IsFalse(hasEnough);
        }
        
        [Test]
        public void HasEnough_ReturnsTrue_IfExactAmount()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            IResource<int32> resource = coins;
            
            resource.Add(10);
            
            // Act
            bool hasEnough = resource.HasEnough(10);
            
            // Assert
            Assert.IsTrue(hasEnough);
        }
        
        [Test]
        public void SetAmount_SetsCorrectAmount()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            IResource<int32> resource = coins;
            
            // Act
            resource.SetAmount(10);
            
            // Assert
            Assert.AreEqual(10, coins.Amount);
        }
        
        [Test]
        public void TryTake_ReturnsTrue_AndTakes_IfEnoughResource()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            IResource<int32> resource = coins;
            
            resource.Add(10);
            
            // Act
            bool taken = resource.TryTake(5);
            
            // Assert
            Assert.IsTrue(taken);
            Assert.AreEqual(5, coins.Amount);
        }
        
        [Test]
        public void TryTake_ReturnsFalse_AndDoesNotTake_IfNotEnoughResource()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            IResource<int32> resource = coins;
            
            resource.Add(10);
            
            // Act
            bool taken = resource.TryTake(15);
            
            // Assert
            Assert.IsFalse(taken);
            Assert.AreEqual(10, coins.Amount);
        }
        
        [Test]
        public void TryTake_ReturnsTrue_AndTakes_IfExactAmount()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            IResource<int32> resource = coins;
            
            resource.Add(10);
            
            // Act
            bool taken = resource.TryTake(10);
            
            // Assert
            Assert.IsTrue(taken);
            Assert.AreEqual(0, coins.Amount);
        }
        
        [Test]
        public void Reset_SetsResourceToDefaultAmount()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            IResource<int32> resource = coins;
            
            resource.Add(10);
            
            // Act
            coins.Reset();
            
            // Assert
            Assert.AreEqual(0, coins.Amount);
        }
        
        [Test]
        public void Take_TakesLimitsIntoAccount()
        {
            // Arrange
            ExampleDiamondsGlobalResource diamonds = ExampleDiamondsGlobalResource.Instance;
            IResource<int32> resource = diamonds;
            
            resource.SetAmount(50);
            
            // Act
            resource.Take(10000);
            
            // Assert
            Assert.AreEqual(0, diamonds.Amount);
        }
        
        [Test]
        public void Add_TakesLimitsIntoAccount()
        {
            // Arrange
            ExampleDiamondsGlobalResource diamonds = ExampleDiamondsGlobalResource.Instance;
            IResource<int32> resource = diamonds;
            
            resource.SetAmount(50);
            
            // Act
            resource.Add(10000);
            
            // Assert
            Assert.AreEqual(100, diamonds.Amount);
        }
        
        [Test]
        public void SetAmount_TakesLimitsIntoAccount()
        {
            // Arrange
            ExampleDiamondsGlobalResource diamonds = ExampleDiamondsGlobalResource.Instance;
            IResource<int32> resource = diamonds;
            
            
            // Act
            resource.SetAmount(10000);
            
            // Assert
            Assert.AreEqual(100, diamonds.Amount);
            
            // Act
            resource.SetAmount(-10000);
            
            // Assert
            Assert.AreEqual(0, diamonds.Amount);
        }
        
        [Test]
        public void Reset_SetsDefaultAmount()
        {
            // Arrange
            ExampleDiamondsGlobalResource diamonds = ExampleDiamondsGlobalResource.Instance;
            IResource<int32> resource = diamonds;
            
            resource.SetAmount(100);
            
            // Act
            diamonds.Reset();
            
            // Assert
            Assert.AreEqual(10, diamonds.Amount);
        }
        
        
    }
}