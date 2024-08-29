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
    /// This struct represents a 8-bit unsigned integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct uint8 : IUnsignedNumber, INumber<byte>, ISupportsFloatConversion<uint8>, IEquatable<uint8>,
        IEquatable<float>, IEquatable<double>, IEquatable<sbyte>, IEquatable<short>, IEquatable<ushort>,
        IEquatable<int>, IEquatable<uint>, IEquatable<long>, IEquatable<ulong>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private byte _value;

        byte INumber<byte>.Value
        {
            get => _value;
            set => _value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator uint8(byte number) => *(uint8*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator byte(uint8 number) => *(byte*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float32(uint8 number) => number._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float64(uint8 number) => number._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint8 FromFloat(float value) =>
            _value = (byte) math.clamp(value, byte.MinValue, byte.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint8 FromDouble(double value) =>
            _value = (byte) math.clamp(value, byte.MinValue, byte.MaxValue);

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
        public bool Equals(uint8 other) => _value.Equals(other._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(uint8 left, sbyte right) => left._value == right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(uint8 left, sbyte right) => left._value != right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(sbyte left, uint8 right) => left == right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(sbyte left, uint8 right) => left != right._value;

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