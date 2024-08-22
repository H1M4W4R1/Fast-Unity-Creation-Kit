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
    /// This struct represents a 64-bit unsigned integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct uint64 : IUnsignedNumber, ISupportsFloatConversion<uint64>,
        IAddOperationSupport<uint8, uint64>, IAddOperationSupport<uint16, uint64>,
        IAddOperationSupport<uint32, uint64>, IAddOperationSupport<uint64, uint64>,
        IAddOperationSupport<float32, float32>, IAddOperationSupport<float64, float64>,
        IMultiplyOperationSupport<uint8, uint64>, IMultiplyOperationSupport<uint16, uint64>,
        IMultiplyOperationSupport<uint32, uint64>, IMultiplyOperationSupport<uint64, uint64>,
        IMultiplyOperationSupport<float32, float32>, IMultiplyOperationSupport<float64, float64>,
        ISubtractOperationSupport<uint8, uint64>, ISubtractOperationSupport<uint16, uint64>,
        ISubtractOperationSupport<uint32, uint64>, ISubtractOperationSupport<uint64, uint64>,
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
        private ulong _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator uint64(ulong number) => *(uint64*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator ulong(uint64 number) => *(ulong*) &number;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float32(uint64 number) => number._value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float64(uint64 number) => number._value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 FromFloat(float value) => _value = (ulong) value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 FromDouble(double value) => _value = (ulong) value;

#region OP_ADDITION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Add(uint8 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Add(uint16 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Add(uint32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Add(uint64 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Add(float32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(float64 rightHandSide) => _value + rightHandSide;

#endregion

#region OP_MULTIPLICATION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Multiply(uint8 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Multiply(uint16 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Multiply(uint32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Multiply(uint64 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Multiply(float32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(float64 rightHandSide) => _value * rightHandSide;

#endregion

#region OP_SUBTRACTION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Subtract(uint8 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Subtract(uint16 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Subtract(uint32 rightHandSide) => _value - rightHandSide;

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