using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Features.Physics.Any;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Features.Physics.Specific
{
    /// <summary>
    /// Feature that listens for collision events.
    /// </summary>
    public abstract class Collision2DWithFeatureBase<TOtherCollider> :
        Collision2DFeatureBase
    {
        protected virtual async UniTask OnCollisionEnterWith([NotNull] Collision2D collision,
            [NotNull] TOtherCollider other) => await UniTask.CompletedTask;

        protected virtual async UniTask OnCollisionExitWith([NotNull] Collision2D collision,
            [NotNull] TOtherCollider other) => await UniTask.CompletedTask;

        protected virtual async UniTask OnCollisionStayWith([NotNull] Collision2D collision,
            [NotNull] TOtherCollider other) => await UniTask.CompletedTask;

        protected sealed override async UniTask OnCollisionEnterWith(Collision2D collision, GameObject other)
        {
            if (other.TryGetComponent(out TOtherCollider otherCollider))
                await OnCollisionEnterWith(collision, otherCollider);
        }

        protected sealed override async UniTask OnCollisionExitWith(Collision2D collision, GameObject other)
        {
            if (other.TryGetComponent(out TOtherCollider otherCollider))
                await OnCollisionExitWith(collision, otherCollider);
        }

        protected sealed override async UniTask OnCollisionStayWith(Collision2D collision, GameObject other)
        {
            if (other.TryGetComponent(out TOtherCollider otherCollider))
                await OnCollisionStayWith(collision, otherCollider);
        }
    }
}