using FastUnityCreationKit.Unity.Interfaces.Callbacks;
using FastUnityCreationKit.Unity.Time;
using FastUnityCreationKit.Unity.Time.Enums;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Examples._03_FastMonoBehaviour_TimeModes.Scripts
{
    public sealed class FixedUpdateFastMonoBehaviourExample : TimeMonoBehaviourExample, IFixedUpdateCallback
    {
        [NotNull] protected override string GetMessage(float deltaTime) => $"Update called with deltaTime: {TimeAPI.FixedDeltaTime}";
        
        public override UpdateTime UpdateTimeConfig => UpdateTime.RealtimeSinceStartup;

        public void OnObjectFixedUpdated() => Debug.Log(GetMessage(TimeAPI.FixedDeltaTime));
    }
}