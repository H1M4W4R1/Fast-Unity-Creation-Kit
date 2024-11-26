namespace FastUnityCreationKit.Identification.Abstract.Identifiers
{
    /// <summary>
    /// Represents an identifier - a value that can be used to identify an object.
    /// Warning: this does not imply that the identifier is unique. For that purpose
    /// see <see cref="IUniqueIdentifier"/>.
    /// </summary>
    public interface IIdentifier
    {
        /// <summary>
        /// Checks if the identifier was created.
        /// </summary>
        public bool IsCreated { get; }
    }
}