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
    /// Represents a 256-bit number.
    /// Used as reference for SIMD operations.
    /// Requires support for AVX2 (Advanced Vector Extensions 2).
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [BurstCompile] 
    [Serializable]
    public struct v256 : INumber, IVectorizedNumber
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)] [SerializeField]
        private uint4x2 _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator v256(uint4x2 number) => *(v256*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator uint4x2(v256 number) => *(uint4x2*) &number;
    }
}