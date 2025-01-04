using FastUnityCreationKit.Identification.Identifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace FastUnityCreationKit.Identification
{
    /// <summary>
    ///     Represents a unique definition that is used to store unique data.
    /// </summary>
    public abstract class UniqueDefinitionBase : SerializedScriptableObject
    {
        protected const string GROUP_ID = "Identification";

        /// <summary>
        ///     Definition identifier.
        /// </summary>
        // ReSharper disable once Unity.RedundantAttributeOnTarget
        [OdinSerialize] [ShowInInspector] [TitleGroup(GROUP_ID)] public Snowflake128 Id { get; private set; } =
            Snowflake128.New;
    }
}