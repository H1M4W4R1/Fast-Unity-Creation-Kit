using FastUnityCreationKit.UI.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.UI
{
    /// <summary>
    /// Represents a global data context.
    /// </summary>
    public abstract class GlobalDataContext<TSelf> : GlobalDataContext, IDataContext
        where TSelf : GlobalDataContext<TSelf>, new()
    {
        /// <summary>
        /// Instance of the global data context.
        /// </summary>
        [NotNull] public static TSelf Instance { get; } = new TSelf();
        
        /// <inheritdoc/>
        public virtual void OnBind(UIObject uiObject)
        {
            // Do nothing
        }
        
        /// <inheritdoc/>
        public override GlobalDataContext GetInstance() => Instance;
        
    }
    
    public abstract class GlobalDataContext
    {
        /// <summary>
        /// Gets the instance of the global data context.
        /// </summary>
        public abstract GlobalDataContext GetInstance();
    }
}