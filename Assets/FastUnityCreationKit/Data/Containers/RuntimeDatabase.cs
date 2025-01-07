using FastUnityCreationKit.Core.Singleton;
using FastUnityCreationKit.Data.Abstract;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Saving.Interfaces;
using FastUnityCreationKit.Saving.Utility;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Data.Containers
{
    /// <summary>
    ///     Runtime Database is used to store data that is created and destroyed during runtime.
    ///     It can be really helpful in terms of quickly gathering data that can be saved and loaded
    ///     when this database implements <see cref="ISaveableObject"/>, which automatically registers
    ///     and unregisters within SaveAPI.
    /// </summary>
    /// <remarks>
    ///     This database could be used to implement for example units storage for RTS genre games.
    ///     Then you can quickly iterate through all units within <see cref="ISaveableObject.BeforeSaveSaved"/>
    ///     method and write all units data to the save file by implementing <see cref="ISaveableObject"/>.
    /// </remarks>
    public abstract class RuntimeDatabase<TSelfSealed, TDataType> : DataContainerBase<TDataType>,
        ISingleton<TSelfSealed>
        where TSelfSealed : RuntimeDatabase<TSelfSealed, TDataType>, new()
    {
        protected RuntimeDatabase()
        {
            if(this is ISaveableObject saveableObject)
                SaveAPI.RegisterSavableObject(saveableObject);
        }

        /// <summary>
        ///     Singleton instance.
        /// </summary>
        [NotNull] public static TSelfSealed Instance => ISingleton<TSelfSealed>.GetInstance();

        /// <summary>
        ///     Register data to the database.
        /// </summary>
        public static void RegisterData([NotNull] TDataType data)
        {
            // Some instances may implement IUniqueDataContainer, so we need to check if the data is already there.
            if (Instance is not IUniqueDataContainer || !Instance.Contains(data))
            {
                Instance.Add(data);
            }
        }

        /// <summary>
        ///     Remove data from the database (first found instance).
        /// </summary>
        public static void UnregisterData([NotNull] TDataType data)
        {
            Instance.Remove(data);
        }
        
        /// <summary>
        ///     Unregister all data that is same as provided one from the database.
        /// </summary>
        public static void UnregisterAllData([NotNull] TDataType data)
        {
            // Handle the case when the instance is IUniqueDataContainer - only one data instance can be stored,
            // so we can just remove it.
            if (Instance is IUniqueDataContainer)
            {
                Instance.Remove(data);
            }
            else
            {
                // Handle the case when the instance is not IUniqueDataContainer - multiple data instances can be stored,
                // so we need to iterate through all data instances and remove them.
                for (int i = 0; i < Instance.Count; i++)
                {
                    // Check if the data is the same as the one we want to remove.
                    // We check against data as Instance[i] may be null, this way we don't need to check for null.
                    if(!data.Equals(Instance[i])) continue;
                    
                    // Unregister the data.
                    Instance.Remove(data);
                }
            }
        }

        ~RuntimeDatabase()
        {
            if(this is ISaveableObject saveableObject)
                SaveAPI.UnregisterSavableObject(saveableObject);
        }
    }
}