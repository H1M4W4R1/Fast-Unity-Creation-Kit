using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Interfaces.Callbacks;

namespace FastUnityCreationKit.Examples._04_FastMonoBehaviour_UpdateModes.Scripts
{
    public abstract class CkMonoBehaviourExampleBase : CKMonoBehaviour, IUpdateCallback
    {
        public void OnObjectUpdated(float deltaTime)
        {
            // Logs message each frame with delta time.
            UnityEngine.Debug.Log($"Update called on {name}.");
        }
    }
}