using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Numerics.Types;
using Unity.Burst;
using UnityEngine;

namespace FastUnityCreationKit.Core.Numerics
{
    /// <summary>
    /// This struct represents a 32-bit floating point number.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct float32 : ISignedNumber, INumber<float>, ISupportsFloatConversion<float32>, IFloatingPointNumber,
        IEquatable<byte>, IEquatable<sbyte>, IEquatable<short>, IEquatable<ushort>,
        IEquatable<int>, IEquatable<uint>, IEquatable<long>, IEquatable<ulong>, IEquatable<double>,
        IEquatable<float32>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private float _value;

        float INumber<float>.Value
        {
            get => _value;
            set => _value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator float32(float number) => *(float32*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator float(float32 number) => *(float*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public float32 FromFloat(float number) => number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public float32 FromDouble(double number) => (float) number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public float ToFloat() => _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public double ToDouble() => _value;

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
        public bool Equals(float32 other) => _value.Equals(other._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(float32 left, float right) => left.Equals(right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(float32 left, float right) => !left.Equals(right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(float left, float32 right) => right.Equals(left);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(float left, float32 right) => !right.Equals(left);

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