using System;
using FastUnityCreationKit.Identification.Abstract.Identifiers;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Identification.Abstract.Identifiable
{
    /// <summary>
    /// Represents an identifiable object.
    /// Objects that implement this interface can be identified by a designated identifier.
    /// By default, all identifiable objects are not unique and supports comparison to the identifier.
    /// </summary>
    public interface IIdentifiable<TIdentifierType> : IEquatable<TIdentifierType>
        where TIdentifierType : IIdentifier
    {
        /// <summary>
        /// Identifier of the object.
        /// </summary>
        [NotNull] public TIdentifierType Identifier { get; }
    }
}