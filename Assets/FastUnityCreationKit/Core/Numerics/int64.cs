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
    /// This struct represents a 64-bit signed integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct int64 : ISignedNumber, INumber<long>, ISupportsFloatConversion<int64>, IEquatable<int64>,
        IEquatable<float>, IEquatable<double>, IEquatable<byte>, IEquatable<sbyte>, IEquatable<short>,
        IEquatable<ushort>, IEquatable<int>, IEquatable<uint>, IEquatable<ulong>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private long _value;

        long INumber<long>.Value
        {
            get => _value;
            set => _value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator int64(long number) => *(int64*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator long(int64 number) => *(long*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float32(int64 number) => number._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float64(int64 number) => number._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 FromFloat(float value) =>
            _value = (long) math.clamp(value, long.MinValue, long.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int64 FromDouble(double value)
            => _value = (long) math.clamp(value, long.MinValue, long.MaxValue);

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
        public bool Equals(int64 other) => _value.Equals(other._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(int64 left, long right) => left._value == right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(int64 left, long right) => left._value != right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(long left, int64 right) => left == right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(long left, int64 right) => left != right._value;

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