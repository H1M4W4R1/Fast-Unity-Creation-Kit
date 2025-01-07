using FastUnityCreationKit.Data.Abstract;
using FastUnityCreationKit.Saving.Interfaces;
using FastUnityCreationKit.Saving.Utility;
using FastUnityCreationKit.Structure.Singleton;
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
            if (!Instance.Contains(data)) Instance.Add(data);
        }

        /// <summary>
        ///     Remove data from the database.
        /// </summary>
        public static void UnregisterData([NotNull] TDataType data)
        {
            Instance.Remove(data);
        }

        ~RuntimeDatabase()
        {
            if(this is ISaveableObject saveableObject)
                SaveAPI.UnregisterSavableObject(saveableObject);
        }
    }
}