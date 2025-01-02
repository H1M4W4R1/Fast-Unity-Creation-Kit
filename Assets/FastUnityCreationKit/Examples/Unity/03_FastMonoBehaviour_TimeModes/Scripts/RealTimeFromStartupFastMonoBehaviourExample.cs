using FastUnityCreationKit.Unity;

namespace FastUnityCreationKit.Examples.Unity._03_FastMonoBehaviour_TimeModes.Scripts
{
    public sealed class RealTimeFromStartupFastMonoBehaviourExample : TimeMonoBehaviourExample
    {
        protected override string GetMessage(float deltaTime) => $"Update called with real time (from start): {deltaTime}";
        
        public override UpdateTime UpdateTimeConfig => UpdateTime.RealtimeSinceStartup;
        
    }
}