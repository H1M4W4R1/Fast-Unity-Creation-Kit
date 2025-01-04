using FastUnityCreationKit.Unity.Time.Enums;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Examples._03_FastMonoBehaviour_TimeModes.Scripts
{
    public sealed class RealTimeFromStartupFastMonoBehaviourExample : TimeMonoBehaviourExample
    {
        [NotNull] protected override string GetMessage(float deltaTime) => $"Update called with real time (from start): {deltaTime}";
        
        public override UpdateTime UpdateTimeConfig => UpdateTime.RealtimeSinceStartup;
        
    }
}