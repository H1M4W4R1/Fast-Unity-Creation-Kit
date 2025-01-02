using FastUnityCreationKit.Unity.Structure.Managers;
using UnityEngine;

namespace FastUnityCreationKit.Examples._02_LocalManager.Scripts
{
    /// </summary>
    public sealed class LocalManagerExample : FastManager<LocalManagerExample>
    {
        /// <summary>
        /// Example method to be called from manager.
        /// </summary>
        public int GetRandomNumber() => Random.Range(0, 100);
        
    }
}