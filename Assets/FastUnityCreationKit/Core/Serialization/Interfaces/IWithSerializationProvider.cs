namespace FastUnityCreationKit.Core.Serialization.Interfaces
{
    /// <summary>
    /// Represents an interface that allows to set a serialization provider.
    /// </summary>
    public interface IWithSerializationProvider<TSerializationProvider> 
        where TSerializationProvider : ISerializationProvider
    {
        
    }
}