using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Core.Extensions;
using FastUnityCreationKit.Unity.Features.Physics.Specific;
using UnityEngine;

namespace FastUnityCreationKit.Examples._06_PhysicsFeatures.Scripts.Features
{
    public abstract class LogOnCollisionWithFeatureBase<TCollisionBehaviour> :
        Collision3DWithFeatureBase<TCollisionBehaviour>
        where TCollisionBehaviour : MonoBehaviour
    {
        protected override UniTask OnCollisionEnterWith(
            Collision collision,
            TCollisionBehaviour other)
        {
            Debug.Log($"<color={Color.green.ToHex()}>{name} collided with {other.name}.</color>");
            return UniTask.CompletedTask;
        }

        protected override UniTask OnCollisionExitWith(
            Collision collision,
            TCollisionBehaviour other)
        {
            Debug.Log($"<color={Color.red.ToHex()}>{name} stopped colliding with {other.name}.</color>");
            return UniTask.CompletedTask;
        }
    }
}