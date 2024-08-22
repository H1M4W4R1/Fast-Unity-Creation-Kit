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
    /// This struct represents a 16-bit signed integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct int16 : ISignedNumber, INegateSupport<int16>,
        IAddOperationSupport<int8, int32>, IAddOperationSupport<int16, int32>,
        IAddOperationSupport<int32, int32>, IAddOperationSupport<int64, int64>,
        IAddOperationSupport<uint8, int32>, IAddOperationSupport<uint16, int32>,
        IAddOperationSupport<uint32, int64>, IAddOperationSupport<uint64, float32>,
        IAddOperationSupport<float32, float32>, IAddOperationSupport<float64, float64>,
        IMultiplyOperationSupport<int8, int32>, IMultiplyOperationSupport<int16, int32>,
        IMultiplyOperationSupport<int32, int32>, IMultiplyOperationSupport<int64, int64>,
        IMultiplyOperationSupport<uint8, int32>, IMultiplyOperationSupport<uint16, int32>,
        IMultiplyOperationSupport<uint32, int64>, IMultiplyOperationSupport<uint64, float32>,
        IMultiplyOperationSupport<float32, float32>, IMultiplyOperationSupport<float64, float64>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private short _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator int16(short number) => *(int16*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator short(int16 number) => *(short*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Add(int8 rightHandSide) => (_value + rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Add(int16 rightHandSide) => (_value + rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Add(int32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Add(int64 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Add(uint8 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Add(uint16 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Add(uint32 rightHandSide) => (int64) (_value + rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Add(uint64 rightHandSide) => (float32) ((double) _value + rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Add(float32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(float64 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Multiply(int8 rightHandSide) => (_value * rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Multiply(int16 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Multiply(int32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Multiply(int64 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Multiply(uint8 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int32 Multiply(uint16 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Multiply(uint32 rightHandSide) => (int64) (_value * rightHandSide);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Multiply(uint64 rightHandSide) => (float32) _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Multiply(float32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(float64 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public int16 Negate() => (short) -_value;
    }
}