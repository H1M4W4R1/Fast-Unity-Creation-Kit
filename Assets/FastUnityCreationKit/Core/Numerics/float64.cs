using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Numerics.Types;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace FastUnityCreationKit.Core.Numerics
{
    /// <summary>
    /// This struct represents a 64-bit floating point number.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct float64 : ISignedNumber, ISupportsFloatConversion<float64>, IFloatingPointNumber,
        IEquatable<float>, IEquatable<byte>, IEquatable<sbyte>, IEquatable<short>, IEquatable<ushort>,
        IEquatable<int>, IEquatable<uint>, IEquatable<long>, IEquatable<ulong>, IEquatable<double>,
        IEquatable<float64>
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
        
        public bool Equals(float other) => _value.Equals(other);
        public bool Equals(byte other) => _value.Equals(other);
        public bool Equals(sbyte other) => _value.Equals(other);
        public bool Equals(short other) => _value.Equals(other);
        public bool Equals(ushort other) => _value.Equals(other);
        public bool Equals(int other) => _value.Equals(other);
        public bool Equals(uint other) => _value.Equals(other);
        public bool Equals(long other) => _value.Equals(other);
        public bool Equals(ulong other) => _value.Equals(other);
        public bool Equals(double other) => _value.Equals(other);
        
        public bool Equals(float64 other) => _value.Equals(other._value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(float64 left, double right) => Math.Abs(left._value - right) < math.EPSILON;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(float64 left, double right) => Math.Abs(left._value - right) > math.EPSILON;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(double left, float64 right) => Math.Abs(left - right._value) < math.EPSILON;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(double left, float64 right) => Math.Abs(left - right._value) > math.EPSILON;
    }
}