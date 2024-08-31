using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has a prefab.
    /// </summary>
    public interface IWithPrefab<out TPrefabType, [UsedImplicitly] TPrefabUsage>
        where TPrefabUsage : IUsageContext
        where TPrefabType : Object
    {
        /// <summary>
        /// Prefab of the object.
        /// </summary>
        public TPrefabType Prefab { get; }
    }
}