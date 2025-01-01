namespace FastUnityCreationKit.Data.Interfaces
{
    /// <summary>
    /// Database is unique data container that can store specified data type.
    /// </summary>
    public interface IDatabase<TDataType> : IDatabase
    {
        
    }
    
    public interface IDatabase : IUniqueDataContainer
    {
        
    }
}