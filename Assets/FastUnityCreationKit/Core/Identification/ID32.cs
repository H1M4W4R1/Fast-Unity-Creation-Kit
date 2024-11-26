using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Core.Identification.Abstract.Identifiers;
using FastUnityCreationKit.Core.Numerics;
using Unity.Burst;

namespace FastUnityCreationKit.Core.Identification
{
    /// <summary>
    /// Represents 32-bit non-unique identifier.
    /// </summary>
    [BurstCompile]
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct ID32 : INumberIdentifier<uint>, IEquatable<ID32>
    {
        [FieldOffset(0)] public readonly uint value;
        [FieldOffset(4)] public readonly byte isCreated;
        [FieldOffset(5)] private readonly byte reserved0;
        [FieldOffset(6)] private readonly ushort reserved1;

        /// <inheritdoc/>
        public bool IsCreated => isCreated == 1;

        /// <summary>
        /// Creates new ID32 identifier with given value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ID32(uint value)
        {
            this.value = value;
            isCreated = 1;
            reserved0 = 0;
            reserved1 = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ID32 other) => other.value == value && other.isCreated == isCreated;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is ID32 other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override unsafe int GetHashCode()
        {
            fixed (uint* p = &value)
            {
                return (*(long*) p).GetHashCode();
            }
        }

        [BurstDiscard] [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => value.ToString();
        
        /// <inheritdoc/>
        public uint Value => value;
    }
}