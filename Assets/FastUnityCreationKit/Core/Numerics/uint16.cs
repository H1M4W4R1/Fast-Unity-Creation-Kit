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
    public struct uint16 : IUnsignedNumber, INumber<ushort>, ISupportsFloatConversion<uint16>, IEquatable<uint16>,
        IEquatable<float>, IEquatable<double>, IEquatable<byte>, IEquatable<int>, IEquatable<uint>, IEquatable<long>,
        IEquatable<ulong>, IEquatable<sbyte>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private ushort _value;

        ushort INumber<ushort>.Value
        {
            get => _value;
            set => _value = value;
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj switch
            {
                float32 number => Equals(number),
                float number => Equals(number),
                byte number => Equals(number),
                sbyte number => Equals(number),
                short number => Equals(number),
                ushort number => Equals(number),
                int number => Equals(number),
                uint number => Equals(number),
                long number => Equals(number),
                ulong number => Equals(number),
                double number => Equals(number),
                _ => false
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override int GetHashCode() => _value.GetHashCode();
    }
}