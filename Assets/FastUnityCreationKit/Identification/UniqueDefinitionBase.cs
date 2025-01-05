using FastUnityCreationKit.Identification.Identifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.Identification
{
    /// <summary>
    ///     Represents a unique definition that is used to store unique data.
    /// </summary>
    public abstract class UniqueDefinitionBase : SerializedScriptableObject
    {
        /// <summary>
        ///     Definition identifier.
        /// </summary>
        // ReSharper disable once Unity.RedundantAttributeOnTarget
        [ShowInInspector] [TitleGroup(GROUP_IDENTIFICATION)] [field: SerializeField, HideInInspector]
        public Snowflake128 Id { get; private set; } =
            Snowflake128.New;
    }
}