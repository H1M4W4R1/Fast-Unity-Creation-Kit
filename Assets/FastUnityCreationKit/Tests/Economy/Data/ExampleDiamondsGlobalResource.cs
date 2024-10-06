using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;
using FastUnityCreationKit.Economy;

namespace FastUnityCreationKit.Tests.Economy.Data
{
    public class ExampleDiamondsGlobalResource : GlobalResource<ExampleDiamondsGlobalResource>,
        IWithDefaultValue<int32>, IWithMaxLimit<int32>, IWithMinLimit<int32>
    {
        public int32 DefaultValue => 10;
        public int32 MaxLimit => 100;
        public int32 MinLimit => 0;
    }
}