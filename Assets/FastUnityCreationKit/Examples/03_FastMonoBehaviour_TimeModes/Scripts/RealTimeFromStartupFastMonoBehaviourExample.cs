using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Time;

namespace FastUnityCreationKit.Examples._03_FastMonoBehaviour_TimeModes.Scripts
{
    public sealed class RealTimeFromStartupFastMonoBehaviourExample : TimeMonoBehaviourExample
    {
        protected override string GetMessage(float deltaTime) => $"Update called with real time (from start): {deltaTime}";
        
        public override UpdateTime UpdateTimeConfig => UpdateTime.RealtimeSinceStartup;
        
    }
}