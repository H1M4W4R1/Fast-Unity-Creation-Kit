using FastUnityCreationKit.Examples.Unity._01_PersistentManager.Scripts;
using FastUnityCreationKit.Unity.Structure.Managers;
using UnityEngine;

namespace FastUnityCreationKit.Examples.Unity._02_LocalManager.Scripts
{
    /// </summary>
    public sealed class LocalManagerExample : FastManager<PersistentManagerExample>
    {
        /// <summary>
        /// Example method to be called from manager.
        /// </summary>
        public int GetRandomNumber() => Random.Range(0, 100);
        
    }
}