using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Interfaces;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace FastUnityCreationKit.UI.Elements.Base
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class UITextBase : UIObjectWithContextBase<string>, IRenderable<string>
    {
        /// <summary>
        ///     Internal reference to the TextMeshProUGUI component.
        /// </summary>
        // ReSharper disable once NullableWarningSuppressionIsUsed
        [NotNull] private TextMeshProUGUI _textMeshProUGUI = null!;

        /// <summary>
        ///     Exposes the TextMeshProUGUI component for child classes to use.
        /// </summary>
        [NotNull] protected TextMeshProUGUI Text => _textMeshProUGUI;

        public virtual void Render(string dataContext)
        {
            _textMeshProUGUI.text = dataContext;
        }

        public override void Setup()
        {
            base.Setup();
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }
    }
}