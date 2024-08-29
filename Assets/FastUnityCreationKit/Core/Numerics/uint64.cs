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
    /// This struct represents a 64-bit unsigned integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct uint64 : IUnsignedNumber, ISupportsFloatConversion<uint64>, IEquatable<ulong>, IEquatable<uint64>,
        IEquatable<float>, IEquatable<double>, IEquatable<byte>, IEquatable<sbyte>, IEquatable<short>, IEquatable<ushort>,
        IEquatable<int>, IEquatable<uint>, IEquatable<long>
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
        
        public bool Equals(float other) => _value.Equals(other);
        public bool Equals(double other) => _value.Equals(other);
        public bool Equals(byte other) => _value.Equals(other);
        public bool Equals(sbyte other) => _value.Equals(other);
        public bool Equals(short other) => _value.Equals(other);
        public bool Equals(ushort other) => _value.Equals(other);
        public bool Equals(int other) => _value.Equals(other);
        public bool Equals(uint other) => _value.Equals(other);
        public bool Equals(long other) => _value.Equals(other);
        public bool Equals(ulong other) => _value.Equals(other);
        public bool Equals(uint64 other) => _value.Equals(other._value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(uint64 left, ulong right) => left._value == right;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(uint64 left, ulong right) => left._value != right;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(ulong left, uint64 right) => left == right._value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(ulong left, uint64 right) => left != right._value;
    }
}