using FastUnityCreationKit.Core.Numerics.Abstract;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Numerics
{
    /// <summary>
    /// Represents a test suite for numeric equality.
    /// </summary>
    public class NumericEqualityTests<TNumber> : TestFixtureBase
        where TNumber : struct, ISupportsFloatConversion<TNumber>, INumber
    {
        
        [Test]
        public void EqualsOther_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            TNumber other = default;
            TNumber otherValue = default;

            number = number.FromDouble(32d);
            other = other.FromDouble(32d);
            otherValue = otherValue.FromDouble(35d);
    
            // Assert
            Assert.IsTrue(number.Equals(other));
            Assert.IsFalse(number.Equals(otherValue));
        }
        
        [Test]
        public void EqualsByte_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            byte other = 32;
            byte otherValue = 35;

            number = number.FromFloat(32f);
    
            // Assert
            Assert.IsTrue(number.Equals(other));
            Assert.IsFalse(number.Equals(otherValue));
        }
        
        [Test]
        public void EqualsSByte_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            sbyte other = 32;
            sbyte otherValue = 35;

            number = number.FromFloat(32f);
    
            // Assert
            Assert.IsTrue(number.Equals(other));
            Assert.IsFalse(number.Equals(otherValue));
        }
        
        [Test]
        public void EqualsShort_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            short other = 32;
            short otherValue = 35;

            number = number.FromFloat(32f);
    
            // Assert
            Assert.IsTrue(number.Equals(other));
            Assert.IsFalse(number.Equals(otherValue));
        }
        
        [Test]
        public void EqualsUShort_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            ushort other = 32;
            ushort otherValue = 35;

            number = number.FromFloat(32f);
    
            // Assert
            Assert.IsTrue(number.Equals(other));
            Assert.IsFalse(number.Equals(otherValue));
        }
        
        [Test]
        public void EqualsInt_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            int other = 32;
            int otherValue = 35;

            number = number.FromFloat(32f);
    
            // Assert
            Assert.IsTrue(number.Equals(other));
            Assert.IsFalse(number.Equals(otherValue));
        }
        
        [Test]
        public void EqualsUInt_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            uint other = 32;
            uint otherValue = 35;

            number = number.FromFloat(32f);
    
            // Assert
            Assert.IsTrue(number.Equals(other));
            Assert.IsFalse(number.Equals(otherValue));
        }
        
        [Test]
        public void EqualsLong_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            long other = 32;
            long otherValue = 35;

            number = number.FromFloat(32f);
    
            // Assert
            Assert.IsTrue(number.Equals(other));
            Assert.IsFalse(number.Equals(otherValue));
        }
        
        [Test]
        public void EqualsULong_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            ulong other = 32;
            ulong otherValue = 35;

            number = number.FromFloat(32f);
    
            // Assert
            Assert.IsTrue(number.Equals(other));
            Assert.IsFalse(number.Equals(otherValue));
        }
        
        [Test]
        public void EqualsDouble_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            double other = 32d;
            double otherValue = 35d;

            number = number.FromDouble(32d);
    
            // Assert
            Assert.IsTrue(number.Equals(other));
            Assert.IsFalse(number.Equals(otherValue));
        }
        
        [Test]
        public void EqualsFloat_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            float other = 32;
            float otherValue = 35;

            number = number.FromFloat(32f);
    
            // Assert
            Assert.IsTrue(number.Equals(other));
            Assert.IsFalse(number.Equals(otherValue));
        }
        
        [Test]
        public void ToFloat_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            float value = 32f;

            number = number.FromFloat(value);
    
            // Act
            float floatValue = number.ToFloat();
    
            // Assert
            Assert.AreEqual(value, floatValue);
        }
        
        [Test]
        public void ToDouble_WorksCorrectly()
        {
            // Arrange
            TNumber number = default;
            double value = 32;

            number = number.FromDouble(value);
     
            // Act
            double doubleValue = number.ToDouble();
    
            // Assert
            Assert.AreEqual(value, doubleValue);
        }
    }
}