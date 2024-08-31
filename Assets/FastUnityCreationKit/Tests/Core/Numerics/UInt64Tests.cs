using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Numerics
{
    [TestFixture]
    public class UInt64Tests : NumericEqualityTests<uint64>
    {
        [Test]
        public void IsSignedNumber_ReturnsFalse()
        {
            // Arrange
            INumber number = (uint64) 1;
            
            // Act
            bool isSignedNumber = number.IsSignedNumber;
            
            // Assert
            Assert.IsFalse(isSignedNumber);
        }

        [Test]
        public void IsFloatingPointNumber_ReturnsFalse()
        {
            // Arrange
            INumber number = (uint64) 1;

            // Act
            bool isFloatingPointNumber = number.IsFloatingPointNumber;

            // Assert
            Assert.IsFalse(isFloatingPointNumber);
        }

    }
}