using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics;
using UnityEngine;

namespace FastUnityCreationKit.Examples._06_PhysicsCallbacks.Scripts
{
    public sealed class CollisionDetector : CKMonoBehaviour, IOnCollisionEnterCallback<CollisionSourceA>,
        IOnCollisionEnterCallback<CollisionSourceB>
    {
        public void OnCollisionEntered(Collision collision, CollisionSourceA other)
        {
            Debug.Log("<color=green>Collision with A</color>");
            
        }

        public void OnCollisionEntered(Collision collision, CollisionSourceB other)
        {
            Debug.Log("<color=red>Collision with B</color>");
        } 
    }
}