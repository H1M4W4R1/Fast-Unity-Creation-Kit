using FastUnityCreationKit.Identification.Identifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
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
        [OdinSerialize] [ShowInInspector] [TitleGroup(GROUP_IDENTIFICATION)] public Snowflake128 Id { get; private set; } =
            Snowflake128.New;
    }
}