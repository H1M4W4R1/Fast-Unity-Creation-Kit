using FastUnityCreationKit.Core.Utility.Singleton;
using UnityEngine;

namespace FastUnityCreationKit.Core.Testing
{
    /// <summary>
    /// Test class for singleton.
    /// Used by test namespace, however needs to be placed outside Editor-only assembly
    /// to be able to be tested (otherwise Unity would throw an error).
    /// </summary>
    public sealed class ExampleMonoBehaviourClassSingleton : MonoBehaviour, IMonoBehaviourSingleton<ExampleMonoBehaviourClassSingleton>
    {
        
    }
}