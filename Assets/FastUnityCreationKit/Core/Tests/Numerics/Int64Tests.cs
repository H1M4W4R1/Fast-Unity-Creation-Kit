using FastUnityCreationKit.Core.Numerics;
using NUnit.Framework;

namespace FastUnityCreationKit.Core.Tests.Numerics
{
    [TestFixture]
    public class Int64Tests
    {
        
        [Test]
        public void Operator_ImplicitConversion_FromInt64_WorksCorrectly()
        {
            long value = 10;
            int64 number = value;
            Assert.AreEqual(value, (long) number);
        }
        
        [Test]
        public void Operator_ImplicitConversion_ToInt64_WorksCorrectly()
        {
            int64 number = 10;
            long value = number;
            Assert.AreEqual(value, (long) number);
        }
        
        [Test]
        public void ToFloat_WorksCorrectly()
        {
            int64 number = new int64();
            number.FromFloat(10);
            Assert.AreEqual(10, (long) number);
        }
        
        [Test]
        public void ToDouble_WorksCorrectly()
        {
            int64 number = new int64();
            number.FromDouble(10);
            Assert.AreEqual(10, (long) number);
        }
        
        [Test]
        public void FromFloat_WorksCorrectly()
        {
            long number = 10;
            int64 expected = number;
            int64 convertedNumber = new int64();
            convertedNumber = convertedNumber.FromFloat(number);
            Assert.AreEqual((long) expected, (long) convertedNumber);
        }
        
        [Test]
        public void FromDouble_WorksCorrectly()
        {
            double number = 10.0;
            int64 expected = 10;
            int64 convertedNumber = new int64();
            convertedNumber = convertedNumber.FromDouble(number);
            Assert.AreEqual((long) expected, (long) convertedNumber);
        }
        
        #region OP_ADDITION
        
        [Test] public void Add_Int8_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int64, int8, int64>(10);
        [Test] public void Add_Int16_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int64, int16, int64>(10);
        [Test] public void Add_Int32_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int64, int32, int64>(10);
        [Test] public void Add_Int64_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int64, int64, int64>(10);
        [Test] public void Add_UInt8_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int64, uint8, int64>(10);
        [Test] public void Add_UInt16_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int64, uint16, int64>(10);
        [Test] public void Add_UInt32_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int64, uint32, int64>(10);
        [Test] public void Add_UInt64_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int64, uint64, float32>(10);
        [Test] public void Add_Float32_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int64, float32, float32>(10);
        [Test] public void Add_Float64_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int64, float64, float64>(10);
        
        #endregion
        
        #region OP_MULTIPLICATION
        
        [Test] public void Multiply_Int8_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int64, int8, int64>(10);
        [Test] public void Multiply_Int16_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int64, int16, int64>(10);
        [Test] public void Multiply_Int32_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int64, int32, int64>(10);
        [Test] public void Multiply_Int64_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int64, int64, int64>(10);
        [Test] public void Multiply_UInt8_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int64, uint8, int64>(10);
        [Test] public void Multiply_UInt16_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int64, uint16, int64>(10);
        [Test] public void Multiply_UInt32_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int64, uint32, int64>(10);
        [Test] public void Multiply_UInt64_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int64, uint64, float32>(10);
        [Test] public void Multiply_Float32_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int64, float32, float32>(10);
        [Test] public void Multiply_Float64_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int64, float64, float64>(10);
        
        #endregion
        
        #region OP_DIVISION
        
        [Test] public void Divide_Int8_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int64, int8, float32>(10);
        [Test] public void Divide_Int16_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int64, int16, float32>(10);
        [Test] public void Divide_Int32_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int64, int32, float32>(10);
        [Test] public void Divide_Int64_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int64, int64, float32>(10);
        [Test] public void Divide_UInt8_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int64, uint8, float32>(10);
        [Test] public void Divide_UInt16_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int64, uint16, float32>(10);
        [Test] public void Divide_UInt32_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int64, uint32, float32>(10);
        [Test] public void Divide_UInt64_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int64, uint64, float32>(10);
        [Test] public void Divide_Float32_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int64, float32, float32>(10);
        [Test] public void Divide_Float64_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int64, float64, float64>(10);
        
        #endregion
        
        #region OP_SUBTRACTION
        
        [Test] public void Subtract_Int8_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int64, int8, int64>(10);
        [Test] public void Subtract_Int16_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int64, int16, int64>(10);
        [Test] public void Subtract_Int32_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int64, int32, int64>(10);
        [Test] public void Subtract_Int64_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int64, int64, int64>(10);
        [Test] public void Subtract_UInt8_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int64, uint8, int64>(10);
        [Test] public void Subtract_UInt16_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int64, uint16, int64>(10);
        [Test] public void Subtract_UInt32_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int64, uint32, int64>(10);
        [Test] public void Subtract_UInt64_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int64, uint64, float32>(10);
        [Test] public void Subtract_Float32_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int64, float32, float32>(10);
        [Test] public void Subtract_Float64_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int64, float64, float64>(10);
        
        #endregion
    }
}