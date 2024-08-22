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
        IMultiplyOperationSupport<uint32, float64>, IMultiplyOperationSupport<uint64, float64>
     
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Negate() => -_value;
    }
}