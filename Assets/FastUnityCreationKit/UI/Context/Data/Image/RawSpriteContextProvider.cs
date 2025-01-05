using FastUnityCreationKit.UI.Context.Data.Text;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Global;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Context.Data.Image
{
    /// <summary>
    ///     Providers raw sprite context. It always returns image that is set in the inspector.
    /// </summary>
    public sealed class RawSpriteContextProvider : SpriteContextBaseProvider
    {
        [SerializeField]
        [Required]
        [TitleGroup(PROVIDER_CONFIGURATION)]
        [Tooltip("Sprite that will be returned by this context.")]
        [CanBeNull]
        private Sprite sprite;

        public override Sprite Provide()
        {
            return sprite;
        }
    }
}