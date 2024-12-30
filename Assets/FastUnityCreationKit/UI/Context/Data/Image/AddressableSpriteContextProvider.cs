using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FastUnityCreationKit.UI.Context.Data.Image
{
    /// <summary>
    /// Represents a context that is populated with sprite data. This context provider uses Addressables to load the sprite.
    /// When sprite is loading, it returns null and waits until the sprite is loaded.
    /// </summary>
    public sealed class AddressableSpriteContextProvider : SpriteContextBaseProvider
    {
        [SerializeField] [TabGroup("Configuration")] [Required]
        [Tooltip("Reference to the sprite that will be returned by this context.")]
        private AssetReferenceSprite spriteReference;

        /// <summary>
        /// If true, the sprite is currently loading. Will return null until the sprite is loaded.
        /// </summary>
        private bool _isLoading;
        
        /// <summary>
        /// Cached sprite asset.
        /// </summary>
        private Sprite _spriteCache;

        /// <summary>
        /// Handle to the async operation that loads the sprite.
        /// </summary>
        private AsyncOperationHandle<Sprite> _handle;

        /// <summary>
        /// Current sprite asset. Also handles loading of the asset.
        /// </summary>
        public Sprite Image
        {
            get
            {
                // If the reference is loading, return null
                if (_isLoading) return null;
                
                if (_spriteCache != null) return _spriteCache;
                
                // If the reference is null, return null
                if(spriteReference == null) return null;
                
                // If the reference is valid, return the asset
                if (spriteReference.IsValid())
                {
                    _spriteCache = spriteReference.Asset as Sprite;
                    return _spriteCache;
                }

                // Set the loading flag
                _isLoading = true;
                
                // If the reference is not valid, load the asset asynchronously 
                _handle = spriteReference.LoadAssetAsync<Sprite>();
                _handle.Completed += handle =>
                {
                    // Cache the result and make the object dirty
                    _spriteCache = handle.Result;
                    NotifyContextHasChanged();
                    
                    // Reset the loading flag
                    _isLoading = false;
                };
                
                // Return null until the asset is loaded
                return null;
            }
        }

        /// <summary>
        /// Provides the sprite asset.
        /// </summary>
        /// <returns>Sprite asset.</returns>
        public override Sprite Provide() => Image;

        private void OnDestroy()
        {
            // Release the handle when the object is destroyed
            if (_handle.IsValid())
                Addressables.Release(_handle);
        }
    }
}