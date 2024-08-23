using System;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Core.Identification.Abstract;
using FastUnityCreationKit.Core.Identification.Abstract.Identifiers;
using FastUnityCreationKit.Core.Numerics;
using Unity.Burst;
using Unity.Mathematics;

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
    [BurstCompile] [StructLayout(LayoutKind.Explicit)]
    public struct Snowflake128 : IUniqueIdentifier, IEquatable<Snowflake128>
    {
        [FieldOffset(0)] public v128 vectorized;
        [FieldOffset(0)] public long timestamp;
        [FieldOffset(8)] public uint identifierData;
        [FieldOffset(12)] public ushort additionalData;
        [FieldOffset(14)] public byte reserved;
        [FieldOffset(15)] public byte created;

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

        public bool Equals(Snowflake128 other) =>
             other.vectorized == vectorized;
        
        public override bool Equals(object obj)
        {
            return obj is Snowflake128 other && Equals(other);
        }

        public override int GetHashCode() => vectorized.GetHashCode();
        
        public static bool operator ==(Snowflake128 left, Snowflake128 right) => left.Equals(right);
        
        public static bool operator !=(Snowflake128 left, Snowflake128 right) => !left.Equals(right);

        [BurstDiscard]
        public override string ToString() =>
             $"{timestamp:X16}-{identifierData:X8}-{additionalData:X4}";
        
    }
}