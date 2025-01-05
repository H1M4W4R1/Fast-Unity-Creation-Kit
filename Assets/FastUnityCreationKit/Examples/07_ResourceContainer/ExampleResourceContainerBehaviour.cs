using FastUnityCreationKit.Annotations.Editor;
using FastUnityCreationKit.Economy;
using FastUnityCreationKit.Unity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.Examples._07_ResourceContainer
{
    public sealed class ExampleResourceContainerBehaviour : CKMonoBehaviour
    {
        [ShowInInspector] [NoLabel] 
        [field: SerializeField, HideInInspector]
        [NamedSection("Resource container", 14)]
        public ExampleResourceContainer ResourceContainer = null;

        [Button("Set-up resource container")]
        private void Start()
        {
            // Get resource by identifier 
            ResourceBase resourceBase = ResourceDatabase.Instance.GetResource<ExampleResource>();
            if(ReferenceEquals(resourceBase, null)) return;
            ResourceContainer = new ExampleResourceContainer(resourceBase.Id);
        }

    }
}