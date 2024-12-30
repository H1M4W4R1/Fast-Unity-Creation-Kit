using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Context.Data.Image
{
    /// <summary>
    /// Providers raw sprite context. It always returns image that is set in the inspector.
    /// </summary>
    public sealed class RawSpriteContextProvider : SpriteContextBaseProvider
    {
        [SerializeField] [TabGroup("Configuration")] [Required]
        [Tooltip("Sprite that will be returned by this context.")]
        private Sprite sprite;
        
        public override Sprite Provide() => sprite;
    }
}