using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Callbacks;

namespace FastUnityCreationKit.Examples.Unity._04_FastMonoBehaviour_UpdateModes.Scripts
{
    public abstract class FastMonoBehaviourExampleBase : FastMonoBehaviour, IUpdateCallback
    {
        public void OnObjectUpdated(float deltaTime)
        {
            // Logs message each frame with delta time.
            UnityEngine.Debug.Log($"Update called on {name}.");
        }
    }
}