using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Context;
using UnityEngine;

namespace FastUnityCreationKit.UI.Interfaces
{
    /// <summary>
    /// Represents a UI object that can be rendered.
    /// </summary>
    /// <typeparam name="TDataContextSealed">Context used to render this object.</typeparam>
    /// <typeparam name="TSelfUIObject">UI object type.</typeparam>
    public interface IRenderable<TSelfUIObject, TDataContextSealed> : IRenderable
        where TSelfUIObject : UIObject<TSelfUIObject>, new()
    {
        /// <summary>
        /// Gets the data context for this renderer.
        /// </summary>
        public DataContextInfo<TDataContextSealed> DataContext
        {
            get
            {
                // Check if this object is UIObject, if so, get data context
                if (this is UIObject<TSelfUIObject> uiObject) return uiObject.GetDataContext<TDataContextSealed>();

                // Log error if this object is not UIObject
                Debug.LogError($"DataContext is not supported on {GetType().Name}.");
                return default;
            }
        }

        void IRenderable.TryRender(bool forceRender)
        {
            // Check if this object is UIObject
            if (this is not UIObject<TSelfUIObject> uiObject)
            {
                Debug.LogError($"Render is not supported on {GetType().Name}.");
                return;
            }

            // Render this object
            if (DataContext.IsValid)
            {
                if (forceRender || DataContext.IsDirty)
                {
                    Render(DataContext.Context);
                    DataContext.Consume();
                }
            }
            else
            {
                Debug.LogError($"DataContext is not valid on {GetType().Name}.", uiObject);
            }
        }
    }

    public interface IRenderable
    {
        internal void TryRender(bool forceRender = false);

        /// <summary>
        /// Render this object
        /// </summary>
        public void Render<TDataContextSealed>(TDataContextSealed usingDataContext);
    }
}