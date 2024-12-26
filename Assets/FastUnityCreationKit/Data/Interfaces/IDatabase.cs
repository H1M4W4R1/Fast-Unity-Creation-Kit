using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Data.Interfaces
{
	/// <summary>
	/// Interface for database - this is only used with database objects that are stored as ScriptableObjects.
	/// This allows database items to easily access specific databases.
	/// </summary>
    public interface IDatabase<TDataType> : IDatabase, IDataContainer<TDataType>
    {
	   
    }
	
	/// <summary>
	/// Do not use this directly. <see cref="IDatabase{TDataType}"/> should be used instead.
	/// </summary>
	public interface IDatabase
	{
	}
}