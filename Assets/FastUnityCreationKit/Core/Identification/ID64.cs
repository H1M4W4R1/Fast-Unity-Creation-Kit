using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Core.Identification.Abstract.Identifiers;
using FastUnityCreationKit.Core.Numerics;
using Unity.Burst;
using Unity.Mathematics;

namespace FastUnityCreationKit.Core.Identification
{
    /// <summary>
    /// Represents 64-bit non-unique identifier.
    /// </summary>
    [BurstCompile]
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct ID64 : INumberIdentifier<uint64>, IEquatable<ID64>
    {
        [FieldOffset(0)] public readonly uint4 vectorized; // 16B
        
        [FieldOffset(0)] public readonly ulong value; // 8B
        [FieldOffset(8)] public readonly byte isCreated; // 1B
        [FieldOffset(9)] private readonly byte reserved0; // 1B
        [FieldOffset(10)] private readonly ushort reserved1; // 2B
        [FieldOffset(12)] private readonly uint reserved2; // 4B

        /// <inheritdoc/>
        public bool IsCreated => isCreated == 1;

        /// <summary>
        /// Creates new ID64 identifier with given value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ID64(ulong value)
        {
            // Overriden by remaining data
            vectorized = default;
            
            this.value = value;
            isCreated = 1;
            reserved0 = 0;
            reserved1 = 0;
            reserved2 = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ID64 other) => math.all(other.vectorized == vectorized);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is ID64 other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override unsafe int GetHashCode() => vectorized.GetHashCode();

        [BurstDiscard] [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => value.ToString();
        
        /// <inheritdoc/>
        public uint64 Value => value;
    }
}