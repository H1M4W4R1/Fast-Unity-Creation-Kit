using System;
using FastUnityCreationKit.Annotations.Info;
using FastUnityCreationKit.Saving.Interfaces;
using FastUnityCreationKit.Saving.Utility;
using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.Unity.Interfaces.Interaction;
using FastUnityCreationKit.Unity.Time.Enums;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Core.Objects;
using FastUnityCreationKit.Unity.Events.Unity;
using FastUnityCreationKit.Unity.Interfaces.Callbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.Unity
{
    /// <summary>
    /// Base class for all MonoBehaviours compatible with the FastUnityCreationKit.
    /// Used to automatically handle interface processing.
    /// </summary>
    [SupportedFeature(typeof(ICreateCallback))] [SupportedFeature(typeof(IDestroyCallback))]
    [SupportedFeature(typeof(IEnabledCallback))] [SupportedFeature(typeof(IDisabledCallback))]
    [SupportedFeature(typeof(IFixedUpdateCallback))] [SupportedFeature(typeof(IPreUpdateCallback))]
    [SupportedFeature(typeof(IUpdateCallback))] [SupportedFeature(typeof(IPostUpdateCallback))]
    [SupportedFeature(typeof(IQuitCallback))] [SupportedFeature(typeof(ISaveableObject))]
    [SupportedFeature(typeof(ITemporaryObject))] [SupportedFeature(typeof(IPersistentObject))]
    [SupportedFeature(typeof(IClickable))] [SupportedFeature(typeof(IHoverable))]
    [SupportedFeature(typeof(IDoubleClickable))] [SupportedFeature(typeof(ISelectable))]
    [SupportedFeature(typeof(IInitializable))] 
    public abstract class CKMonoBehaviour : SerializedMonoBehaviour
    {
        protected const string GROUP_STATE = "State";
        protected const string GROUP_DEBUG = "Debug";
        protected const string GROUP_CONFIGURATION = "Configuration";

        /// <summary>
        /// Check if the object uses the update mode
        /// Used to render the update mode in the inspector.
        /// </summary>
        private bool UsesUpdateMode => this is IUpdateCallback ||
                                       this is IPreUpdateCallback ||
                                       this is IPostUpdateCallback;
        
        /// <summary>
        /// State of the object. If true, the object is disabled.
        /// </summary>
        // ReSharper disable once Unity.RedundantAttributeOnTarget
        [HideInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)]
        public bool IsDisabled { get; private set; }

        /// <summary>
        /// State of the object. If true, the object is enabled.
        /// Directly opposite of <see cref="IsDisabled"/>.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)]
        public bool IsEnabled => !IsDisabled;

        /// <summary>
        /// State of the object. If true, the object is destroyed.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)]
        public bool IsDestroyed { get; private set; }

        /// <summary>
        /// If true, the object will be updated even when disabled.
        /// </summary>
        [ShowInInspector] [TitleGroup(GROUP_CONFIGURATION)]
        [ShowIf(nameof(UsesUpdateMode))]
        public virtual UpdateMode UpdateMode => UpdateMode.MonoBehaviour;

        /// <summary>
        /// Mode of time used for updating the object.
        /// </summary>
        [ShowInInspector] [TitleGroup(GROUP_CONFIGURATION)]
        [ShowIf(nameof(UsesUpdateMode))]
        public virtual UpdateTime UpdateTimeConfig => UpdateTime.DeltaTime;

        /// <summary>
        /// Avoid overriding the Awake method. Implement the <see cref="IInitializable"/> interface instead
        /// and use the <see cref="IInitializable.OnInitialize"/> method.
        /// </summary>
        protected virtual void Awake()
        {
            // Register this object to the object registry.
            CKEventsManager.Instance.RegisterFastMonoBehaviour(this);

            // Check if supports persistent interface.
            if (this is IPersistentObject)
            {
                // Ensure that object is on the root level as Unity only allows root level objects to be made persistent.
                if (transform.parent)
                {
                    transform.SetParent(null);

                    // Log warning in editor's console to notify the user.
#if UNITY_EDITOR
                    Guard<ValidationLogConfig>.Warning(
                        $"Persistent object {name} is not on the root level. This may cause unexpected behavior." +
                        $"Fail-safe triggered: moving object to root layer.");
#endif
                }

                // Make the object persistent.
                DontDestroyOnLoad(gameObject);

                // Check if is temporary object.
                if (this is ITemporaryObject)
                {
                    // Log error in console if object is both persistent and temporary.
                    Guard<ValidationLogConfig>.Warning(
                        $"Object {name} is both persistent and temporary. This will cause severe issues!");
                }
            }

            // Initialize the object if it implements the IInitializable interface.
            if (this is IInitializable initializable)
                initializable.Initialize();

            // Register the object to the save system if it implements the ISaveableObject interface.
            if (this is ISaveableObject saveableObject)
                SaveAPI.RegisterSavableObject(saveableObject);

            NotifyObjectWasCreated();

            // If this is both clickable and selectable print warning.
            if (this is IClickable and ISelectable)
            {
                Guard<ValidationLogConfig>.Warning(
                    $"Object {name} is both clickable and selectable. This may cause unexpected behavior.");
            }
        }

        protected void OnEnable()
        {
            IsDisabled = false;
            NotifyObjectWasEnabled();
        }

        protected void OnDisable()
        {
            IsDisabled = true;
            NotifyObjectWasDisabled();
        }

        protected void OnDestroy()
        {
            NotifyObjectWasDestroyed();
            IsDestroyed = true;

            // Unregister the object from the save system if it implements the ISaveableObject interface.
            if (this is ISaveableObject saveableObject)
                SaveAPI.UnregisterSavableObject(saveableObject);

            // Unregister this object from the object registry.
            CKEventsManager.Instance.UnregisterFastMonoBehaviour(this);
        }

        internal static void HandlePreUpdate(CKMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectWasPreUpdated(deltaTime);
        }

        internal static void HandleUpdate(CKMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectWasUpdated(deltaTime);
        }

        internal static void HandlePostUpdate(CKMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectWasPostUpdated(deltaTime);
        }

        internal static void HandleFixedUpdate(CKMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectWasFixedUpdated();
        }

        internal static void HandleTemporaryObject(CKMonoBehaviour behaviour, float deltaTime)
        {
            // Check if object is temporary, if not skip
            if (behaviour is not ITemporaryObject temporaryObject) return;

            if (temporaryObject.ShouldBeDestroyed())
                Destroy(behaviour.gameObject);
        }

        protected virtual void NotifyObjectWasCreated()
        {
            if (this is ICreateCallback createCallback)
                createCallback.OnObjectCreated();
            
            // Trigger the event for the object.
            OnObjectCreatedEvent.Trigger(this);
        }

        protected virtual void NotifyObjectWasDestroyed()
        {
            if (this is IDestroyCallback destroyCallback)
                destroyCallback.OnObjectDestroyed();
            
            OnObjectDestroyedEvent.Trigger(this);
        }

        protected virtual void NotifyObjectWasEnabled()
        {
            if (this is IEnabledCallback enabledCallback)
                enabledCallback.OnObjectEnabled();
            
            OnObjectEnabledEvent.Trigger(this);
        }

        protected virtual void NotifyObjectWasDisabled()
        {
            if (this is IDisabledCallback disabledCallback)
                disabledCallback.OnObjectDisabled();
            
            OnObjectDisabledEvent.Trigger(this);
        }

        protected virtual void NotifyObjectWasFixedUpdated()
        {
            if (this is IFixedUpdateCallback fixedUpdateCallback)
                fixedUpdateCallback.OnObjectFixedUpdated();
        }

        protected virtual void NotifyObjectWasPreUpdated(float deltaTime)
        {
            if (this is IPreUpdateCallback preUpdateCallback)
                preUpdateCallback.OnBeforeObjectUpdated(deltaTime);
        }

        protected virtual void NotifyObjectWasUpdated(float deltaTime)
        {
            if (this is IUpdateCallback updateCallback)
                updateCallback.OnObjectUpdated(deltaTime);
        }

        protected virtual void NotifyObjectWasPostUpdated(float deltaTime)
        {
            if (this is IPostUpdateCallback postUpdateCallback)
                postUpdateCallback.OnAfterObjectUpdated(deltaTime);
        }

        private void OnApplicationQuit()
        {
            if (this is IQuitCallback quitCallback)
                quitCallback.OnQuit();
        }
    }
}