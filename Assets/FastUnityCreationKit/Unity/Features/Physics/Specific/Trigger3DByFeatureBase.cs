using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Features.Physics.Any;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Features.Physics.Specific
{
    /// <summary>
    ///     Feature base for 3D triggers
    /// </summary>
    public abstract class Trigger3DByFeatureBase<TTriggeringObject> : Trigger3DFeatureBase
    {
        protected async UniTask OnTriggerEnteredBy(
            [NotNull] Collider detectedCollider,
            [NotNull] TTriggeringObject other)
        {
            await Task.CompletedTask;
        }

        protected async UniTask OnTriggerExitedBy(
            [NotNull] Collider detectedCollider,
            [NotNull] TTriggeringObject other)
        {
            await Task.CompletedTask;
        }

        protected async UniTask OnTriggerStayedBy(
            [NotNull] Collider detectedCollider,
            [NotNull] TTriggeringObject other)
        {
            await Task.CompletedTask;
        }

        protected sealed override async UniTask OnTriggerEnteredBy(Collider detectedCollider, GameObject other)
        {
            if (other.TryGetComponent(out TTriggeringObject triggeringObject))
                await OnTriggerEnteredBy(detectedCollider, triggeringObject);
        }

        protected sealed override async UniTask OnTriggerExitedBy(Collider detectedCollider, GameObject other)
        {
            if (other.TryGetComponent(out TTriggeringObject triggeringObject))
                await OnTriggerExitedBy(detectedCollider, triggeringObject);
        }

        protected sealed override async UniTask OnTriggerStayedBy(Collider detectedCollider, GameObject other)
        {
            if (other.TryGetComponent(out TTriggeringObject triggeringObject))
                await OnTriggerStayedBy(detectedCollider, triggeringObject);
        }
    }
}