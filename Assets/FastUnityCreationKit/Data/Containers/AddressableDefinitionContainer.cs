using FastUnityCreationKit.Data.Abstract;
using FastUnityCreationKit.Data.Attributes;
using FastUnityCreationKit.Data.Containers.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.Data.Containers
{
    /// <summary>
    /// Storage for addressable definitions.
    /// </summary>
    public abstract class AddressableDefinitionContainer<TDataType> : AddressableDataContainer<TDataType>
        where TDataType : ScriptableObject, IDefinition<TDataType>
    {
       
    }
}