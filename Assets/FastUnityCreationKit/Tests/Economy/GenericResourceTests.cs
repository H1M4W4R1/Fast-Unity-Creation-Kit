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
            ExampleCoinsGlobalResource.Instance.SetAmount(0);
            ExampleDiamondsGlobalResource.Instance.SetAmount(0);
        }
        
        [Test]
        public void Add_AddsCorrectAmountToResource()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            
            // Act
            coins.Add(10);
            
            // Assert
            Assert.AreEqual(10, coins.Amount);
        }
        
        [Test]
        public void Take_SubtractsCorrectAmountFromResource()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            coins.Add(10);
            
            // Act
            coins.Take(5);
            
            // Assert
            Assert.AreEqual(5, coins.Amount);
        }
        
        [Test]
        public void HasEnough_ReturnsTrue_IfEnoughResource()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            coins.Add(10);
            
            // Act
            bool hasEnough = coins.HasEnough(5);
            
            // Assert
            Assert.IsTrue(hasEnough);
        }
        
        [Test]
        public void HasEnough_ReturnsFalse_IfNotEnoughResource()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            coins.Add(10);
            
            // Act
            bool hasEnough = coins.HasEnough(15);
            
            // Assert
            Assert.IsFalse(hasEnough);
        }
        
        [Test]
        public void HasEnough_ReturnsTrue_IfExactAmount()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            coins.Add(10);
            
            // Act
            bool hasEnough = coins.HasEnough(10);
            
            // Assert
            Assert.IsTrue(hasEnough);
        }
        
        [Test]
        public void SetAmount_SetsCorrectAmount()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            
            // Act
            coins.SetAmount(10);
            
            // Assert
            Assert.AreEqual(10, coins.Amount);
        }
        
        [Test]
        public void TryTake_ReturnsTrue_AndTakes_IfEnoughResource()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            coins.Add(10);
            
            // Act
            bool taken = coins.TryTake(5);
            
            // Assert
            Assert.IsTrue(taken);
            Assert.AreEqual(5, coins.Amount);
        }
        
        [Test]
        public void TryTake_ReturnsFalse_AndDoesNotTake_IfNotEnoughResource()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            coins.Add(10);
            
            // Act
            bool taken = coins.TryTake(15);
            
            // Assert
            Assert.IsFalse(taken);
            Assert.AreEqual(10, coins.Amount);
        }
        
        [Test]
        public void TryTake_ReturnsTrue_AndTakes_IfExactAmount()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            coins.Add(10);
            
            // Act
            bool taken = coins.TryTake(10);
            
            // Assert
            Assert.IsTrue(taken);
            Assert.AreEqual(0, coins.Amount);
        }
        
        [Test]
        public void Reset_SetsResourceToDefaultAmount()
        {
            // Arrange
            ExampleCoinsGlobalResource coins = ExampleCoinsGlobalResource.Instance;
            coins.Add(10);
            
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
            diamonds.SetAmount(50);
            
            // Act
            diamonds.Take(10000);
            
            // Assert
            Assert.AreEqual(0, diamonds.Amount);
        }
        
        [Test]
        public void Add_AddsLimitsIntoAccount()
        {
            // Arrange
            ExampleDiamondsGlobalResource diamonds = ExampleDiamondsGlobalResource.Instance;
            diamonds.SetAmount(50);
            
            // Act
            diamonds.Add(10000);
            
            // Assert
            Assert.AreEqual(100, diamonds.Amount);
        }
        
        [Test]
        public void SetAmount_SetsLimitsIntoAccount()
        {
            // Arrange
            ExampleDiamondsGlobalResource diamonds = ExampleDiamondsGlobalResource.Instance;
            
            // Act
            diamonds.SetAmount(10000);
            
            // Assert
            Assert.AreEqual(100, diamonds.Amount);
            
            // Act
            diamonds.SetAmount(-10000);
            
            // Assert
            Assert.AreEqual(0, diamonds.Amount);
        }
        
        [Test]
        public void Reset_SetsDefaultAmount()
        {
            // Arrange
            ExampleDiamondsGlobalResource diamonds = ExampleDiamondsGlobalResource.Instance;
            diamonds.SetAmount(100);
            
            // Act
            diamonds.Reset();
            
            // Assert
            Assert.AreEqual(10, diamonds.Amount);
        }
        
        
    }
}