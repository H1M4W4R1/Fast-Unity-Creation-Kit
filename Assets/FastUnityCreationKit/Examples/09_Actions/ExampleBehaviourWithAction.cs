using FastUnityCreationKit.Unity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.Examples._09_Actions
{
    public sealed class ExampleBehaviourWithAction : CKMonoBehaviour
    {
        [SerializeField] [ShowInInspector]
        private ExampleAction action;

    }
}