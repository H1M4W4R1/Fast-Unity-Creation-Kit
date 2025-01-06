using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Input;
using FastUnityCreationKit.Input.Enums;
using FastUnityCreationKit.Input.Events;
using FastUnityCreationKit.Input.Events.Data;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Local;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FastUnityCreationKit.Examples._10_InputSystem
{
    public sealed class InputSystemManager : CKManager<InputSystemManager>, IOnObjectCreatedCallback,
        IOnObjectDestroyedCallback
    {
        [Required] [SerializeField] private InputActionAsset inputActionAsset;

        public void OnObjectCreated()
        {
            // Initialize input system
            InputAPI.Initialize();
            
            inputActionAsset.Disable();
            inputActionAsset.FindAction("Key1").performed += context => Debug.Log("Key1 pressed");
            inputActionAsset.FindAction("Key2").performed += context => Debug.Log("Key2 pressed");
            inputActionAsset.FindAction("Key3").performed += context => Debug.Log("Key3 pressed");
            inputActionAsset.Enable();

            // attach to global events
            OnBindingChangeCompletedGlobalEvent.RegisterEventListener(OnBindingChangeCompleted);
            OnBindingDuplicateFoundGlobalEvent.RegisterEventListener(OnBindingDuplicateFound);
        }

        private UniTask OnBindingDuplicateFound(BindingChangeData data)
        {
            Debug.Log($"Duplicate binding found for action {data.action.name} at index {data.bindingIndex}. " +
                      $"Duplicate path: {data.newEffectivePath}");
            return UniTask.CompletedTask;
        }

        private UniTask OnBindingChangeCompleted(BindingChangeData data)
        {
            Debug.Log(
                $"Binding changed for action {data.action.name} at index {data.bindingIndex} from " +
                $"{data.oldEffectivePath} to {data.newEffectivePath}");
            return UniTask.CompletedTask;
        }

        [Button] public void RebindKey1()
        {
            if (!Application.isPlaying) return;

            InputAction action = inputActionAsset.FindAction("Key1");
            action.Disable();
            action.Rebind(InputDeviceType.Keyboard);
            action.Enable();
        }

        public void OnObjectDestroyed()
        {
        }
    }
}