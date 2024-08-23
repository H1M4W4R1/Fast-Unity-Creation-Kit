using System;
using FastUnityCreationKit.Core.Identification.Abstract.Identifiers;

namespace FastUnityCreationKit.Core.Identification.Abstract.Identifiable
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
        public TIdentifierType Identifier { get; }
    }
}