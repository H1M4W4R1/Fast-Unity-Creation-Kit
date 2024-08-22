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
    /// This struct represents a 16-bit unsigned integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct uint16 : IUnsignedNumber,
        IAddOperationSupport<uint8, uint16>, IAddOperationSupport<uint16, uint16>,
        IAddOperationSupport<uint32, uint32>, IAddOperationSupport<uint64, uint64>,
        IAddOperationSupport<float32, float32>, IAddOperationSupport<float64, float64>,
        IMultiplyOperationSupport<uint8, int32>, IMultiplyOperationSupport<uint16, int32>,
        IMultiplyOperationSupport<uint32, int64>, IMultiplyOperationSupport<uint64, float32>,
        IMultiplyOperationSupport<float32, float32>, IMultiplyOperationSupport<float64, float64>,
        ISubtractOperationSupport<uint8, int32>, ISubtractOperationSupport<uint16, int32>,
        ISubtractOperationSupport<uint32, uint32>, ISubtractOperationSupport<uint64, uint64>,
        ISubtractOperationSupport<float32, float32>, ISubtractOperationSupport<float64, float64>,
        IDivideOperationSupport<uint8, float32>, IDivideOperationSupport<uint16, float32>,
        IDivideOperationSupport<uint32, float32>, IDivideOperationSupport<uint64, float32>,
        IDivideOperationSupport<float32, float32>, IDivideOperationSupport<float64, float64>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private ushort _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator uint16(ushort number) => *(uint16*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator ushort(uint16 number) => *(ushort*) &number;

#region OP_ADDITION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint16 Add(uint8 rightHandSide) => (ushort) (_value + rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint16 Add(uint16 rightHandSide) => (ushort) (_value + rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint32 Add(uint32 rightHandSide) => (uint) (_value + rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Add(uint64 rightHandSide) => (ulong) (_value + rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Add(float32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(float64 rightHandSide) => _value + rightHandSide;

#endregion

#region OP_MULTIPLICATION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Multiply(uint8 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Multiply(uint16 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Multiply(uint32 rightHandSide) => (int64) (_value * rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Multiply(uint64 rightHandSide) => (float32) ((double) _value * rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Multiply(float32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(float64 rightHandSide) => _value * rightHandSide;

#endregion

#region OP_SUBTRACTION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Subtract(uint8 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Subtract(uint16 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint32 Subtract(uint32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Subtract(uint64 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Subtract(float32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(float64 rightHandSide) => _value - rightHandSide;

#endregion

#region OP_DIVISION

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
    }
}