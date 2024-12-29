using FastUnityCreationKit.Structure.Initialization;
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
    public class LocalizedStringContextProvider : StringContextBaseProvider, IInitializable
    {
        bool IInitializable.InternalInitializationStatusStorage { get; set; }
        
        [SerializeField] [TabGroup("Configuration")] [Required]
        private LocalizedString localizedString;
        
        private string _cachedString;

        void IInitializable.OnInitialize()
        {
            // Subscribe to string changed event
            localizedString.StringChanged += OnStringChanged;
        }

        private void OnStringChanged(string value)
        {
            // Refresh context if string is changed
            _cachedString = value;
            IsDirty = true;
        }

        private string LocalizedText
        {
            get
            {
                // If the localized string is set, return the localized string
                if (localizedString != null) return _cachedString ?? localizedString.GetLocalizedString();
                
                Debug.LogError("Localized string is not set in the context.", this);
                return string.Empty;

            }
        }

        public override string Provide()
        {
            return LocalizedText;
        }

        

      
    }
}