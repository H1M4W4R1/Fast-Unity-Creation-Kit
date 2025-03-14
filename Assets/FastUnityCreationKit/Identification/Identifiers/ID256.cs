﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Identification.Abstract.Identifiers;
using JetBrains.Annotations;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace FastUnityCreationKit.Identification.Identifiers
{
    /// <summary>
    ///     Represents 256-bit non-unique identifier.
    /// </summary>
    [BurstCompile] [StructLayout(LayoutKind.Explicit)] [Serializable]
    public struct ID256 : INumberIdentifier<uint4x2>, IEquatable<ID256>
    {
        [FieldOffset(0)] [SerializeField] [HideInInspector] private uint4x2 value; // 32B -> 32B
        [FieldOffset(32)] [SerializeField] [HideInInspector] private byte isCreated; // 1B -> 33B


        /// <inheritdoc />
        public bool IsCreated => isCreated == 1;

        /// <summary>
        ///     Creates new ID256 identifier with given value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public ID256(uint4x2 value)
        {
            this.value = value;
            isCreated = 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool Equals(ID256 other)
        {
            return math.all(other.value.c0 == value.c0) &&
                   math.all(other.value.c1 == value.c1) &&
                   other.isCreated == isCreated;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override bool Equals(object obj)
        {
            return obj is ID256 other && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override int GetHashCode()
        {
            return value.GetHashCode() + isCreated.GetHashCode();
        }

        [BurstDiscard] [MethodImpl(MethodImplOptions.AggressiveInlining)] [NotNull]
        public override string ToString()
        {
            return $"{value.c0.x:X8}{value.c0.y:X8}-{value.c0.z:X8}{value.c0.w:X8}-" +
                   $"{value.c1.x:X8}{value.c1.y:X8}-{value.c1.z:X8}{value.c1.w:X8}";
        }

        /// <inheritdoc />
        public uint4x2 Value => value;
    }
}