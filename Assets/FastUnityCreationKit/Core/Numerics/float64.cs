using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Numerics.Abstract.Operations;
using Unity.Burst;
using UnityEngine;

namespace FastUnityCreationKit.Core.Numerics
{
    /// <summary>
    /// This struct represents a 64-bit floating point number.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct float64 : ISignedNumber, INegateSupport<float64>,
        IAddOperationSupport<float64, float64>, IAddOperationSupport<float32, float64>,
        IAddOperationSupport<int8, float64>, IAddOperationSupport<int16, float64>,
        IAddOperationSupport<int32, float64>, IAddOperationSupport<int64, float64>,
        IAddOperationSupport<uint8, float64>, IAddOperationSupport<uint16, float64>,
        IAddOperationSupport<uint32, float64>, IAddOperationSupport<uint64, float64>,
        IMultiplyOperationSupport<float64, float64>, IMultiplyOperationSupport<float32, float64>,
        IMultiplyOperationSupport<int8, float64>, IMultiplyOperationSupport<int16, float64>,
        IMultiplyOperationSupport<int32, float64>, IMultiplyOperationSupport<int64, float64>,
        IMultiplyOperationSupport<uint8, float64>, IMultiplyOperationSupport<uint16, float64>,
        IMultiplyOperationSupport<uint32, float64>, IMultiplyOperationSupport<uint64, float64>,
        ISubtractOperationSupport<float64, float64>, ISubtractOperationSupport<float32, float64>,
        ISubtractOperationSupport<int8, float64>, ISubtractOperationSupport<int16, float64>,
        ISubtractOperationSupport<int32, float64>, ISubtractOperationSupport<int64, float64>,
        ISubtractOperationSupport<uint8, float64>, ISubtractOperationSupport<uint16, float64>,
        ISubtractOperationSupport<uint32, float64>, ISubtractOperationSupport<uint64, float64>,
        IDivideOperationSupport<float64, float64>, IDivideOperationSupport<float32, float64>,
        IDivideOperationSupport<int8, float64>, IDivideOperationSupport<int16, float64>,
        IDivideOperationSupport<int32, float64>, IDivideOperationSupport<int64, float64>,
        IDivideOperationSupport<uint8, float64>, IDivideOperationSupport<uint16, float64>,
        IDivideOperationSupport<uint32, float64>, IDivideOperationSupport<uint64, float64>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private double _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator float64(double number) => *(float64*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator double(float64 number) => *(double*) &number;

#region OP_ADDITION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(float64 rightHandSide) => _value + rightHandSide._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(float32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(int8 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(int16 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(int32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(int64 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(uint8 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(uint16 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(uint32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(uint64 rightHandSide) => _value + rightHandSide;

#endregion

#region OP_MULTIPLICATION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(float64 rightHandSide) => _value * rightHandSide._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(float32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(int8 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(int16 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(int32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(int64 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(uint8 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(uint16 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(uint32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(uint64 rightHandSide) => _value * rightHandSide;

#endregion

#region OP_SUBTRACTION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(float64 rightHandSide) => _value - rightHandSide._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(float32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(int8 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(int16 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(int32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(int64 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(uint8 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(uint16 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(uint32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(uint64 rightHandSide) => _value - rightHandSide;

#endregion

#region OP_DIVISION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Divide(float64 rightHandSide) => _value / rightHandSide._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Divide(float32 rightHandSide) => _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Divide(int8 rightHandSide) => _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Divide(int16 rightHandSide) => _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Divide(int32 rightHandSide) => _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Divide(int64 rightHandSide) => _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Divide(uint8 rightHandSide) => _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Divide(uint16 rightHandSide) => _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Divide(uint32 rightHandSide) => _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Divide(uint64 rightHandSide) => _value / rightHandSide;

#endregion


        [MethodImpl(MethodImplOptions.AggressiveInlining)] public float64 Negate() => -_value;
    }
}