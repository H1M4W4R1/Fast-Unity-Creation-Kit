using System;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Callbacks;
using UnityEngine;

namespace FastUnityCreationKit.Examples.Unity._03_FastMonoBehaviour_TimeModes.Scripts
{
    public sealed class FixedUpdateFastMonoBehaviourExample : TimeMonoBehaviourExample, IFixedUpdateCallback
    {
        protected override string GetMessage(float deltaTime) => $"Update called with deltaTime: {TimeAPI.FixedDeltaTime}";
        
        public override UpdateTime UpdateTimeConfig => UpdateTime.RealtimeSinceStartup;

        private void Update()
        {
            // Do nothing, we're overriding this mode
        }

        public void OnObjectFixedUpdated() => Debug.Log(GetMessage(TimeAPI.FixedDeltaTime));
    }
}