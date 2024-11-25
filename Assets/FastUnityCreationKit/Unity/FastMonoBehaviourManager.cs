using System.Collections.Generic;
using FastUnityCreationKit.Unity.Events.Interfaces;
using UnityEngine;

namespace FastUnityCreationKit.Unity
{
    /// <summary>
    /// This class is implementation of FastMonoBehaviour processing.
    /// Should not be used directly.
    /// </summary>
    public sealed class FastMonoBehaviourManager : MonoBehaviour // Can't be FMB to prevent infinite loop
    {
        private static FastMonoBehaviourManager _instance;

        /// <summary>
        /// List of all known FastMonoBehaviours in the scene.
        /// </summary>
        private List<FastMonoBehaviour> _fastMonoBehaviours = new List<FastMonoBehaviour>();

        /// <summary>
        /// Instance of the FastMonoBehaviourManager.
        /// </summary>
        public static FastMonoBehaviourManager Instance
        {
            get
            {
                // Try to return the instance if it's already created
                if (_instance) return _instance;

                // Find the instance in the scene 
                _instance = FindAnyObjectByType<FastMonoBehaviourManager>();

                // If the instance is not found, create a new one
                if (!_instance)
                    _instance = new GameObject("FastMonoBehaviourManager").AddComponent<FastMonoBehaviourManager>();

                // Return the instance
                return _instance;
            }
        }

        /// <summary>
        /// Returns the first object found in the scene of the specified type.
        /// </summary>
        internal void RegisterFastMonoBehaviour(FastMonoBehaviour fastMonoBehaviour)
        {
            if (!_fastMonoBehaviours.Contains(fastMonoBehaviour))
                _fastMonoBehaviours.Add(fastMonoBehaviour);
        }

        /// <summary>
        /// Removes the specified object from the list of known FastMonoBehaviours.
        /// </summary>
        internal void UnregisterFastMonoBehaviour(FastMonoBehaviour fastMonoBehaviour)
        {
            if (_fastMonoBehaviours.Contains(fastMonoBehaviour))
                _fastMonoBehaviours.Remove(fastMonoBehaviour);
        }

        /// <summary>
        /// Used to set up the instance of the FastMonoBehaviourManager.
        /// </summary>
        private void Awake()
        {
            // Check if the instance is already created
            if (_instance && _instance != this)
            {
                // Destroy the current instance
                Destroy(gameObject);
                return;
            }

            // Set the instance
            _instance = this;

            // Ensure this is on the root level, otherwise Unity may throw an error with DontDestroyOnLoad
            // as only root level objects can be made persistent
            transform.SetParent(null);

            // Make the object persistent
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// This is main update loop of the FastMonoBehaviour system.
        /// This allows for quick updating of multiple objects based on their interfaces.
        /// May be slightly slower (shouldn't be due to lack of P-Invoke operations) than Unity's built-in update loop,
        /// but allows for more control over the update process.
        /// </summary>
        private void Update()
        {
            // Compute delta time
            float deltaTime = Time.deltaTime;

            // Loop through all known FastMonoBehaviours and check for PreUpdateCallbacks
            for (int fastMonoBehaviourIndex = 0;
                 fastMonoBehaviourIndex < _fastMonoBehaviours.Count;
                 fastMonoBehaviourIndex++)
            {
                FastMonoBehaviour fastMonoBehaviour = _fastMonoBehaviours[fastMonoBehaviourIndex];

                if (fastMonoBehaviour is IPreUpdateCallback preUpdate)
                    preUpdate.OnBeforeObjectUpdated(deltaTime);
            }

            // Loop through all known FastMonoBehaviours and check for UpdateCallbacks
            for (int fastMonoBehaviourIndex = 0;
                 fastMonoBehaviourIndex < _fastMonoBehaviours.Count;
                 fastMonoBehaviourIndex++)
            {
                FastMonoBehaviour fastMonoBehaviour = _fastMonoBehaviours[fastMonoBehaviourIndex];

                if (fastMonoBehaviour is IUpdateCallback update)
                    update.OnObjectUpdated(deltaTime);
            }

            // Loop through all known FastMonoBehaviours and check for PostUpdateCallbacks
            for (int fastMonoBehaviourIndex = 0;
                 fastMonoBehaviourIndex < _fastMonoBehaviours.Count;
                 fastMonoBehaviourIndex++)
            {
                FastMonoBehaviour fastMonoBehaviour = _fastMonoBehaviours[fastMonoBehaviourIndex];

                if (fastMonoBehaviour is IPostUpdateCallback postUpdate)
                    postUpdate.OnAfterObjectUpdated(deltaTime);
            }
        }

        /// <summary>
        /// For more reference, see <see cref="Update"/> method.
        /// </summary>
        public void FixedUpdate()
        {
            // Loop through all known FastMonoBehaviours and check for FixedUpdateCallbacks
            for (int fastMonoBehaviourIndex = 0;
                 fastMonoBehaviourIndex < _fastMonoBehaviours.Count;
                 fastMonoBehaviourIndex++)
            {
                FastMonoBehaviour fastMonoBehaviour = _fastMonoBehaviours[fastMonoBehaviourIndex];

                if (fastMonoBehaviour is IFixedUpdateCallback fixedUpdate)
                    fixedUpdate.OnObjectFixedUpdated();
            }
        }
    }
}