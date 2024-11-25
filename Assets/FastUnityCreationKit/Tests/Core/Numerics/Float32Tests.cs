using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Numerics
{
    [TestFixture]
    public class Float32Tests : NumericEqualityTests<float32>
    {
        [Test]
        public void IsSignedNumber_ReturnsTrue()
        {
            // Arrange
            INumber number = (float32) 1.0f;
            
            // Act
            bool isSignedNumber = number.IsSignedNumber;
            
            // Assert
            Assert.IsTrue(isSignedNumber);
        }
        
        [Test]
        public void IsFloatingPointNumber_ReturnsTrue()
        {
            // Arrange
            INumber number = (float32) 1.0f;
            
            // Act
            bool isFloatingPointNumber = number.IsFloatingPointNumber;
            
            // Assert
            Assert.IsTrue(isFloatingPointNumber);
        }
        
    }
}