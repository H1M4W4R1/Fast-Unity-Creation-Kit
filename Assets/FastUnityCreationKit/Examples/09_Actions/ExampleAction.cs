using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Actions;
using FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks;
using FastUnityCreationKit.Unity.Actions.Interfaces.Results;
using FastUnityCreationKit.Unity.Actions.Results;
using UnityEngine;

namespace FastUnityCreationKit.Examples._09_Actions
{
    [Serializable]
    public sealed class ExampleAction : ActionBaseWithCooldown, IActionIsOnCooldownCallback,
        IActionExecutedSuccessfullyCallback
    {
        protected override float DefaultCooldownTime => 5f;
        
        protected override UniTask<IActionResult> PerformExecution()
        {
            Debug.Log("Action executed");
            return UniTask.FromResult<IActionResult>(new DefaultActionSuccessResult());
        }

        protected override UniTask OnCooldownStarted()
        {
            Debug.Log("Cooldown started");
            return UniTask.CompletedTask;
        }

        protected override UniTask OnCooldownComplete()
        {
            Debug.Log("Cooldown complete");
            return UniTask.CompletedTask;
        }

        public UniTask OnExecutedWhenOnCooldown()
        {
            Debug.Log("Executed when on cooldown");
            return UniTask.CompletedTask;
        }

        public UniTask OnExecuted()
        {
            Debug.Log("Executed successfully");
            return UniTask.CompletedTask;
        }
    }
}