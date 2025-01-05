using FastUnityCreationKit.Data.Abstract;
using FastUnityCreationKit.Saving.Abstract;
using FastUnityCreationKit.Saving.Interfaces;
using FastUnityCreationKit.Saving.Utility;
using FastUnityCreationKit.Structure.Singleton;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Data.Containers
{
    /// <summary>
    ///     Runtime Database is used to store data that is created and destroyed during runtime.
    ///     It can be really helpful in terms of quickly gathering data that can be saved and loaded
    ///     as this database implements <see cref="ISaveableObject"/>, which automatically registers
    ///     and unregisters within SaveAPI.
    /// </summary>
    /// <remarks>
    ///     This database could be used to implement for example units storage for RTS genre games.
    ///     Then you can quickly iterate through all units within <see cref="ISaveableObject.BeforeSaveSaved"/>
    ///     method and write all units data to the save file.
    /// </remarks>
    public abstract class RuntimeDatabase<TSelfSealed, TDataType> : DataContainerBase<TDataType>,
        ISingleton<TSelfSealed>, ISaveableObject
        where TSelfSealed : RuntimeDatabase<TSelfSealed, TDataType>, new()
    {
        protected RuntimeDatabase()
        {
            SaveAPI.RegisterSavableObject(this);
        }

        /// <summary>
        ///     Singleton instance.
        /// </summary>
        [NotNull] public static TSelfSealed Instance => ISingleton<TSelfSealed>.GetInstance();

        /// <summary>
        ///     Called before the save is saved.
        /// </summary>
        public virtual void BeforeSaveSaved(SaveBase toSave)
        {
            // Do nothing by default
        }

        /// <summary>
        ///     Called after the save is loaded.
        /// </summary>
        public virtual void AfterSaveLoaded(SaveBase fromSave)
        {
            // Do nothing by default
        }

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
            SaveAPI.UnregisterSavableObject(this);
        }
    }
}