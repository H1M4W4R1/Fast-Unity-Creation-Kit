using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Data.Abstract;
using FastUnityCreationKit.Data.Containers.Interfaces;
using FastUnityCreationKit.Data.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FastUnityCreationKit.Data.Containers
{
    /// <summary>
    /// Storage for addressable definitions.
    /// </summary>
    public abstract class AddressableDatabase<TSelfSealed, TDataType> : AddressableDefinitionContainer<TDataType>, 
        IDatabase<TDataType>, IUniqueDataContainer
        where TSelfSealed : AddressableDatabase<TSelfSealed, TDataType>, new()
        where TDataType : Object, IDefinition<TDataType>
    {
        private static TSelfSealed _instance;
        private static AsyncOperationHandle<TSelfSealed> _operationHandle;

        /// <summary>
        /// Get the instance of the database.
        /// </summary>
        public static TSelfSealed Instance
        {
            get
            {
                // ReferenceEquals is used to check if the instance is null
                // this way we don't check Unity object lifecycle which is expensive
                // so the performance of this method is as fast as possible
                // and on-par with regular class null check.
                //
                // We assume that instance will be either null or a reference to database
                // ScriptableObject, so we don't need to check if it's destroyed.
                // The issue will arise if user will destroy the database, but it's not
                // within specification to destroy the database, as database is managed by the system. 
                if(!ReferenceEquals(_instance, null)) return _instance;
                
                // Prevent loading the database multiple times
                if(_operationHandle.IsValid() && !_operationHandle.IsDone)
                {
                    _operationHandle.WaitForCompletion();
                    _instance = _operationHandle.Result;
                    return _instance;
                }
                
                // Load the database 
                _operationHandle = Addressables.LoadAssetAsync<TSelfSealed>(typeof(TSelfSealed).Name);
                _operationHandle.WaitForCompletion();
                
                _instance = _operationHandle.Result;
                return _instance;
            }
        }
        
        [Button("Force Populate")]
        public void ForcePopulate()
        {
            Populate().Forget();
        }

        public IList RawData => (IList) All;
    }
}