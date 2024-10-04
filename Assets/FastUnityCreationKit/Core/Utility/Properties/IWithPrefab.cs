using FastUnityCreationKit.Context.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has a prefab.
    /// </summary>
    public interface IWithPrefab<TPrefabType, [UsedImplicitly] TPrefabUsage> : IWithPrefab,
        IWithProperty<IWithPrefab<TPrefabType, TPrefabUsage>, IWithPrefab<TPrefabType, AnyUsageContext>, TPrefabType,
            TPrefabUsage>
        where TPrefabUsage : IUsageContext
        where TPrefabType : Object
    {
        /// <summary>
        /// Prefab of the object.
        /// </summary>
        public TPrefabType Prefab { get; }

        TPrefabType IWithProperty<IWithPrefab<TPrefabType, TPrefabUsage>, IWithPrefab<TPrefabType, AnyUsageContext>,
            TPrefabType, TPrefabUsage>.Property => Prefab;
    }

    public interface IWithPrefab
    {
        /// <summary>
        /// Tries to get the prefab of the specified usage context.
        /// </summary>
        [CanBeNull]
        public TPrefabType GetPrefab<TPrefabType, TPrefabUsage>()
            where TPrefabType : Object
            where TPrefabUsage : IUsageContext
            => IWithProperty<IWithPrefab<TPrefabType, TPrefabUsage>, IWithPrefab<TPrefabType, AnyUsageContext>,
                    TPrefabType, TPrefabUsage>
                .GetProperty(this);
    }
}