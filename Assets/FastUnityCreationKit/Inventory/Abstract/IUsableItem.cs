using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Inventory.Context;
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
        public bool IsItemUsableInContext([NotNull] IUseItemContext interactionContext);

        /// <summary>
        /// Uses the item.
        /// </summary>
        public async UniTask<bool> UseItemAsync([NotNull] IUseItemContext interactionContext)
        {
            // If the item can't be used, return false.
            if (!IsItemUsableInContext(interactionContext)) return false;
            
            await OnUsedAsync(interactionContext);
            return true;
        }
        
        /// <summary>
        /// Called when the item is used.
        /// </summary>
        protected UniTask OnUsedAsync([NotNull] IUseItemContext interactionContext);
    }
}