using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Numerics
{
    [TestFixture]
    public class Int32Tests : NumericEqualityTests<int32>
    {
        [Test]
        public void IsSignedNumber_ReturnsTrue()
        {
            // Arrange
            INumber number = (int32) 1;
            
            // Act
            bool isSignedNumber = number.IsSignedNumber;
            
            // Assert
            Assert.IsTrue(isSignedNumber);
        }

        [Test]
        public void IsFloatingPointNumber_ReturnsFalse()
        {
            // Arrange
            INumber number = (int32) 1;

            // Act
            bool isFloatingPointNumber = number.IsFloatingPointNumber;

            // Assert
            Assert.IsFalse(isFloatingPointNumber);
        }

    }
}