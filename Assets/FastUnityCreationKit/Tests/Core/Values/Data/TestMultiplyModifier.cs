using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Values.Modifiers;

namespace FastUnityCreationKit.Tests.Core.Values.Data
{
    public sealed class TestMultiplyModifier : MultiplyModifier<float32>
    {
        public TestMultiplyModifier(float32 amount) : base(amount)
        {
        }
    }
}