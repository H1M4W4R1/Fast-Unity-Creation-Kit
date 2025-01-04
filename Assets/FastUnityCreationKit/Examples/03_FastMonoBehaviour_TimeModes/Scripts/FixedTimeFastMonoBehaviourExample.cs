using FastUnityCreationKit.Unity.Interfaces.Callbacks;
using FastUnityCreationKit.Unity.Time;
using FastUnityCreationKit.Unity.Time.Enums;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Examples._03_FastMonoBehaviour_TimeModes.Scripts
{
    public sealed class FixedUpdateFastMonoBehaviourExample : TimeMonoBehaviourExample, IFixedUpdateCallback
    {
        public override UpdateTime UpdateTimeConfig => UpdateTime.RealtimeSinceStartup;

        public void OnObjectFixedUpdated()
        {
            Debug.Log(GetMessage(TimeAPI.FixedDeltaTime));
        }

        [NotNull] protected override string GetMessage(float deltaTime)
        {
            return $"Update called with deltaTime: {TimeAPI.FixedDeltaTime}";
        }
    }
}