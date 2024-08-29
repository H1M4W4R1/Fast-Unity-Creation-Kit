using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Values.Modifiers;

namespace FastUnityCreationKit.Tests.Core.Values.Data
{
    public sealed class TestFlatAddModifier : FlatAddModifier<float32>
    {
        public TestFlatAddModifier(float32 amount) : base(amount)
        {
        }
    }
}