using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Inventory.Data;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Inventory.Abstract
{
    /// <summary>
    /// Represents an item that can be equipped.
    /// </summary>
    public interface IEquippableItem 
    {
        /// <summary>
        /// Checks if the item is equipped.
        /// </summary>
        public bool IsEquipped { get; protected set; }
        
        /// <summary>
        /// Checks if the item can be equipped.
        /// </summary>
        public bool IsItemEquippableInContext([NotNull] IItemInteractionContext interactionContext);
        
        /// <summary>
        /// Checks if the item can be removed.
        /// </summary>
        public bool IsItemUnequippableInContext([NotNull] IItemInteractionContext interactionContext);

        /// <summary>
        /// Equips the item.
        /// </summary>
        public async UniTask<bool> EquipItemAsync([NotNull] IItemInteractionContext interactionContext)
        {
            // If the item can't be equipped or it's already equipped, return false.
            if (!IsItemEquippableInContext(interactionContext) || IsEquipped) return false;
            
            IsEquipped = true;
            await OnEquippedAsync(interactionContext);
            return true;

        }
        
        /// <summary>
        /// Removes the item.
        /// </summary>
        public async UniTask<bool> UnequipItemAsync([NotNull] IItemInteractionContext interactionContext)
        {
            // If the item can't be unequipped or it's not equipped, return false.
            if (!IsItemUnequippableInContext(interactionContext) || !IsEquipped)
                return false;
            
            IsEquipped = false;
            await OnUnequippedAsync(interactionContext);
            return true;
        }
        
        /// <summary>
        /// Called when the item is equipped.
        /// </summary>
        public UniTask OnEquippedAsync([NotNull] IItemInteractionContext interactionContext);
        
        /// <summary>
        /// Called when the item is removed from worn slot.
        /// </summary>
        public UniTask OnUnequippedAsync([NotNull] IItemInteractionContext interactionContext);
    }
}