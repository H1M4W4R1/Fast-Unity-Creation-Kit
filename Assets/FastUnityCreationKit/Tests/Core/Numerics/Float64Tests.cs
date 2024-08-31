using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Numerics
{
    [TestFixture]
    public class Float64Tests : NumericEqualityTests<float64>
    {
        [Test]
        public void IsSignedNumber_ReturnsTrue()
        {
            // Arrange
            INumber number = (float64) 1.0;
            
            // Act
            bool isSignedNumber = number.IsSignedNumber;
            
            // Assert
            Assert.IsTrue(isSignedNumber);
        }
        
        [Test]
        public void IsFloatingPointNumber_ReturnsTrue()
        {
            // Arrange
            INumber number = (float64) 1.0;
            
            // Act
            bool isFloatingPointNumber = number.IsFloatingPointNumber;
            
            // Assert
            Assert.IsTrue(isFloatingPointNumber);
        }
        
    }
}