namespace FastUnityCreationKit.Identification.Abstract.Identifiers
{
    /// <summary>
    /// Represents identifier with number data for given number type.
    /// </summary>
    public interface INumberIdentifier<out TNumber> : INumberIdentifier 
        where TNumber : notnull
    {
        /// <summary>
        /// Gets value of the identifier.
        /// </summary>
        public TNumber Value { get; }
        
    }

    /// <summary>
    /// Represents identifier with number data.
    /// Used as low-level abstraction for number identifiers and shall be only used in
    /// "where" constraints. For actual usage, see <see cref="INumberIdentifier{TNumber}"/>
    /// </summary>
    public interface INumberIdentifier : IIdentifier
    {
        
    }
}