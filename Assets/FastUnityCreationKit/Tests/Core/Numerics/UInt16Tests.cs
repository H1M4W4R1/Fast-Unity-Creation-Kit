using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Numerics
{
    [TestFixture]
    public class UInt16Tests : NumericEqualityTests<uint16>
    {
        [Test]
        public void IsSignedNumber_ReturnsFalse()
        {
            // Arrange
            INumber number = (uint16) 1;
            
            // Act
            bool isSignedNumber = number.IsSignedNumber;
            
            // Assert
            Assert.IsFalse(isSignedNumber);
        }

        [Test]
        public void IsFloatingPointNumber_ReturnsFalse()
        {
            // Arrange
            INumber number = (uint16) 1;

            // Act
            bool isFloatingPointNumber = number.IsFloatingPointNumber;

            // Assert
            Assert.IsFalse(isFloatingPointNumber);
        }

    }
}