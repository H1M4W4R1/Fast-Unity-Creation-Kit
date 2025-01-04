using FastUnityCreationKit.UI.Interfaces;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Interfaces.Callbacks;
using FastUnityCreationKit.Unity.Time.Enums;
using UnityEngine;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// The base class for all UI objects in Fast Unity Creation Kit.
    /// </summary>
    public abstract class UIObjectBase : CKMonoBehaviour, ICreateCallback, IDestroyCallback
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
        
#region UPDATE_CONFIGURATION

        // UI objects are always updated (even when disabled or when time is paused) and
        // they are updated using unscaled delta time - to prevent UI from being dependent on time scale.
        public override UpdateTime UpdateTimeConfig => UpdateTime.UnscaledDeltaTime;
        public override UpdateMode UpdateMode => UpdateMode.UpdateWhenDisabled | UpdateMode.UpdateWhenTimePaused;

#endregion

    }
}