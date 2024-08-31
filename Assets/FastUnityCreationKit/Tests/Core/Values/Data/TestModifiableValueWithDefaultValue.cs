using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;
using FastUnityCreationKit.Core.Values;

namespace FastUnityCreationKit.Tests.Core.Values.Data
{
    public sealed class TestModifiableValueWithDefaultValue : ModifiableValue<float32>, IWithDefaultValue<float32>
    {
        public float32 DefaultValue => 25f;
    }
}