using FastUnityCreationKit.Annotations.Info;
using FastUnityCreationKit.UI.Interfaces;
using FastUnityCreationKit.UI.Utility;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Basic;
using FastUnityCreationKit.Utility.Logging;
using UnityEngine;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// The base class for all UI objects in Fast Unity Creation Kit.
    /// </summary>
    public abstract class UIObjectBase : UIBehaviour, ICreateCallback, IDestroyCallback
    {
        /// <summary>
        /// Rect transform of this object.
        /// </summary>
        public RectTransform RectTransform { get; private set; }

        public virtual void OnObjectCreated()
        {
            // Get rect transform
            RectTransform = GetComponent<RectTransform>();
         
            // Register this object
            UIManager.Instance.RegisterUserInterfaceObject(this);
            
            // Setup object
            Setup();
            Guard<UserInterfaceLogConfig>.Verbose($"UI object {name} has been set-up correctly.");

            // Check if this object is IRenderable, if so, try to render
            if (this is IRenderable renderable)
            {
                renderable.Render(true);
                Guard<UserInterfaceLogConfig>.Verbose($"UI object {name} has been rendered correctly for the first time.");
            }

            // Call after first render
            AfterFirstRenderOrCreated();
        }

        public virtual void Setup()
        {
        }
        
        public virtual void AfterFirstRenderOrCreated()
        {
        }

        public virtual void Teardown()
        {
        }


        public virtual void OnObjectDestroyed()
        {
            // Teardown object
            Teardown();
            
            // Unregister this object
            UIManager.Instance.UnregisterUserInterfaceObject(this);
        }
    }
}