using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Core.Identification.Abstract.Identifiers;
using FastUnityCreationKit.Core.Numerics;
using Unity.Burst;

namespace FastUnityCreationKit.Core.Identification
{
    /// <summary>
    /// Simple 8-bit non-unique identifier.
    /// </summary>
    [BurstCompile]
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct ID8 : INumberIdentifier<byte>, IEquatable<ID8>
    {
        [FieldOffset(0)] public readonly byte value;
        [FieldOffset(1)] public readonly byte isCreated;
        
        /// <inheritdoc/>
        public bool IsCreated => isCreated == 1;
        
        /// <summary>
        /// Creates new ID8 identifier with given value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ID8(byte value)
        {
            this.value = value;
            isCreated = 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ID8 other) => other.value == value && other.isCreated == isCreated;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is ID8 other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override unsafe int GetHashCode()
        {
            fixed (byte* p = &value)
            {
                return (*(short*) p).GetHashCode();
            }
        }

        [BurstDiscard] [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => value.ToString();

        /// <inheritdoc/>
        public byte Value => value;
    }
}