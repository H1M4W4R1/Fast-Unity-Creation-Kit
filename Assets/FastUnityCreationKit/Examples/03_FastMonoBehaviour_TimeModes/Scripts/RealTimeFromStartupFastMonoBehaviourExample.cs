using FastUnityCreationKit.Unity.Time.Enums;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Examples._03_FastMonoBehaviour_TimeModes.Scripts
{
    public sealed class RealTimeFromStartupFastMonoBehaviourExample : TimeMonoBehaviourExample
    {
        public override UpdateTime UpdateTimeConfig => UpdateTime.RealtimeSinceStartup;

        [NotNull] protected override string GetMessage(float deltaTime)
        {
            return $"Update called with real time (from start): {deltaTime}";
        }
    }
}