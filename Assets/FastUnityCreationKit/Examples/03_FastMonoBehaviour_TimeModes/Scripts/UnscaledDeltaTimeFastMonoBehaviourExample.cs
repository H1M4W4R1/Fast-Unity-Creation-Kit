using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Time;

namespace FastUnityCreationKit.Examples._03_FastMonoBehaviour_TimeModes.Scripts
{
    public sealed class UnscaledDeltaTimeFastMonoBehaviourExample : TimeMonoBehaviourExample
    {
        protected override string GetMessage(float deltaTime) => $"Update called with unscaled delta time: {deltaTime}";
        
        public override UpdateTime UpdateTimeConfig => UpdateTime.UnscaledDeltaTime;
        
    }
}