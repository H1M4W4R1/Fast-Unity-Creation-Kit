using FastUnityCreationKit.Identification.Identifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace FastUnityCreationKit.Identification
{
    /// <summary>
    /// Represents a unique definition that is used to store unique data.
    /// </summary>
    public abstract class UniqueDefinitionBase : SerializedScriptableObject
    {
        protected const string GROUP_ID = "Identification";
        
#if UNITY_EDITOR
        /// <summary>
        /// Preview of the definition identifier.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_ID)]  public string IdPreview => Id.ToString();
#endif

        /// <summary>
        /// Definition identifier.
        /// </summary>
        // ReSharper disable once Unity.RedundantAttributeOnTarget
        [OdinSerialize] [HideInInspector]
        public Snowflake128 Id { get; private set; } = Snowflake128.New;
    }
}