using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;
using FastUnityCreationKit.Core.Values;

namespace FastUnityCreationKit.Tests.Core.Values.Data
{
    public class TestModifiableValueWithLimits : ModifiableValue<float32>, IWithMinLimit<float32>, IWithMaxLimit<float32>
    {
        public float32 MinLimit => -10f;
        public float32 MaxLimit => 10f;
    }
}