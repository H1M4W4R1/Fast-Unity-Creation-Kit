using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Inventory.Data;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Inventory.Abstract
{
    /// <summary>
    /// Represents an item that can be used.
    /// </summary>
    public interface IUsableItem
    {
        /// <summary>
        /// Checks if the item can be used.
        /// </summary>
        public bool IsItemUsableInContext([NotNull] IItemInteractionContext interactionContext);

        /// <summary>
        /// Uses the item.
        /// </summary>
        public async UniTask<bool> UseItem([NotNull] IItemInteractionContext interactionContext)
        {
            // If the item can't be used, return false.
            if (!IsItemUsableInContext(interactionContext)) return false;
            
            await OnUsedAsync(interactionContext);
            return true;
        }
        
        /// <summary>
        /// Called when the item is used.
        /// </summary>
        protected UniTask OnUsedAsync([NotNull] IItemInteractionContext interactionContext);
    }
}