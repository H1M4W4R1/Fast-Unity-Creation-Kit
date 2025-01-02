using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Callbacks;
using UnityEngine;

namespace FastUnityCreationKit.Examples._01_PersistentManager.Scripts
{
    /// <summary>
    /// Behaviour used to demonstrate the usage of the persistent manager.
    /// </summary>
    public sealed class PersistentManagerTestBehaviourExample : FastMonoBehaviour, IUpdateCallback
    {
        private float _tempTimerValue;
        
        public void OnObjectUpdated(float deltaTime)
        {
            // Logs message each second with random number
            // provided by the persistent manager.
            if(_tempTimerValue <= 0)
            {
                _tempTimerValue = 1;
                Debug.Log($"Update called with random value {PersistentManagerExample.Instance.GetRandomNumber()}.");
            }
            else
            {
                _tempTimerValue -= deltaTime;
            }
        }
    }
}