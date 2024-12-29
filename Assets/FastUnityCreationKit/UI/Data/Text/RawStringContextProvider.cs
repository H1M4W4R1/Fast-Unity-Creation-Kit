using FastUnityCreationKit.Unity.Events.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Data.Text
{
    public sealed class RawStringContextProvider : StringContextBaseProvider
    {
        [TabGroup("Configuration")] [ShowInInspector] [SerializeField]
        private string text;
        private string _renderedText;
        
        public void FixedUpdate()
        {
            // Check if text has changed and update it
            if (_renderedText == text) return;
            _renderedText = text;
            
            // Notify that the object has been updated
            IsDirty = true;
        }

        public override string Provide()
        {
            return text;
        }
    }
}