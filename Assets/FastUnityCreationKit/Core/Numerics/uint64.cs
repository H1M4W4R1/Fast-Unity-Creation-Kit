using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Core.Numerics.Abstract;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace FastUnityCreationKit.Core.Numerics
{
    /// <summary>
    /// This struct represents a 64-bit unsigned integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct uint64 : IUnsignedNumber, ISupportsFloatConversion<uint64>
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
        public uint64 FromFloat(float value) =>
            _value = (ulong) math.clamp(value, ulong.MinValue, ulong.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint64 FromDouble(double value) => 
            _value = (ulong) math.clamp(value, ulong.MinValue, ulong.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ToFloat() => _value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ToDouble() => _value;
        
        [BurstDiscard] public override string ToString() => _value.ToString(CultureInfo.InvariantCulture);
    }
}