using FastUnityCreationKit.Input.Enums;
using JetBrains.Annotations;
using UnityEngine.InputSystem;

namespace FastUnityCreationKit.Input.Events.Data
{
    public struct BindingChangeData
    {
        [NotNull] public InputAction action;
        public int bindingIndex;
        public InputDeviceType allowedDevices;

        public BindingChangeData([NotNull] InputAction action, int bindingIndex, InputDeviceType allowedDevices)
        {
            this.action = action;
            this.bindingIndex = bindingIndex;
            this.allowedDevices = allowedDevices;
        }

        /// <summary>
        ///     This method can be called if rebind was not successful (due to user canceling the rebind process
        ///     or key duplication). It will start the rebind process again for the same action, binding index
        ///     and allowed devices unless <see cref="allowedDevices"/> is <see cref="InputDeviceType.Unknown"/>.
        ///     Which would indicate that this change was made externally, and we don't know what devices are allowed.
        /// </summary>
        public void RebindAgain()
        {
            if(allowedDevices == InputDeviceType.Unknown) return;
            action.Rebind(bindingIndex, allowedDevices);
        }
    }
}