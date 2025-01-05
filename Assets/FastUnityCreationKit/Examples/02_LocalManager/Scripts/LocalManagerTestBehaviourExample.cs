using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Local;
using UnityEngine;

namespace FastUnityCreationKit.Examples._02_LocalManager.Scripts
{
    /// <summary>
    ///     Behaviour used to demonstrate the usage of the persistent manager.
    /// </summary>
    public sealed class LocalManagerTestBehaviourExample : CKMonoBehaviour, IOnObjectUpdateCallback
    {
        private float _tempTimerValue;

        public void OnObjectUpdate(float deltaTime)
        {
            // Logs message each second with random number
            // provided by the persistent manager.
            if (_tempTimerValue <= 0)
            {
                _tempTimerValue = 1;
                Debug.Log($"Update called with random value {LocalManagerExample.Instance.GetRandomNumber()}.");
            }
            else
            {
                _tempTimerValue -= deltaTime;
            }
        }
    }
}