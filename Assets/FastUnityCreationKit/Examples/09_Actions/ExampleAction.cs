using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Unity.Actions;
using UnityEngine;

namespace FastUnityCreationKit.Examples._09_Actions
{
    [Serializable]
    public sealed class ExampleAction : ActionBaseWithCooldown
    {
        protected override float DefaultCooldownTime => 5f;
        
        protected override UniTask<ActionExecutionState> PerformExecution()
        {
            Debug.Log("Action executed");
            return UniTask.FromResult(ActionExecutionState.Success);
        }

        protected override UniTask OnCooldownStarted()
        {
            Debug.Log("Cooldown started");
            return UniTask.CompletedTask;
        }

        protected override UniTask OnCooldownTimePassed(double deltaTime)
        {
            // Debug.Log($"Cooldown time passed {deltaTime}");
            return UniTask.CompletedTask;
        }

        protected override UniTask OnCooldownComplete()
        {
            Debug.Log("Cooldown complete");
            return UniTask.CompletedTask;
        }

        protected override UniTask OnExecutedDuringCooldown()
        {
            Debug.Log("Executed during cooldown");
            return UniTask.CompletedTask;
        }
    }
}