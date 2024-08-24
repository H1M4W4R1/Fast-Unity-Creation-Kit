using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Values.Modifiers;

namespace FastUnityCreationKit.Core.Values.Tests.Data
{
    public sealed class TestAddModifier : AddModifier<float32>
    {
        public TestAddModifier(float32 amount) : base(amount)
        {
        }
    }
}