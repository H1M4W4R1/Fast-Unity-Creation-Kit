using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Features.Physics.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Features.Physics.Any
{
    /// <summary>
    /// Used to detect 3D trigger events.
    /// </summary>
    public abstract class Trigger3DFeatureBase : CKTriggerFeatureBase
    {
        protected virtual async UniTask OnTriggerEnteredBy([NotNull] Collider detectedCollider,
            [NotNull] GameObject other) =>
            await UniTask.CompletedTask;

        protected virtual async UniTask OnTriggerExitedBy([NotNull] Collider detectedCollider,
            [NotNull] GameObject other) =>
            await UniTask.CompletedTask;

        protected virtual async UniTask OnTriggerStayedBy([NotNull] Collider detectedCollider,
            [NotNull] GameObject other) =>
            await UniTask.CompletedTask;

        protected async void OnTriggerEnter([NotNull] Collider other)
        {
            CurrentInteractionCont++;
            await OnTriggerEnteredBy(other, other.gameObject);
        }

        protected async void OnTriggerExit([NotNull] Collider other) =>
            await OnTriggerExitedBy(other, other.gameObject);

        protected async void OnTriggerStay([NotNull] Collider other)
        {
            await OnTriggerStayedBy(other, other.gameObject);
            CurrentInteractionCont--;
        }
    }
}