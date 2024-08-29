using System;
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
    /// Represents a 128-bit vectorized number.
    /// Used as reference for SIMD operations.
    /// Requires support for SSE (Streaming SIMD Extensions) and AVX (Advanced Vector Extensions).
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile]
    [Serializable]
    public struct v128 : IVectorizedNumber, IEquatable<v128>, IEquatable<uint4>
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)]
        [SerializeField]
        private uint4 _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator v128(uint4 number) => *(v128*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator uint4(v128 number) => *(uint4*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(v128 left, v128 right) => math.all(left._value == right._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(v128 left, v128 right) => !(left == right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(uint4 other) => _value.Equals(other);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(v128 other) => this == other;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is v128 other && Equals(other);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => _value.GetHashCode();
    }
}