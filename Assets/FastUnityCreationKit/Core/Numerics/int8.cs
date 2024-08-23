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
    /// This struct represents a 8-bit signed integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct int8 : ISignedNumber, ISupportsFloatConversion<int8>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private sbyte _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator int8(sbyte number) => *(int8*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator sbyte(int8 number) => *(sbyte*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float32(int8 number) => number._value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float64(int8 number) => number._value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int8 FromFloat(float value) => 
            _value = (sbyte) math.clamp(value, sbyte.MinValue, sbyte.MaxValue);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int8 FromDouble(double value) => 
            _value = (sbyte) math.clamp(value, sbyte.MinValue, sbyte.MaxValue);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ToFloat() => _value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ToDouble() => _value;

        [BurstDiscard] public override string ToString() => _value.ToString(CultureInfo.InvariantCulture);
    }
}