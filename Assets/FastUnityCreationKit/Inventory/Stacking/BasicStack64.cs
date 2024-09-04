using FastUnityCreationKit.Core.Numerics;

namespace FastUnityCreationKit.Inventory.Stacking
{
    /// <summary>
    /// Represents a stack of items with a limit of 64.
    /// Implemented in e.g. Minecraft.
    /// </summary>
    public sealed class BasicStack64 : ItemStackCounterValue
    {
        public override int32 MaxLimit => 64;
    }
}