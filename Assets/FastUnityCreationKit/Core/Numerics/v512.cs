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
    public struct v512 : INumber, IVectorizedNumber
    {
        /// <summary>
        /// Current value of the number.
        /// </summary>
        [FieldOffset(0)] [SerializeField]
        private uint4x4 _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator v512(uint4x4 number) => *(v512*) &number;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe implicit operator uint4x4(v512 number) => *(uint4x4*) &number;
    }
}