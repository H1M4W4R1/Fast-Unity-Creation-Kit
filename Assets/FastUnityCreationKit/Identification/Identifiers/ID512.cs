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
    ///     Represents 512-bit non-unique identifier.
    /// </summary>
    [BurstCompile] [StructLayout(LayoutKind.Explicit)] [Serializable]
    public struct ID512 : INumberIdentifier<uint4x4>, IEquatable<ID512>
    {
        [FieldOffset(0)] [SerializeField] [HideInInspector] public uint4x4 value;
        [FieldOffset(32)] [SerializeField] [HideInInspector] public byte isCreated;


        /// <inheritdoc />
        public bool IsCreated => isCreated == 1;

        /// <summary>
        ///     Creates new ID512 identifier with given value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public ID512(uint4x4 value)
        {
            this.value = value;
            isCreated = 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool Equals(ID512 other)
        {
            return math.all(other.value.c0 == value.c0) &&
                   math.all(other.value.c1 == value.c1) &&
                   math.all(other.value.c2 == value.c2) &&
                   math.all(other.value.c3 == value.c3) &&
                   other.isCreated == isCreated;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override bool Equals(object obj)
        {
            return obj is ID512 other && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override int GetHashCode()
        {
            return value.GetHashCode() + isCreated.GetHashCode();
        }

        [BurstDiscard] [MethodImpl(MethodImplOptions.AggressiveInlining)] [NotNull]
        public override string ToString()
        {
            return $"{value.c0.x:X8}{value.c0.y:X8}-{value.c0.z:X8}{value.c0.w:X8}-" +
                   $"{value.c1.x:X8}{value.c1.y:X8}-{value.c1.z:X8}{value.c1.w:X8}-" +
                   $"{value.c2.x:X8}{value.c2.y:X8}-{value.c2.z:X8}{value.c2.w:X8}-" +
                   $"{value.c3.x:X8}{value.c3.y:X8}-{value.c3.z:X8}{value.c3.w:X8}";
        }

        /// <inheritdoc />
        public uint4x4 Value => value;
    }
}