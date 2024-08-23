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
    /// This struct represents a 8-bit unsigned integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct uint8 : IUnsignedNumber, ISupportsFloatConversion<uint8>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private byte _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator uint8(byte number) => *(uint8*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator byte(uint8 number) => *(byte*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float32(uint8 number) => number._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float64(uint8 number) => number._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public uint8 FromFloat(float value) => 
            _value = (byte) math.clamp(value, byte.MinValue, byte.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint8 FromDouble(double value) => 
            _value = (byte) math.clamp(value, byte.MinValue, byte.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public float ToFloat() => _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public double ToDouble() => _value;

        [BurstDiscard] public override string ToString() => _value.ToString(CultureInfo.InvariantCulture);
    }
}