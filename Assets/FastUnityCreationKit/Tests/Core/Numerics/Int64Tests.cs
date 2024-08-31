using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Numerics
{
    [TestFixture]
    public class Int64Tests : NumericEqualityTests<int64>
    {
        
        [Test]
        public void IsSignedNumber_ReturnsTrue()
        {
            // Arrange
            INumber number = (int64) 1;
            
            // Act
            bool isSignedNumber = number.IsSignedNumber;
            
            // Assert
            Assert.IsTrue(isSignedNumber);
        }
        
        [Test]
        public void IsFloatingPointNumber_ReturnsFalse()
        {
            // Arrange
            INumber number = (int64) 1;
            
            // Act
            bool isFloatingPointNumber = number.IsFloatingPointNumber;
            
            // Assert
            Assert.IsFalse(isFloatingPointNumber);
        }
        
    }
}