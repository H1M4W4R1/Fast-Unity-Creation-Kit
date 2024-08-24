using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Values.Modifiers;

namespace FastUnityCreationKit.Core.Values.Tests.Data
{
    public sealed class TestMultiplyModifier : MultiplyModifier<float32>
    {
        public TestMultiplyModifier(float32 amount) : base(amount)
        {
        }
    }
}