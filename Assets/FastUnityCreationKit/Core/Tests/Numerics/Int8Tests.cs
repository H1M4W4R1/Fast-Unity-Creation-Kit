using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Numerics.Abstract.Operations;
using NUnit.Framework;

namespace FastUnityCreationKit.Core.Tests.Numerics
{
    [TestFixture]
    public class Int8Tests
    {
        [Test]
        public void Operator_ImplicitConversion_FromSByte_WorksCorrectly()
        {
            sbyte value = 10;
            int8 number = value;
            Assert.AreEqual(value, (sbyte) number);
        }

        [Test]
        public void Operator_ImplicitConversion_ToSByte_WorksCorrectly()
        {
            int8 number = 10;
            sbyte value = number;
            Assert.AreEqual(value, (sbyte) number);
        }

        [Test]
        public void Operator_ExplicitConversion_ToFloat32_WorksCorrectly()
        {
            int8 number = 10;
            float32 value = (float32) number;
            Assert.AreEqual(value, (float32) number);
        }

        [Test]
        public void Operator_ExplicitConversion_ToFloat64_WorksCorrectly()
        {
            int8 number = 10;
            float64 value = (float64) number;
            Assert.AreEqual(value, (float64) number);
        }

        [Test]
        public void FromFloat_WorksCorrectly()
        {
            int8 number = new int8();
            number.FromFloat(10);
            Assert.AreEqual(10, (sbyte) number);
        }

        [Test]
        public void FromDouble_WorksCorrectly()
        {
            int8 number = new int8();
            number.FromDouble(10);
            Assert.AreEqual(10, (sbyte) number);
        }

#region OP_ADDITION

        [Test] public void Add_Int8_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int8, int8, int32>(10);
        [Test] public void Add_Int16_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int8, int16, int32>(10);
        [Test] public void Add_Int32_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int8, int32, int32>(10);
        [Test] public void Add_Int64_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int8, int64, int64>(10);
        [Test] public void Add_UInt8_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int8, uint8, int32>(10);
        [Test] public void Add_UInt16_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int8, uint16, int32>(10);
        [Test] public void Add_UInt32_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int8, uint32, int64>(10);
        [Test] public void Add_UInt64_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int8, uint64, float32>(10);
        [Test] public void Add_Float32_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int8, float32, float32>(10);
        [Test] public void Add_Float64_WorksCorrectly() => Utility.Add_T_WorksCorrectly<int8, float64, float64>(10);

#endregion

#region OP_MULTIPLICATION

        [Test] public void Multiply_Int8_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int8, int8, int32>(10);

        [Test]
        public void Multiply_Int16_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int8, int16, int32>(10);

        [Test]
        public void Multiply_Int32_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int8, int32, int32>(10);

        [Test]
        public void Multiply_Int64_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int8, int64, int64>(10);

        [Test]
        public void Multiply_UInt8_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int8, uint8, int32>(10);

        [Test]
        public void Multiply_UInt16_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int8, uint16, int32>(10);

        [Test]
        public void Multiply_UInt32_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int8, uint32, int64>(10);

        [Test]
        public void Multiply_UInt64_WorksCorrectly() => Utility.Multiply_T_WorksCorrectly<int8, uint64, float32>(10);

        [Test]
        public void Multiply_Float32_WorksCorrectly() =>
            Utility.Multiply_T_WorksCorrectly<int8, float32, float32>(10);

        [Test]
        public void Multiply_Float64_WorksCorrectly() =>
            Utility.Multiply_T_WorksCorrectly<int8, float64, float64>(10);

#endregion

#region OP_SUBTRACTION

        [Test] public void Subtract_Int8_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int8, int8, int32>(10);

        [Test]
        public void Subtract_Int16_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int8, int16, int32>(10);

        [Test]
        public void Subtract_Int32_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int8, int32, int32>(10);

        [Test]
        public void Subtract_Int64_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int8, int64, int64>(10);

        [Test]
        public void Subtract_UInt8_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int8, uint8, int32>(10);

        [Test]
        public void Subtract_UInt16_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int8, uint16, int32>(10);

        [Test]
        public void Subtract_UInt32_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int8, uint32, int64>(10);

        [Test]
        public void Subtract_UInt64_WorksCorrectly() => Utility.Subtract_T_WorksCorrectly<int8, uint64, float32>(10);

        [Test]
        public void Subtract_Float32_WorksCorrectly() =>
            Utility.Subtract_T_WorksCorrectly<int8, float32, float32>(10);

        [Test]
        public void Subtract_Float64_WorksCorrectly() =>
            Utility.Subtract_T_WorksCorrectly<int8, float64, float64>(10);

#endregion

#region OP_DIVISION

        [Test] public void Divide_Int8_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int8, int8, float32>(10);
        [Test] public void Divide_Int16_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int8, int16, float32>(10);
        [Test] public void Divide_Int32_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int8, int32, float32>(10);
        [Test] public void Divide_Int64_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int8, int64, float32>(10);
        [Test] public void Divide_UInt8_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int8, uint8, float32>(10);
        [Test] public void Divide_UInt16_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int8, uint16, float32>(10);
        [Test] public void Divide_UInt32_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int8, uint32, float32>(10);

        [Test]
        public void Divide_UInt64_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int8, uint64, float32>(10);

        [Test]
        public void Divide_Float32_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int8, float32, float32>(10);

        [Test]
        public void Divide_Float64_WorksCorrectly() => Utility.Divide_T_WorksCorrectly<int8, float64, float64>(10);

#endregion
    }
}