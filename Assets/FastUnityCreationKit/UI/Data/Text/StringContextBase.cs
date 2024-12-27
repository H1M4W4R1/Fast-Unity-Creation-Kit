using FastUnityCreationKit.UI.Context;
using FastUnityCreationKit.UI.Data.Interfaces;
using Sirenix.OdinInspector;

namespace FastUnityCreationKit.UI.Data.Text
{
    /// <summary>
    /// Represents a context that is populated with string data.
    /// </summary>
    public abstract class StringContextBase<TSelfSealed> : DataContext<TSelfSealed>, IStringContext
        where TSelfSealed : StringContextBase<TSelfSealed>, new()
    {
        /// <summary>
        /// Represents the localized text for the context.
        /// All text provided by this context should be always fully localized.
        /// </summary>
        [TabGroup("Debug")] [ReadOnly] [ShowInInspector] public abstract string LocalizedText { get; }
    }
}