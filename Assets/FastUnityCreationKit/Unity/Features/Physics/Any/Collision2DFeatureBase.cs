using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Features.Physics.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Features.Physics.Any
{
    /// <summary>
    /// Feature that handles collision events.
    /// </summary>
    public abstract class Collision2DFeatureBase : CKCollisionFeatureBase
    {
        protected virtual async UniTask OnCollisionEnterWith([NotNull] Collision2D collision, [NotNull] GameObject other) 
            => await UniTask.CompletedTask;
        
        protected virtual async UniTask OnCollisionExitWith([NotNull] Collision2D collision, [NotNull] GameObject other)
            => await UniTask.CompletedTask;
        
        protected virtual async UniTask OnCollisionStayWith([NotNull] Collision2D collision, [NotNull] GameObject other)
            => await UniTask.CompletedTask;
        
        protected async void OnCollisionEnter2D([NotNull] Collision2D other)
        {
            CurrentInteractionCont++;
            await OnCollisionEnterWith(other, other.gameObject);
        }
        
        protected async void OnCollisionExit2D([NotNull] Collision2D other)
        {
            await OnCollisionExitWith(other, other.gameObject);
            CurrentInteractionCont--;
        }
        
        protected async void OnCollisionStay2D([NotNull] Collision2D other)
        {
            await OnCollisionStayWith(other, other.gameObject);
        }
    }
}