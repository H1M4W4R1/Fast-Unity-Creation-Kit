using FastUnityCreationKit.Identification.Identifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace FastUnityCreationKit.Identification
{
    /// <summary>
    /// Represents a unique definition that is used to store unique data.
    /// </summary>
    public abstract class UniqueDefinitionBase : SerializedScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// Preview of the definition identifier.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TabGroup("Identification")] public string IdPreview => Id.ToString();
#endif

        /// <summary>
        /// Definition identifier.
        /// </summary>
        [OdinSerialize]
        [ReadOnly]
        [ShowInInspector]
        [TabGroup("Debug")]
        public Snowflake128 Id { get; private set; } = Snowflake128.New;
    }
}