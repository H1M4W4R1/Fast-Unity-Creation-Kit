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
    /// This struct represents a 64-bit signed integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct int64 : ISignedNumber, INegateSupport<int64>,
        IAddOperationSupport<int8, int64>, IAddOperationSupport<int16, int64>,
        IAddOperationSupport<int32, int64>, IAddOperationSupport<int64, int64>,
        IAddOperationSupport<uint8, int64>, IAddOperationSupport<uint16, int64>,
        IAddOperationSupport<uint32, int64>, IAddOperationSupport<uint64, float32>,
        IAddOperationSupport<float32, float32>, IAddOperationSupport<float64, float64>,
        IMultiplyOperationSupport<int8, int64>, IMultiplyOperationSupport<int16, int64>,
        IMultiplyOperationSupport<int32, int64>, IMultiplyOperationSupport<int64, int64>,
        IMultiplyOperationSupport<uint8, int64>, IMultiplyOperationSupport<uint16, int64>,
        IMultiplyOperationSupport<uint32, int64>, IMultiplyOperationSupport<uint64, float32>,
        IMultiplyOperationSupport<float32, float32>, IMultiplyOperationSupport<float64, float64>,
        ISubtractOperationSupport<int8, int64>, ISubtractOperationSupport<int16, int64>,
        ISubtractOperationSupport<int32, int64>, ISubtractOperationSupport<int64, int64>,
        ISubtractOperationSupport<uint8, int64>, ISubtractOperationSupport<uint16, int64>,
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
        private long _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator int64(long number) => *(int64*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator long(int64 number) => *(long*) &number;

#region OP_ADDITION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Add(int8 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Add(int16 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Add(int32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Add(int64 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Add(uint8 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Add(uint16 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Add(uint32 rightHandSide) => (int64) (_value + rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Add(uint64 rightHandSide) => (float32) ((double) _value + rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Add(float32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(float64 rightHandSide) => _value + rightHandSide;

#endregion

#region OP_MULTIPLICATION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Multiply(int8 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Multiply(int16 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Multiply(int32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Multiply(int64 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Multiply(uint8 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Multiply(uint16 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Multiply(uint32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Multiply(uint64 rightHandSide) => (float32) _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Multiply(float32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(float64 rightHandSide) => _value * rightHandSide;

#endregion

#region OP_SUBTRACTION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Subtract(int8 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Subtract(int16 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Subtract(int32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Subtract(int64 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Subtract(uint8 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Subtract(uint16 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Subtract(uint32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Subtract(uint64 rightHandSide) => (float) _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Subtract(float32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(float64 rightHandSide) => _value - rightHandSide;

#endregion

#region OP_DIVISION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(int8 rightHandSide) => (float) _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(int16 rightHandSide) => (float) _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(int32 rightHandSide) => (float) _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(int64 rightHandSide) => (float) _value / rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(uint8 rightHandSide) => (float) _value / rightHandSide;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(uint16 rightHandSide) => (float) _value / rightHandSide;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(uint32 rightHandSide) => (float) _value / rightHandSide;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(uint64 rightHandSide) => (float) _value / rightHandSide;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Divide(float32 rightHandSide) => _value / rightHandSide;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Divide(float64 rightHandSide) => _value / rightHandSide;

#endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Negate() => -_value;
    }
}