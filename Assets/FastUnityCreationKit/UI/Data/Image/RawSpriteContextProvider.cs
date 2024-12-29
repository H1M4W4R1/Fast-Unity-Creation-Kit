using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Data.Image
{
    public sealed class RawSpriteContextProvider : SpriteContextBaseProvider
    {
        [SerializeField] [TabGroup("Configuration")] [Required]
        private Sprite sprite;


        public override Sprite Provide() => sprite;
    }
}