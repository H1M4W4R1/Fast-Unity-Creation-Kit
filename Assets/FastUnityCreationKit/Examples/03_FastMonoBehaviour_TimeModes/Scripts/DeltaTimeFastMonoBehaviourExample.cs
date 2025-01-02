namespace FastUnityCreationKit.Examples._03_FastMonoBehaviour_TimeModes.Scripts
{
    public sealed class DeltaTimeFastMonoBehaviourExample : TimeMonoBehaviourExample
    {
        protected override string GetMessage(float deltaTime) => $"Update called with delta time: {deltaTime}";
    }
}