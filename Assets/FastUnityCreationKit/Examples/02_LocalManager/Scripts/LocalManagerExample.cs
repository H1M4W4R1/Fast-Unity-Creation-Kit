using FastUnityCreationKit.Unity;
using UnityEngine;

namespace FastUnityCreationKit.Examples._02_LocalManager.Scripts
{
    public sealed class LocalManagerExample : CKManager<LocalManagerExample>
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