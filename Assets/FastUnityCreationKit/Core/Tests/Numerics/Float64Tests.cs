using FastUnityCreationKit.Core.Numerics;
using NUnit.Framework;

namespace FastUnityCreationKit.Core.Tests.Numerics
{
    [TestFixture]
    public class Float64Tests
    {
        [Test]
        public void Operator_ImplicitConversion_FromDouble_WorksCorrectly()
        {
            // Arrange
            double value = 10.0;

            // Act
            float64 number = value;

            // Assert
            Assert.AreEqual(value, (double) number);
        }

        [Test]
        public void Operator_ImplicitConversion_ToDouble_WorksCorrectly()
        {
            // Arrange
            float64 number = 10.0;

            // Act
            double value = number;

            // Assert
            Assert.AreEqual((double) number, value);
        }

        [Test]
        public void FromFloat_WorksCorrectly()
        {
            // Arrange
            float value = 10.0f;

            // Act
            float64 number = new float64();
            number = number.FromFloat(value);

            // Assert
            Assert.AreEqual(value, (float) number);
        }

        [Test]
        public void FromDouble_WorksCorrectly()
        {
            // Arrange
            double value = 10.0;

            // Act
            float64 number = new float64();
            number = number.FromDouble(value);

            // Assert
            Assert.AreEqual(value, (double) number);
        }

#region OP_ADDITION

        [Test] public void Add_Int8_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float64, int8, float64>(10);
        [Test] public void Add_Int16_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float64, int16, float64>(10);
        [Test] public void Add_Int32_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float64, int32, float64>(10);
        [Test] public void Add_Int64_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float64, int64, float64>(10);
        [Test] public void Add_UInt8_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float64, uint8, float64>(10);
        [Test] public void Add_UInt16_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float64, uint16, float64>(10);
        [Test] public void Add_UInt32_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float64, uint32, float64>(10);
        [Test] public void Add_UInt64_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float64, uint64, float64>(10);
        [Test] public void Add_Float32_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float64, float32, float64>(10);
        [Test] public void Add_Float64_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float64, float64, float64>(10);

#endregion

#region OP_SUBTRACTION

        [Test]
        public void Subtract_Int8_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float64, int8, float64>(10);

        [Test]
        public void Subtract_Int16_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float64, int16, float64>(10);

        [Test]
        public void Subtract_Int32_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float64, int32, float64>(10);

        [Test]
        public void Subtract_Int64_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float64, int64, float64>(10);

        [Test]
        public void Subtract_UInt8_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float64, uint8, float64>(10);

        [Test]
        public void Subtract_UInt16_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float64, uint16, float64>(10);

        [Test]
        public void Subtract_UInt32_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float64, uint32, float64>(10);

        [Test]
        public void Subtract_UInt64_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float64, uint64, float64>(10);

        [Test]
        public void Subtract_Float32_WorksCorrectly() =>
            Utility.Subtract_T_WorksCorrectly<float64, float32, float64>(10);

        [Test]
        public void Subtract_Float64_WorksCorrectly() =>
            Utility.Subtract_T_WorksCorrectly<float64, float64, float64>(10);

#endregion

#region OP_MULTIPLICATION

        [Test]
        public void Multiply_Int8_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float64, int8, float64>(10);

        [Test]
        public void Multiply_Int16_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float64, int16, float64>(10);

        [Test]
        public void Multiply_Int32_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float64, int32, float64>(10);

        [Test]
        public void Multiply_Int64_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float64, int64, float64>(10);

        [Test]
        public void Multiply_UInt8_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float64, uint8, float64>(10);

        [Test]
        public void Multiply_UInt16_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float64, uint16, float64>(10);

        [Test]
        public void Multiply_UInt32_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float64, uint32, float64>(10);

        [Test]
        public void Multiply_UInt64_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float64, uint64, float64>(10);

        [Test]
        public void Multiply_Float32_WorksCorrectly() =>
            Utility.Multiply_T_WorksCorrectly<float64, float32, float64>(10);

        [Test]
        public void Multiply_Float64_WorksCorrectly() =>
            Utility.Multiply_T_WorksCorrectly<float64, float64, float64>(10);

#endregion

#region OP_DIVISION

        [Test] public void Divide_Int8_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float64, int8, float64>(10);

        [Test]
        public void Divide_Int16_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float64, int16, float64>(10);

        [Test]
        public void Divide_Int32_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float64, int32, float64>(10);

        [Test]
        public void Divide_Int64_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float64, int64, float64>(10);

        [Test]
        public void Divide_UInt8_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float64, uint8, float64>(10);

        [Test]
        public void Divide_UInt16_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float64, uint16, float64>(10);

        [Test]
        public void Divide_UInt32_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float64, uint32, float64>(10);

        [Test]
        public void Divide_UInt64_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float64, uint64, float64>(10);

        [Test]
        public void Divide_Float32_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float64, float32, float64>(10);

        [Test]
        public void Divide_Float64_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float64, float64, float64>(10);

#endregion
    }
}