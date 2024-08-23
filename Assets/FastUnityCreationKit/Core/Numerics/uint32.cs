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
    /// This struct represents a 32-bit unsigned integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct uint32 : IUnsignedNumber, ISupportsFloatConversion<uint32>,
        IAddOperationSupport<uint8, uint32>, IAddOperationSupport<uint16, uint32>,
        IAddOperationSupport<uint32, uint32>, IAddOperationSupport<uint64, uint64>,
        IAddOperationSupport<float32, float32>, IAddOperationSupport<float64, float64>,
        IMultiplyOperationSupport<uint8, uint32>, IMultiplyOperationSupport<uint16, uint32>,
        IMultiplyOperationSupport<uint32, uint64>, IMultiplyOperationSupport<uint64, uint64>,
        IMultiplyOperationSupport<float32, float32>, IMultiplyOperationSupport<float64, float64>,
        ISubtractOperationSupport<uint8, uint32>, ISubtractOperationSupport<uint16, uint32>,
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
        private uint _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator uint32(uint number) => *(uint32*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator uint(uint32 number) => *(uint*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float32(uint32 number) => number._value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float64(uint32 number) => number._value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint32 FromFloat(float value) => _value = (uint) value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint32 FromDouble(double value) => _value = (uint) value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ToFloat() => _value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ToDouble() => _value;
        
#region OP_ADDITION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint32 Add(in uint8 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint32 Add(in uint16 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint32 Add(in uint32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Add(in uint64 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Add(in float32 rightHandSide) => _value + rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Add(in float64 rightHandSide) => _value + rightHandSide;

#endregion

#region OP_MULTIPLICATION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint32 Multiply(in uint8 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint32 Multiply(in uint16 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Multiply(in uint32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Multiply(in uint64 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Multiply(in float32 rightHandSide) => _value * rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Multiply(in float64 rightHandSide) => _value * rightHandSide;

#endregion

#region OP_SUBTRACTION

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint32 Subtract(in uint8 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint32 Subtract(in uint16 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint32 Subtract(in uint32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 Subtract(in uint64 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float32 Subtract(in float32 rightHandSide) => _value - rightHandSide;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 Subtract(in float64 rightHandSide) => _value - rightHandSide;

#endregion

#region OP_DIVISION

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
            
            [BurstDiscard] public override string ToString() => _value.ToString(CultureInfo.InvariantCulture);
    }
}