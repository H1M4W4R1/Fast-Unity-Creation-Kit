using FastUnityCreationKit.Data.Abstract;

namespace FastUnityCreationKit.Data.Containers
{
    /// <summary>
    /// Runtime Database is used to store data that is created and destroyed during runtime.
    ///
    /// It can be really helpful in terms of quickly gathering data that can be saved and loaded.
    /// </summary>
    public abstract class RuntimeDatabase<TDataType> : DataContainerBase<TDataType>
    {
        
    }
}