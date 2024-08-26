using FastUnityCreationKit.Core.Utility.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Core.Utility
{
    /// <summary>
    /// Represents that an object has a prefab.
    /// </summary>
    public interface IWithPrefab<out TPrefabType, [UsedImplicitly] TPrefabUsage>
        where TPrefabUsage : IPrefabUsage
        where TPrefabType : Object
    {
        /// <summary>
        /// Prefab of the object.
        /// </summary>
        public TPrefabType Prefab { get; }
    }
}