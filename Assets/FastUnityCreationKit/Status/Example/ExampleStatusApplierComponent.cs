using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.Status.Example
{
    public sealed class ExampleStatusApplierComponent : MonoBehaviour
    {
        [Button("Ignite")]
        public async void Ignite()
        {
            await GetComponent<EntityStatusComponent>().AddStatus<BurningStatus>(10);
        }
        
        [Button("Frenzy")]
        public async void Frenzy()
        {
            await GetComponent<EntityStatusComponent>().AddStatus<FrenzyStatus>(10);
        }
        
        [Button("Remove All Statuses")]
        public async void RemoveAllStatuses()
        {
            await GetComponent<EntityStatusComponent>().ClearAll();
        }
        
    }
}