using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Identification.Abstract.Identifiers;
using JetBrains.Annotations;
using Unity.Burst;
using Unity.Mathematics;

namespace FastUnityCreationKit.Identification.Identifiers
{
    /// <summary>
    /// Represents 128-bit non-unique identifier.
    /// </summary>
    [BurstCompile]
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct ID128 : INumberIdentifier<uint4>, IEquatable<ID128>
    {
        [FieldOffset(0)] public readonly uint4 value; // 16B -> 16B
        [FieldOffset(16)] public readonly byte isCreated; // 1B -> 17B

        /// <inheritdoc/>
        public bool IsCreated => isCreated == 1;

        /// <summary>
        /// Creates new ID128 identifier with given value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ID128(uint4 value)
        {
            this.value = value;
            isCreated = 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ID128 other) => math.all(other.value == value) &&
                                           other.isCreated == isCreated;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is ID128 other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => value.GetHashCode() + isCreated.GetHashCode();

        [BurstDiscard]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public override string ToString() => $"{value.x:X8}{value.y:X8}-{value.z:X8}{value.w:X8}";

        /// <inheritdoc/>
        public uint4 Value => value;
    }
}