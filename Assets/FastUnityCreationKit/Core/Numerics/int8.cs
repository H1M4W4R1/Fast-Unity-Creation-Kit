using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Numerics.Abstract.Operations;
using Unity.Burst;
using UnityEngine;

namespace FastUnityCreationKit.Core.Numerics
{
    /// <summary>
    /// This struct represents a 8-bit signed integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct int8 : ISignedNumber, INegateSupport<int8>, ISupportsFloatConversion<int8>,
        IAddOperationSupport<int8, int32>, IAddOperationSupport<int16, int32>,
        IAddOperationSupport<int32, int32>, IAddOperationSupport<int64, int64>,
        IAddOperationSupport<uint8, int32>, IAddOperationSupport<uint16, int32>,
        IAddOperationSupport<uint32, int64>, IAddOperationSupport<uint64, float32>,
        IAddOperationSupport<float32, float32>, IAddOperationSupport<float64, float64>,
        IMultiplyOperationSupport<int8, int32>, IMultiplyOperationSupport<int16, int32>,
        IMultiplyOperationSupport<int32, int32>, IMultiplyOperationSupport<int64, int64>,
        IMultiplyOperationSupport<uint8, int32>, IMultiplyOperationSupport<uint16, int32>,
        IMultiplyOperationSupport<uint32, int64>, IMultiplyOperationSupport<uint64, float32>,
        IMultiplyOperationSupport<float32, float32>, IMultiplyOperationSupport<float64, float64>,
        ISubtractOperationSupport<int8, int32>, ISubtractOperationSupport<int16, int32>,
        ISubtractOperationSupport<int32, int32>, ISubtractOperationSupport<int64, int64>,
        ISubtractOperationSupport<uint8, int32>, ISubtractOperationSupport<uint16, int32>,
        ISubtractOperationSupport<uint32, int64>, ISubtractOperationSupport<uint64, float32>,
        ISubtractOperationSupport<float32, float32>, ISubtractOperationSupport<float64, float64>,
        IDivideOperationSupport<int8, float32>, IDivideOperationSupport<int16, float32>,
        IDivideOperationSupport<int32, float32>, IDivideOperationSupport<int64, float32>,
        IDivideOperationSupport<uint8, float32>, IDivideOperationSupport<uint16, float32>,
        IDivideOperationSupport<uint32, float32>, IDivideOperationSupport<uint64, float32>,
        IDivideOperationSupport<float32, float32>, IDivideOperationSupport<float64, float64>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private sbyte _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator int8(sbyte number) => *(int8*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator sbyte(int8 number) => *(sbyte*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float32(int8 number) => number._value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float64(int8 number) => number._value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int8 FromFloat(float value) => _value = (sbyte) value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int8 FromDouble(double value) => _value = (sbyte) value; 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ToFloat() => _value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ToDouble() => _value;
        
#region OP_ADDITION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Add(in int8 rightHandSide) => _value + rightHandSide._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Add(in int16 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Add(in int32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Add(in int64 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Add(in uint8 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Add(in uint16 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Add(in uint32 rightHandSide) => (int64) (_value + rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Add(in uint64 rightHandSide) => (float) _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Add(in float32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(in float64 rightHandSide) => _value + rightHandSide;

#endregion

#region OP_MULTIPLICATION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Multiply(in int8 rightHandSide) => _value * rightHandSide._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Multiply(in int16 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Multiply(in int32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Multiply(in int64 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Multiply(in uint8 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Multiply(in uint16 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Multiply(in uint32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Multiply(in uint64 rightHandSide) => (float) _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Multiply(in float32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(in float64 rightHandSide) => _value * rightHandSide;

#endregion

#region OP_SUBTRACTION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Subtract(in int8 rightHandSide) => _value - rightHandSide._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Subtract(in int16 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Subtract(in int32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Subtract(in int64 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Subtract(in uint8 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Subtract(in uint16 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Subtract(in uint32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Subtract(in uint64 rightHandSide) => (float) _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Subtract(in float32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(in float64 rightHandSide) => _value - rightHandSide;

#endregion

#region OP_DIVISION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(in int8 rightHandSide) => (float) _value / rightHandSide._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(in int16 rightHandSide) => (float) _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(in int32 rightHandSide) => (float) _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(in int64 rightHandSide) => (float) _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(in uint8 rightHandSide) => (float) _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(in uint16 rightHandSide) => (float) _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(in uint32 rightHandSide) => (float) _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(in uint64 rightHandSide) => (float) _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(in float32 rightHandSide) => _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Divide(in float64 rightHandSide) => _value / rightHandSide;

#endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public int8 Negate() => (sbyte) -_value;
        
        [BurstDiscard] public override string ToString() => _value.ToString(CultureInfo.InvariantCulture);
    }
}