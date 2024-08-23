using FastUnityCreationKit.Core.Numerics;
using NUnit.Framework;

namespace FastUnityCreationKit.Core.Tests.Numerics
{
    [TestFixture]
    public class Float32Tests
    {
        [Test]
        public void Operator_ImplicitConversion_FromFloat_WorksCorrectly()
        {
            // Prepare
            float number = 1.0f;

            // Act
            float32 convertedNumber = number;

            // Assert
            Assert.AreEqual(number, (float) convertedNumber);
        }

        [Test]
        public void Operator_ImplicitConversion_ToFloat_WorksCorrectly()
        {
            // Prepare
            float32 number = 1.0f;

            // Act
            float convertedNumber = number;

            // Assert
            Assert.AreEqual((float) number, convertedNumber);
        }

        [Test]
        public void FromFloat_WorksCorrectly()
        {
            // Prepare
            float number = 1.0f;
            float32 expected = number;

            // Act
            float32 convertedNumber = new float32();
            convertedNumber = convertedNumber.FromFloat(number);

            // Assert
            Assert.AreEqual((float) expected, (float) convertedNumber);
        }

        [Test]
        public void FromDouble_WorksCorrectly()
        {
            // Prepare
            double number = 1.0;
            float32 expected = 1.0f;

            // Act
            float32 convertedNumber = new float32();
            convertedNumber = convertedNumber.FromDouble(number);

            // Assert
            Assert.AreEqual(expected, convertedNumber);
        }


#region OP_ADDITION

        [Test] public void Add_Int8_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float32, int8, float32>(10);
        [Test] public void Add_Int16_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float32, int16, float32>(10);
        [Test] public void Add_Int32_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float32, int32, float32>(10);
        [Test] public void Add_Int64_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float32, int64, float32>(10);
        [Test] public void Add_UInt8_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float32, uint8, float32>(10);
        [Test] public void Add_UInt16_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float32, uint16, float32>(10);
        [Test] public void Add_UInt32_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float32, uint32, float32>(10);
        [Test] public void Add_UInt64_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float32, uint64, float32>(10);
        [Test] public void Add_Float32_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float32, float32, float32>(10);
        [Test] public void Add_Float64_WorksCorrectly() => Utility.Add_T_WorksCorrectly<float32, float64, float64>(10);

#endregion

#region OP_SUBTRACTION

        [Test]
        public void Subtract_Int8_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float32, int8, float32>(10);

        [Test]
        public void Subtract_Int16_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float32, int16, float32>(10);

        [Test]
        public void Subtract_Int32_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float32, int32, float32>(10);

        [Test]
        public void Subtract_Int64_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float32, int64, float32>(10);

        [Test]
        public void Subtract_UInt8_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float32, uint8, float32>(10);

        [Test]
        public void Subtract_UInt16_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float32, uint16, float32>(10);

        [Test]
        public void Subtract_UInt32_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float32, uint32, float32>(10);

        [Test]
        public void Subtract_UInt64_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<float32, uint64, float32>(10);

        [Test]
        public void Subtract_Float32_WorksCorrectly() =>
            Utility.Subtract_T_WorksCorrectly<float32, float32, float32>(10);

        [Test]
        public void Subtract_Float64_WorksCorrectly() =>
            Utility.Subtract_T_WorksCorrectly<float32, float64, float64>(10);

#endregion

#region OP_MULTIPLICATION

        [Test]
        public void Multiply_Int8_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float32, int8, float32>(10);

        [Test]
        public void Multiply_Int16_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float32, int16, float32>(10);

        [Test]
        public void Multiply_Int32_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float32, int32, float32>(10);

        [Test]
        public void Multiply_Int64_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float32, int64, float32>(10);

        [Test]
        public void Multiply_UInt8_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float32, uint8, float32>(10);

        [Test]
        public void Multiply_UInt16_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float32, uint16, float32>(10);

        [Test]
        public void Multiply_UInt32_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float32, uint32, float32>(10);

        [Test]
        public void Multiply_UInt64_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<float32, uint64, float32>(10);

        [Test]
        public void Multiply_Float32_WorksCorrectly() =>
            Utility.Multiply_T_WorksCorrectly<float32, float32, float32>(10);

        [Test]
        public void Multiply_Float64_WorksCorrectly() =>
            Utility.Multiply_T_WorksCorrectly<float32, float64, float64>(10);

#endregion

#region OP_DIVISION

        [Test] public void Divide_Int8_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float32, int8, float32>(10);

        [Test]
        public void Divide_Int16_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float32, int16, float32>(10);

        [Test]
        public void Divide_Int32_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float32, int32, float32>(10);

        [Test]
        public void Divide_Int64_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float32, int64, float32>(10);

        [Test]
        public void Divide_UInt8_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float32, uint8, float32>(10);

        [Test]
        public void Divide_UInt16_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float32, uint16, float32>(10);

        [Test]
        public void Divide_UInt32_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float32, uint32, float32>(10);

        [Test]
        public void Divide_UInt64_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float32, uint64, float32>(10);

        [Test]
        public void Divide_Float32_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float32, float32, float32>(10);

        [Test]
        public void Divide_Float64_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<float32, float64, float64>(10);

#endregion
    }
}