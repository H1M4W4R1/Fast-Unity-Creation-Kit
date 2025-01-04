using JetBrains.Annotations;

namespace FastUnityCreationKit.Data.Interfaces
{
    /// <summary>
    ///     Represents an object container that can be indexed by a specific type.
    /// </summary>
    public interface IIndexableBy<out TDataType, in TIndexType>
    {
        [CanBeNull] public TDataType this[[NotNull] TIndexType index] { get; }
    }
}