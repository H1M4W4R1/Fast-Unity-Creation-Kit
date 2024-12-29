using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FastUnityCreationKit.UI.Data.Image
{
    public sealed class AddressableSpriteContextProvider : SpriteContextBaseProvider
    {
        [SerializeField] [TabGroup("Configuration")] [Required]
        private AssetReferenceSprite spriteReference;

        private bool _isLoading;
        private Sprite _spriteCache;
        
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
                spriteReference.LoadAssetAsync<Sprite>().Completed += handle =>
                {
                    // Cache the result and make the object dirty
                    _spriteCache = handle.Result;
                    IsDirty = true;
                    
                    // Reset the loading flag
                    _isLoading = false;
                };
                
                // Return null until the asset is loaded
                return null;
            }
        }

        public override Sprite Provide() => Image;
    }
}