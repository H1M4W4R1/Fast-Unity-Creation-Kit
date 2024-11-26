using System.Collections.Generic;
using FastUnityCreationKit.Structure.Singleton;
using FastUnityCreationKit.Unity.Events.Interfaces;
using UnityEngine;

namespace FastUnityCreationKit.Unity
{
    /// <summary>
    /// This class is implementation of FastMonoBehaviour processing.
    /// Should not be used directly.
    /// </summary>
    public sealed class FastMonoBehaviourManager : MonoBehaviour, IMonoBehaviourSingleton<FastMonoBehaviourManager> // Can't be FMB to prevent infinite loop
    {
        public static FastMonoBehaviourManager Instance => IMonoBehaviourSingleton<FastMonoBehaviourManager>.GetInstance();
      
        /// <summary>
        /// List of all known FastMonoBehaviours in the scene.
        /// </summary>
        private readonly List<FastMonoBehaviour> _fastMonoBehaviours = new();
       
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
            // Ensure this is on the root level, otherwise Unity may throw an error with DontDestroyOnLoad
            // as only root level objects can be made persistent
            transform.SetParent(null);

            // Make the object persistent
            DontDestroyOnLoad(gameObject);
            
            // Note that we're not setting instance here because it's handled by the singleton system
            // for more information see IMonoBehaviourSingleton.cs
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