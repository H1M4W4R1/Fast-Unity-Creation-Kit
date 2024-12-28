using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Data.Image
{
    public sealed class RawSpriteContext : SpriteContextBase<RawSpriteContext>
    {
        [SerializeField] [TabGroup("Configuration")] [Required]
        private Sprite sprite;
        
        public override Sprite Image => sprite; 
    }
}