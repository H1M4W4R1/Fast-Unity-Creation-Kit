using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Features.Physics.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Features.Physics.Any
{
    /// <summary>
    /// Used to detect 2D trigger events.
    /// </summary>
    public abstract class Trigger2DFeatureBase : CKTriggerFeatureBase
    {
        protected virtual async UniTask OnTriggerEnteredBy([NotNull] Collider2D detectedCollider,
            [NotNull] GameObject other) =>
            await UniTask.CompletedTask;

        protected virtual async UniTask OnTriggerExitedBy([NotNull] Collider2D detectedCollider,
            [NotNull] GameObject other) =>
            await UniTask.CompletedTask;

        protected virtual async UniTask OnTriggerStayedBy([NotNull] Collider2D detectedCollider,
            [NotNull] GameObject other) =>
            await UniTask.CompletedTask;

        protected async void OnTriggerEnter2D([NotNull] Collider2D other)
        {
            CurrentInteractionCont++;
            await OnTriggerEnteredBy(other, other.gameObject);
        }

        protected async void OnTriggerExit2D([NotNull] Collider2D other) =>
            await OnTriggerExitedBy(other, other.gameObject);

        protected async void OnTriggerStay2D([NotNull] Collider2D other)
        {
            await OnTriggerStayedBy(other, other.gameObject);
            CurrentInteractionCont--;
        }
    }
}