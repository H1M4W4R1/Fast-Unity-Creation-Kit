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
    /// This struct represents a 8-bit signed integer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct int8 : ISignedNumber, INumber<sbyte>, ISupportsFloatConversion<int8>, IEquatable<int8>,
        IEquatable<float>, IEquatable<double>, IEquatable<byte>, IEquatable<int>, IEquatable<uint>, IEquatable<long>,
        IEquatable<ulong>, IEquatable<short>, IEquatable<ushort>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private sbyte _value;

        sbyte INumber<sbyte>.Value
        {
            get => _value;
            set => _value = value;
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public float ToFloat() => _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public double ToDouble() => _value;

        [BurstDiscard] public override string ToString() => _value.ToString(CultureInfo.InvariantCulture);

        public bool Equals(int8 other) => _value.Equals(other._value);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(int8 left, sbyte right) => left._value == right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(int8 left, sbyte right) => left._value != right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(sbyte left, int8 right) => left == right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(sbyte left, int8 right) => left != right._value;

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