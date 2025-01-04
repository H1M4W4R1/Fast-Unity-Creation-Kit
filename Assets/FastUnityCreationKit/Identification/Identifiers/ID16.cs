using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Identification.Abstract.Identifiers;
using JetBrains.Annotations;
using Unity.Burst;

namespace FastUnityCreationKit.Identification.Identifiers
{
    /// <summary>
    ///     Represents 16-bit non-unique identifier.
    /// </summary>
    [BurstCompile] [StructLayout(LayoutKind.Explicit)]
    public readonly struct ID16 : INumberIdentifier<ushort>, IEquatable<ID16>
    {
        [FieldOffset(0)] public readonly ushort value;
        [FieldOffset(2)] public readonly byte isCreated;
        [FieldOffset(3)] private readonly byte reserved;

        /// <inheritdoc />
        public bool IsCreated => isCreated == 1;

        /// <summary>
        ///     Creates new ID16 identifier with given value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public ID16(ushort value)
        {
            this.value = value;
            isCreated = 1;
            reserved = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool Equals(ID16 other)
        {
            return other.value == value && other.isCreated == isCreated;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override bool Equals(object obj)
        {
            return obj is ID16 other && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override unsafe int GetHashCode()
        {
            fixed (ushort* p = &value)
            {
                return (*(int*) p).GetHashCode();
            }
        }

        [BurstDiscard] [MethodImpl(MethodImplOptions.AggressiveInlining)] [NotNull]
        public override string ToString()
        {
            return $"{value:X4}";
        }

        /// <inheritdoc />
        public ushort Value => value;
    }
}