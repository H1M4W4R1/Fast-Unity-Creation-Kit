using FastUnityCreationKit.Unity.Interfaces;
using FastUnityCreationKit.Unity.Structure.Managers;
using UnityEngine;

namespace FastUnityCreationKit.Examples.Unity._01_PersistentManager.Scripts
{
    /// <summary>
    /// Represents a persistent manager.
    /// This manager will be automatically created if requested for first time (if not created before)
    /// and moved to the [DontDestroyOnLoad] scene.
    /// </summary>
    public sealed class PersistentManagerExample : FastManager<PersistentManagerExample>, IPersistent
    {
        /// <summary>
        /// Example method to be called from manager.
        /// </summary>
        public int GetRandomNumber() => Random.Range(0, 100);
        
    }
}