using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.Unity.Callbacks;
using FastUnityCreationKit.Unity.Interfaces;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Logging;
using UnityEngine;

namespace FastUnityCreationKit.Unity
{
    /// <summary>
    /// Base class for all MonoBehaviours compatible with the FastUnityCreationKit.
    /// Used to automatically handle interface processing.
    /// </summary>
    public abstract class FastMonoBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Avoid overriding the Awake method. Implement the <see cref="IInitializable"/> interface instead
        /// and use the <see cref="IInitializable.OnInitialize"/> method.
        /// </summary>
        protected virtual void Awake()
        {
            // Register this object to the object registry.
            FastMonoBehaviourManager.Instance.RegisterFastMonoBehaviour(this);

            // Check if supports persistent interface.
            if (this is IPersistent persistent)
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
            }

            // Initialize the object if it implements the IInitializable interface.
            if (this is IInitializable initializable)
                initializable.Initialize();

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
            NotifyObjectWasEnabled();
        }

        protected void OnDisable()
        {
            NotifyObjectWasDisabled();
        }

        protected void OnDestroy()
        {
            NotifyObjectWasDestroyed();

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
    }
}