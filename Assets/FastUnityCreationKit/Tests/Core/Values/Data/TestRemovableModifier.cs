using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Utility;
using FastUnityCreationKit.Core.Utility.Properties;
using FastUnityCreationKit.Core.Values.Modifiers;

namespace FastUnityCreationKit.Tests.Core.Values.Data
{
    public class TestRemovableModifier : FlatAddModifier<float32>, IConditionallyRemovable
    {
        public bool toRemove;
        
        public TestRemovableModifier(float32 amount) : base(amount)
        {
        }

        public bool IsRemovalConditionMet()
        {
            return toRemove;
        }
    }
}