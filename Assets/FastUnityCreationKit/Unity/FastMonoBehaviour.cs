using System;
using FastUnityCreationKit.Annotations.Info;
using FastUnityCreationKit.Saving.Interfaces;
using FastUnityCreationKit.Saving.Utility;
using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Basic;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics;
using FastUnityCreationKit.Unity.Interfaces.Configuration;
using FastUnityCreationKit.Unity.Interfaces.Interaction;
using FastUnityCreationKit.Unity.Time.Enums;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Core.Objects;
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
    [SupportedFeature(typeof(ITemporaryObject))] [SupportedFeature(typeof(IPersistent))]
    [SupportedFeature(typeof(IClickable))] [SupportedFeature(typeof(IHoverable))]
    [SupportedFeature(typeof(IDoubleClickable))] [SupportedFeature(typeof(ISelectable))]
    [SupportedFeature(typeof(IInitializable))] [SupportedFeature(typeof(IOnTriggerEnterCallback))]
    [SupportedFeature(typeof(IOnTriggerExitCallback))] [SupportedFeature(typeof(IOnTriggerStayCallback))]
    [SupportedFeature(typeof(IOnCollisionEnterCallback))] [SupportedFeature(typeof(IOnCollisionExitCallback))]
    [SupportedFeature(typeof(IOnCollisionStayCallback))]
    public abstract class FastMonoBehaviour : MonoBehaviour
    {
        protected const string GROUP_STATE = "State";
        protected const string GROUP_DEBUG = "Debug";
        protected const string GROUP_CONFIGURATION = "Configuration";
        
        /// <summary>
        /// State of the object. If true, the object is disabled.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)]
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
        public virtual UpdateMode UpdateMode => UpdateMode.MonoBehaviour;

        /// <summary>
        /// Mode of time used for updating the object.
        /// </summary>
        [ShowInInspector] [TitleGroup(GROUP_CONFIGURATION)]
        public virtual UpdateTime UpdateTimeConfig => UpdateTime.DeltaTime;

        /// <summary>
        /// Avoid overriding the Awake method. Implement the <see cref="IInitializable"/> interface instead
        /// and use the <see cref="IInitializable.OnInitialize"/> method.
        /// </summary>
        protected virtual void Awake()
        {
            // Register this object to the object registry.
            FastMonoBehaviourManager.Instance.RegisterFastMonoBehaviour(this);

            // Check if supports persistent interface.
            if (this is IPersistent)
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
            FastMonoBehaviourManager.Instance.UnregisterFastMonoBehaviour(this);
        }

        internal static void HandlePreUpdate(FastMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectWasPreUpdated(deltaTime);
        }

        internal static void HandleUpdate(FastMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectWasUpdated(deltaTime);
        }

        internal static void HandlePostUpdate(FastMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectWasPostUpdated(deltaTime);
        }

        internal static void HandleFixedUpdate(FastMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectWasFixedUpdated();
        }

        internal static void HandleTemporaryObject(FastMonoBehaviour behaviour, float deltaTime)
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
        }

        protected virtual void NotifyObjectWasDestroyed()
        {
            if (this is IDestroyCallback destroyCallback)
                destroyCallback.OnObjectDestroyed();
        }

        protected virtual void NotifyObjectWasEnabled()
        {
            if (this is IEnabledCallback enabledCallback)
                enabledCallback.OnObjectEnabled();
        }

        protected virtual void NotifyObjectWasDisabled()
        {
            if (this is IDisabledCallback disabledCallback)
                disabledCallback.OnObjectDisabled();
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

        protected void OnTriggerEnter(Collider other)
        {
            if (this is IOnTriggerEnterCallback triggerEnterCallback)
                triggerEnterCallback._OnTriggerEnter(other);
        }
        
        protected void OnTriggerExit(Collider other)
        {
            if (this is IOnTriggerExitCallback triggerExitCallback)
                triggerExitCallback._OnTriggerExit(other);
        }
        
        protected void OnTriggerStay(Collider other)
        {
            if (this is IOnTriggerStayCallback triggerStayCallback)
                triggerStayCallback._OnTriggerStay(other);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (this is IOnCollisionEnterCallback collisionEnterCallback)
                collisionEnterCallback._OnCollisionEnter(other);
        }
        
        private void OnCollisionExit(Collision other)
        {
            if (this is IOnCollisionExitCallback collisionExitCallback)
                collisionExitCallback._OnCollisionExit(other);
        }
        
        private void OnCollisionStay(Collision other)
        {
            if (this is IOnCollisionStayCallback collisionStayCallback)
                collisionStayCallback._OnCollisionStay(other);
        }

        private void OnApplicationQuit()
        {
            if (this is IQuitCallback quitCallback)
                quitCallback.OnQuit();
        }
    }
}