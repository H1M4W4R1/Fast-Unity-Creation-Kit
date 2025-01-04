using JetBrains.Annotations;

namespace FastUnityCreationKit.Examples._03_FastMonoBehaviour_TimeModes.Scripts
{
    public sealed class DeltaTimeFastMonoBehaviourExample : TimeMonoBehaviourExample
    {
        [NotNull] protected override string GetMessage(float deltaTime)
        {
            return $"Update called with delta time: {deltaTime}";
        }
    }
}