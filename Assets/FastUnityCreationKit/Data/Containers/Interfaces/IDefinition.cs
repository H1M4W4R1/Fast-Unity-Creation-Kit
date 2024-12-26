namespace FastUnityCreationKit.Data.Containers.Interfaces
{
    /// <summary>
    /// Definition of game object with sealed class passed as generic parameter.
    /// </summary>
    public interface IDefinition<TSelfSealed> : IDefinition
        where TSelfSealed : IDefinition<TSelfSealed>
    {
        
    }
    
    /// <summary>
    /// Represents definition of game object for example Status or Item.
    /// </summary>
    public interface IDefinition
    {
        
    }
}