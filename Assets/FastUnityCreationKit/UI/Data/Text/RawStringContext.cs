using FastUnityCreationKit.Unity.Events.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Data.Text
{
    public sealed class RawStringContext : StringContextBase<RawStringContext>, IUpdateCallback
    {
        [TabGroup("Configuration")] [ShowInInspector] [SerializeField]
        private string text;
        private string _renderedText;

        public override string LocalizedText => text;

        public void OnObjectUpdated(float deltaTime)
        {
            // Check if text has changed and update it
            if (_renderedText == text) return;
            _renderedText = text;
            
            // Notify that the object has been updated
            MakeDirty();
        }
    }
}