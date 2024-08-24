using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Values.Modifiers;

namespace FastUnityCreationKit.Core.Values.Tests.Data
{
    public sealed class TestFlatAddModifier : FlatAddModifier<float32>
    {
        public TestFlatAddModifier(float32 amount) : base(amount)
        {
        }
    }
}