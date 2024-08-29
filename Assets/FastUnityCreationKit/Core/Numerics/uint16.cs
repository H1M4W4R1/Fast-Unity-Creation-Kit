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
    /// This struct represents a 16-bit unsigned integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct uint16 : IUnsignedNumber, ISupportsFloatConversion<uint16>, IEquatable<ushort>, IEquatable<uint16>,
        IEquatable<float>, IEquatable<double>, IEquatable<byte>, IEquatable<int>, IEquatable<uint>, IEquatable<long>,
        IEquatable<ulong>, IEquatable<sbyte>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private ushort _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator uint16(ushort number) => *(uint16*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator ushort(uint16 number) => *(ushort*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float32(uint16 number) => number._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float64(uint16 number) => number._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint16 FromFloat(float value) => 
            _value = (ushort) math.clamp(value, ushort.MinValue, ushort.MaxValue); 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint16 FromDouble(double value) => 
            _value = (ushort) math.clamp(value, ushort.MinValue, ushort.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public float ToFloat() => _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public double ToDouble() => _value;


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
        public bool Equals(uint16 other) => _value.Equals(other._value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(uint16 left, ushort right) => left._value == right;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(uint16 left, ushort right) => left._value != right;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(ushort left, uint16 right) => left == right._value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(ushort left, uint16 right) => left != right._value;
        
    }
}