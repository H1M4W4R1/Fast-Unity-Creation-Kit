using FastUnityCreationKit.Data.Containers;
using FastUnityCreationKit.Data.Containers.Interfaces;
using UnityEngine;

namespace FastUnityCreationKit.Data.Interfaces
{
    /// <summary>
    /// Represents an object that has a database
    /// </summary>
    /// <typeparam name="TDatabaseType">The type of database</typeparam>
    /// <typeparam name="TSelfSealedDataType">The type of the object</typeparam>
    public interface IWithDatabase<out TDatabaseType, TSelfSealedDataType> : IWithDatabase
        where TDatabaseType : IDatabase<TSelfSealedDataType>, new() 
        where TSelfSealedDataType : ScriptableObject
    {
        /// <summary>
        /// The database of the object
        /// </summary>
        TDatabaseType Database { get; }
        
        /// <summary>
        /// Raw database of the object
        /// </summary>
        IDatabase IWithDatabase.RawDatabase => Database;
    }

    public interface IWithDatabase
    {
        IDatabase RawDatabase { get; }
    }
}