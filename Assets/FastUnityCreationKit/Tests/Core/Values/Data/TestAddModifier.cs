using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Values.Modifiers;

namespace FastUnityCreationKit.Tests.Core.Values.Data
{
    public sealed class TestAddModifier : AddModifier<float32>
    {
        public TestAddModifier(float32 amount) : base(amount)
        {
        }
    }
}