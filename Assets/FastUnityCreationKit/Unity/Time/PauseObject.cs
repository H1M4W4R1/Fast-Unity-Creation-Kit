using System;
using System.Runtime.CompilerServices;
using FastUnityCreationKit.Identification.Identifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace FastUnityCreationKit.Unity.Time
{
    /// <summary>
    /// Represents a pause object - used to pause the game.
    /// </summary>
    [Serializable]
    public struct PauseObject : IEquatable<PauseObject>
    {
        /// <summary>
        /// Identifier of the pause object.
        /// </summary>
        [OdinSerialize] [ShowInInspector] [ReadOnly]
        public Snowflake128 Id { get; private set; }

        /// <summary>
        /// Creates a new pause object.
        /// </summary>
        public PauseObject(Snowflake128 id)
        {
            Id = id;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(PauseObject a, PauseObject b) => a.Id == b.Id;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(PauseObject a, PauseObject b) => a.Id != b.Id;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is PauseObject other && other.Id == Id;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => Id.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(PauseObject other) => other.Id == Id;
    }
}