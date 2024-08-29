using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Core.Identification.Abstract.Identifiers;
using FastUnityCreationKit.Core.Numerics;
using Unity.Burst;

namespace FastUnityCreationKit.Core.Identification
{
    /// <summary>
    /// 128-bit unique identifier inspired by Twitter/X Snowflake.
    ///
    /// Snowflake128 consists of:
    /// <ul>
    /// <li> 64 bits for <b>timestamp</b></li>
    /// <li> 32 bits for additional identifier data</li>
    /// <li> 16 bits for additional data</li>
    /// <li> 8 bits reserved for future use</li>
    /// <li> 8 bits representing that identifier was created</li>
    /// </ul>
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct Snowflake128 : IUniqueIdentifier, IEquatable<Snowflake128>
    {
        [FieldOffset(0)] public readonly v128 vectorized;
        [FieldOffset(0)] public readonly long timestamp;
        [FieldOffset(8)] public readonly uint identifierData;
        [FieldOffset(12)] public readonly ushort additionalData;
        [FieldOffset(14)] public readonly byte reserved;
        [FieldOffset(15)] public readonly byte created;

        /// <inheritdoc/>
        public bool IsCreated => created == 1;
        
        /// <summary>
        /// Creates new Snowflake128 identifier with given timestamp, identifier data and additional data.
        /// </summary>
        public Snowflake128(long timestamp, uint identifierData, ushort additionalData)
        {
            // This value is overriden by remaining data and thus should be ignored
            vectorized = default;
            
            this.timestamp = timestamp;
            this.identifierData = identifierData;
            this.additionalData = additionalData;
            reserved = 0;
            created = 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Snowflake128 other) =>
             other.vectorized == vectorized;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is Snowflake128 other && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => vectorized.GetHashCode();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Snowflake128 left, Snowflake128 right) => left.Equals(right);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Snowflake128 left, Snowflake128 right) => !left.Equals(right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() =>
             $"{timestamp:X16}-{identifierData:X8}-{additionalData:X4}";
        
    }
}