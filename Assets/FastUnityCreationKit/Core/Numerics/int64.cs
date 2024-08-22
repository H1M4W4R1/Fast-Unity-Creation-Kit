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
        IMultiplyOperationSupport<float32, float32>, IMultiplyOperationSupport<float64, float64>
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 Negate() => -_value;
    }
}