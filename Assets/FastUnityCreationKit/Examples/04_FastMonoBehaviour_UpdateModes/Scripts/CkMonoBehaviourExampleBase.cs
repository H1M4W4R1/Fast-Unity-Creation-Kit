using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Interfaces.Callbacks;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Local;
using UnityEngine;

namespace FastUnityCreationKit.Examples._04_FastMonoBehaviour_UpdateModes.Scripts
{
    public abstract class CkMonoBehaviourExampleBase : CKMonoBehaviour, IOnObjectUpdateCallback
    {
        public void OnObjectUpdate(float deltaTime)
        {
            // Logs message each frame with delta time.
            Debug.Log($"Update called on {name}.");
        }
    }
}