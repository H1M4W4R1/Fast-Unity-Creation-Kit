using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Core.Numerics.Abstract;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace FastUnityCreationKit.Core.Numerics
{
    /// <summary>
    /// Represents a 512-bit number.
    /// Used as reference for SIMD operations.
    /// Requires support for AVX-512 (Advanced Vector Extensions 512).
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct v512 : IVectorizedNumber, IEquatable<v512>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private uint4x4 _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator v512(uint4x4 number) => *(v512*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator uint4x4(v512 number) => *(uint4x4*) &number;

        public static bool operator ==(v512 left, v512 right) => math.all(left._value.c0 == right._value.c0) &&
                                                                 math.all(left._value.c1 == right._value.c1) &&
                                                                 math.all(left._value.c2 == right._value.c2) &&
                                                                 math.all(left._value.c3 == right._value.c3);

        public static bool operator !=(v512 left, v512 right) => !(left == right);
        
        public bool Equals(v512 other) => this == other;
        public override bool Equals(object obj) => obj is v512 other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();
    }
}