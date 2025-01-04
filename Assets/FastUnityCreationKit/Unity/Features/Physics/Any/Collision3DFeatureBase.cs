using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Features.Physics.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Features.Physics.Any
{
    /// <summary>
    ///     Feature that handles collision events.
    /// </summary>
    public abstract class Collision3DFeatureBase : CKCollisionFeatureBase
    {
        protected async void OnCollisionEnter([NotNull] Collision other)
        {
            CurrentInteractionCont++;
            await OnCollisionEnterWith(other, other.gameObject);
        }

        protected async void OnCollisionExit([NotNull] Collision other)
        {
            await OnCollisionExitWith(other, other.gameObject);
        }

        protected async void OnCollisionStay([NotNull] Collision other)
        {
            await OnCollisionStayWith(other, other.gameObject);
            CurrentInteractionCont--;
        }

        protected virtual async UniTask OnCollisionEnterWith(
            [NotNull] Collision collision,
            [NotNull] GameObject other)
        {
            await UniTask.CompletedTask;
        }

        protected virtual async UniTask OnCollisionExitWith(
            [NotNull] Collision collision,
            [NotNull] GameObject other)
        {
            await UniTask.CompletedTask;
        }

        protected virtual async UniTask OnCollisionStayWith(
            [NotNull] Collision collision,
            [NotNull] GameObject other)
        {
            await UniTask.CompletedTask;
        }
    }
}