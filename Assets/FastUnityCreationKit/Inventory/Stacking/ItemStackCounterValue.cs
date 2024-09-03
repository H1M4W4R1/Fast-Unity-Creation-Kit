using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;
using FastUnityCreationKit.Core.Values;

namespace FastUnityCreationKit.Inventory.Stacking
{
    /// <summary>
    /// Represents a type of item stack - determines how many items can be stacked in a single slot.
    /// </summary>
    public abstract class ItemStackCounterValue : ModifiableValue<int32>, IWithMinLimit<int32>, IWithMaxLimit<int32>
    {
        /// <summary>
        /// Minimum amount of items that can be stacked.
        /// </summary>
        public virtual int32 MinLimit => 0;

        /// <summary>
        /// Maximum amount of items that can be stacked.
        /// </summary>
        public abstract int32 MaxLimit { get; }
    }
}