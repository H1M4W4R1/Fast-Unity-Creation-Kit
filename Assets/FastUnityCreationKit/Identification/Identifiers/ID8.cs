﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Identification.Abstract.Identifiers;
using JetBrains.Annotations;
using Unity.Burst;
using UnityEngine;

namespace FastUnityCreationKit.Identification.Identifiers
{
    /// <summary>
    ///     Simple 8-bit non-unique identifier.
    /// </summary>
    [BurstCompile] [StructLayout(LayoutKind.Explicit)] [Serializable]
    public struct ID8 : INumberIdentifier<byte>, IEquatable<ID8>
    {
        [FieldOffset(0)] [SerializeField] [HideInInspector] private byte value;
        [FieldOffset(1)] [SerializeField] [HideInInspector] private byte isCreated;

        /// <inheritdoc />
        public bool IsCreated => isCreated == 1;

        /// <summary>
        ///     Creates new ID8 identifier with given value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public ID8(byte value)
        {
            this.value = value;
            isCreated = 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool Equals(ID8 other)
        {
            return other.value == value && other.isCreated == isCreated;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override bool Equals(object obj)
        {
            return obj is ID8 other && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override unsafe int GetHashCode()
        {
            fixed (byte* p = &value)
            {
                return (*(short*) p).GetHashCode();
            }
        }

        [BurstDiscard] [MethodImpl(MethodImplOptions.AggressiveInlining)] [NotNull]
        public override string ToString()
        {
            return $"{value:X2}";
        }

        /// <inheritdoc />
        public byte Value => value;
    }
}