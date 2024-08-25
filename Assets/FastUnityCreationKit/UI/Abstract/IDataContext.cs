using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// Represents a data context used to render UI elements.
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// Executed when data context is bound to the UI object.
        /// </summary>
        public void OnBind([NotNull] IUIObjectWithDataContext uiObject);
    }
}