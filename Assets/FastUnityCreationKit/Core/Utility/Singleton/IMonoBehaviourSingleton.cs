using FastUnityCreationKit.Core.Utility.Internal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Core.Utility.Singleton
{
    /// <summary>
    /// Represents a singleton that is a <see cref="MonoBehaviour"/>.
    /// Use this interface to mark a MonoBehaviour as a singleton.
    /// </summary>
    public interface IMonoBehaviourSingleton<TSelf> : ISingleton<TSelf>
        where TSelf : MonoBehaviour, IMonoBehaviourSingleton<TSelf>, new()
    {
        /// <summary>
        /// Gets the instance of the singleton.
        /// Overwrites <see cref="ISingleton{TSelf}.GetInstance"/>
        /// </summary>
        public new static TSelf GetInstance()
        {
            Validation.AssertType<TSelf, MonoBehaviour>();

            // Check if the instance exists
            if (Instance) return Instance;

            // Find the instance
            Instance = Object.FindAnyObjectByType<TSelf>();

            // Check if the instance exists
            if (Instance) return Instance;

            // Create a new instance
            Instance = new GameObject(nameof(TSelf)).AddComponent<TSelf>();

            // Return the instance
            return Instance;
        }
    }
}