using FastUnityCreationKit.Unity.Events.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;

namespace FastUnityCreationKit.UI.Data.Text
{
    /// <summary>
    /// This class is used to represent a localized string context.
    /// In standard configuration it is used to display static localized text, however it also
    /// can be used to display text with variables, but in such case it needs to be inherited
    /// into variable-based context that supports <see cref="IUpdateCallback"/>
    /// </summary>
    public class LocalizedStringContext : StringContextBase<LocalizedStringContext>
    {
        [SerializeField] [TabGroup("Configuration")] [Required]
        private LocalizedString localizedString;
        
        private string _cachedString;

        public override void Setup()
        {
            base.Setup();
            
            // Subscribe to string changed event
            localizedString.StringChanged += OnStringChanged;
        }

        private void OnStringChanged(string value)
        {
            // Refresh context if string is changed
            _cachedString = value;
            MakeDirty();
        }

        public override string LocalizedText
        {
            get
            {
                if (localizedString == null)
                {
                    Debug.LogError("Localized string is not set in the context.", this);
                    return string.Empty;
                }
               
                return _cachedString ?? localizedString.GetLocalizedString();
            }
        }
    }
}