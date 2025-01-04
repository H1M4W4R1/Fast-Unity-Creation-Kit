using FastUnityCreationKit.Unity.Time.Enums;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Examples._03_FastMonoBehaviour_TimeModes.Scripts
{
    public sealed class UnscaledDeltaTimeFastMonoBehaviourExample : TimeMonoBehaviourExample
    {
        public override UpdateTime UpdateTimeConfig => UpdateTime.UnscaledDeltaTime;

        [NotNull] protected override string GetMessage(float deltaTime)
        {
            return $"Update called with unscaled delta time: {deltaTime}";
        }
    }
}