using FastUnityCreationKit.Core.Logging;
using JetBrains.Annotations;
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
    public class LocalizedStringContextProvider : StringContextBaseProvider
    {
        [Required] [SerializeField] [TitleGroup(PROVIDER_CONFIGURATION)] [CanBeNull] private LocalizedString localizedString;
        
        /// <summary>
        /// Access to localized string.
        /// </summary>
        [CanBeNull]
        public LocalizedString LocalizedString => localizedString;
        
        private string _cachedString;

        protected override void Setup()
        {
            base.Setup();
            
            // Subscribe to string changed event
            if(localizedString != null)
                localizedString.StringChanged += OnStringChanged;
        }

        protected override void TearDown()
        {
            base.TearDown();
            
            // Unsubscribe from string changed event
            if(localizedString != null)
                localizedString.StringChanged -= OnStringChanged;
        }

        private void OnStringChanged(string value)
        {
            // Refresh context if string is changed
            _cachedString = value;
            NotifyContextHasChanged();
        }

        [NotNull] private string LocalizedText
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

        [NotNull]
        public override string Provide()
        {
            return LocalizedText;
        }

        

      
    }
}