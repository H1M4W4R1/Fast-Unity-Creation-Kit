using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;

namespace FastUnityCreationKit.Inventory.Stacking
{
    /// <summary>
    /// Represents a single item stack - a stack that can hold only one item.
    /// </summary>
    public sealed class SingleItemStack : ItemStackCounterValue
    {
        /// <summary>
        /// Maximum amount of items that can be stacked.
        /// </summary>
        public override int32 MaxLimit => 1;
    }
}