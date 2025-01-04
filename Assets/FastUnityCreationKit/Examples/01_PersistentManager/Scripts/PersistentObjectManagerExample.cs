using FastUnityCreationKit.Core.Objects;
using FastUnityCreationKit.Unity;
using UnityEngine;

namespace FastUnityCreationKit.Examples._01_PersistentManager.Scripts
{
    /// <summary>
    ///     Represents a persistent manager.
    ///     This manager will be automatically created if requested for first time (if not created before)
    ///     and moved to the [DontDestroyOnLoad] scene.
    /// </summary>
    public sealed class PersistentObjectManagerExample : CKManager<PersistentObjectManagerExample>,
        IPersistentObject
    {
        /// <summary>
        ///     Example method to be called from manager.
        /// </summary>
        public int GetRandomNumber()
        {
            return Random.Range(0, 100);
        }
    }
}