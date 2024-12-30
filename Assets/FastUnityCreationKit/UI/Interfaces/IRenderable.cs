using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Context;
using FastUnityCreationKit.Utility.Logging;
using UnityEngine;

namespace FastUnityCreationKit.UI.Interfaces
{
    /// <summary>
    /// Represents a UI object that can be rendered.
    /// </summary>
    /// <typeparam name="TDataContextSealed">Context used to render this object.</typeparam>
    public interface IRenderable<TDataContextSealed> : IRenderable
    {
        /// <summary>
        /// Gets the data context for this renderer.
        /// </summary>
        public DataContextInfo<TDataContextSealed> DataContext
        {
            get
            {
                // Check if this object is UIObject, if so, get data context
                if (this is UIObject uiObject) return uiObject.GetDataContext<TDataContextSealed>();

                // Log error if this object is not UIObject
                Guard<UserInterfaceLogConfig>.Error($"IRenderable is not supported on {GetType().Name}.");
                return default;
            }
        }

        void IRenderable.TryRender(bool forceRender)
        {
            // Check if this object is UIObject
            if (this is not UIObject uiObject)
            {
                Guard<UserInterfaceLogConfig>.Error($"IRenderable is not supported on {GetType().Name}.");
                return;
            }

            // Render this object
            if (DataContext.IsValid)
            {
                if (forceRender || DataContext.IsDirty)
                {
                    Render(DataContext.Context);
                    DataContext.Consume();
                    
                    Guard<UserInterfaceLogConfig>.Verbose($"Rendered {GetType().Name} [was enforced: {forceRender}].");
                }
            }
            else
            {
                Guard<UserInterfaceLogConfig>.Error($"Data context is not valid for {GetType().Name}.");
            }
        }
        
        /// <summary>
        /// Render this object
        /// </summary>
        public void Render(TDataContextSealed dataContext);
    }

    /// <summary>
    /// Internal interface for rendering objects. Do not implement this interface directly.
    /// See <see cref="IRenderable{TDataContextSealed}"/> instead.
    /// </summary>
    public interface IRenderable
    {
        internal void TryRender(bool forceRender = false);
    }
}