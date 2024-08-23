using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Core.Numerics.Abstract;
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
    public struct float64 : ISignedNumber, ISupportsFloatConversion<float64>, IFloatingPointNumber
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
        public float64 FromFloat(float number) => number;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float64 FromDouble(double number) => number;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ToFloat() => (float) _value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ToDouble() => _value;
        
        [BurstDiscard] public override string ToString() => _value.ToString(CultureInfo.InvariantCulture);
    }
}