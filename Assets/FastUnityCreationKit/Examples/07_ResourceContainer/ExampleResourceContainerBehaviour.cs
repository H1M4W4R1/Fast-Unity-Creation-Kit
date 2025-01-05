using FastUnityCreationKit.Economy;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Actions;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace FastUnityCreationKit.Examples._07_ResourceContainer
{
    public sealed class ExampleResourceContainerBehaviour : CKMonoBehaviour
    {
        [ShowInInspector]
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