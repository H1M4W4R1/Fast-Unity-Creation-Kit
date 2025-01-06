using JetBrains.Annotations;
using UnityEngine.InputSystem;

namespace FastUnityCreationKit.Input.Events.Data
{
    public struct BindingChangeData
    {
        [NotNull] public InputAction action;
        public int bindingIndex;
        
        public BindingChangeData([NotNull] InputAction action, int bindingIndex)
        {
            this.action = action;
            this.bindingIndex = bindingIndex;
        }
    }
}