using System;
using FastUnityCreationKit.Core.Identification.Abstract.Identifiers;

namespace FastUnityCreationKit.Core.Identification.Abstract.Identifiable
{
    /// <summary>
    /// Represents an identifiable object that is uniquely identifiable.
    /// Objects that implement this interface can be identified by a designated identifier.
    /// </summary>
    /// <remarks>
    /// You must ensure that the identifier is unique across multiple objects.
    /// Beware that <see cref="TIdentifierType"/> can be any type of identifier, not just unique identifiers.
    /// </remarks>
    public interface IUniquelyIdentifiable<TIdentifierType> : IIdentifiable<TIdentifierType>
        where TIdentifierType : IIdentifier
    {
        
    }
}