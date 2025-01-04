using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.UI.Utility;
using FastUnityCreationKit.Core.Logging;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;

namespace FastUnityCreationKit.UI.Context.Data.Text
{
    /// <summary>
    /// This class is used to represent a localized string context.
    /// In standard configuration it is used to display static localized text, however it also
    /// can be used to display text with variables, but in such case it needs to be inherited
    /// into variable-based context. You can use <see cref="LocalizedString"/> property
    /// to access the localized string and set custom variables. Changes will be automatically
    /// reflected in the context.
    /// </summary>
    public class LocalizedStringContextProvider : StringContextBaseProvider, IInitializable
    {
        bool IInitializable.InternalInitializationStatusStorage { get; set; }
        
        [Required] [SerializeField] [TitleGroup(PROVIDER_CONFIGURATION)]
        private LocalizedString localizedString;
        
        /// <summary>
        /// Access to localized string.
        /// </summary>
        public LocalizedString LocalizedString => localizedString;
        
        private string _cachedString;

        public void OnInitialize()
        {
            // Subscribe to string changed event
            localizedString.StringChanged += OnStringChanged;
        }

        private void OnStringChanged(string value)
        {
            // Refresh context if string is changed
            _cachedString = value;
            NotifyContextHasChanged();
        }

        private string LocalizedText
        {
            get
            {
                // If the localized string is set, return the cached string or localized string
                // if cached string is not set.
                if (localizedString != null)
                {
                    if(!string.IsNullOrEmpty(_cachedString)) return _cachedString;
                    _cachedString = localizedString.GetLocalizedString();
                    return _cachedString;
                }
                
                Guard<UserInterfaceLogConfig>.Error("Localized string is not set in the context provider.");
                return string.Empty;
            }
        }

        public override string Provide()
        {
            return LocalizedText;
        }

        

      
    }
}