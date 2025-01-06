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

        
    }
}