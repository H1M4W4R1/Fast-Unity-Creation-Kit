using System.Collections.Generic;
using FastUnityCreationKit.Crafting.Workstations;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Crafting.Data
{
    /// <summary>
    /// Represents the context of a crafting operation. This interface is used to define
    /// data required for crafting operations - such as the workstation used to craft the item.
    /// </summary>
    public interface ICraftingContext<out TSelf> : ICraftingContext
        where TSelf : struct, ICraftingContext<TSelf>
    {

    }
    
    /// <summary>
    /// Represents the context of a crafting operation.
    /// Do not use this struct directly, use <see cref="ICraftingContext{TSelf}"/> instead.
    /// </summary>
    public interface ICraftingContext
    {
        /// <summary>
        /// Gets the workstation used to craft the item.
        /// This property is required for any given crafting operation.
        /// </summary>
        [NotNull] public List<ICraftingWorkstation> WorkstationsNearby { get; }
    }
}