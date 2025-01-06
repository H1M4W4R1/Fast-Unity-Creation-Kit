using FastUnityCreationKit.Input.Enums;
using JetBrains.Annotations;
using UnityEngine.InputSystem;

namespace FastUnityCreationKit.Input.Events.Data
{
    public readonly struct BindingChangeData
    {
        /// <summary>
        ///     Input action that had a binding changed.
        /// </summary>
        [NotNull] public readonly InputAction action;
        
        /// <summary>
        ///     Index of the binding that was changed within the <see cref="action"/>
        /// </summary>
        public readonly int bindingIndex;
        
        /// <summary>
        ///     Flags for all devices that were allowed to be used for the rebind.
        /// </summary>
        public readonly InputDeviceType allowedDevices;
        
        /// <summary>
        ///     Old effective path of the binding (before the change). In case of duplicate bindings, this will be
        ///     still the correct path.
        ///     Can be <see cref="string.Empty"/> if done externally.
        /// </summary>
        [NotNull] public readonly string oldEffectivePath;
        
        /// <summary>
        ///     New effective path of the binding (after the change). In case of duplicate bindings, this is the path
        ///     binding was attempted to be changed to.
        ///     Can be <see cref="string.Empty"/> if done externally.
        /// </summary>
        [NotNull] public readonly string newEffectivePath;

        /// <summary>
        ///     Check if the binding is a composite binding.
        /// </summary>
        public bool IsComposite => action.bindings[bindingIndex].isComposite;
        
        public BindingChangeData([NotNull] InputAction action, int bindingIndex, InputDeviceType allowedDevices,
            [NotNull] string oldEffectivePath, [NotNull] string newEffectivePath)
        {
            this.action = action;
            this.bindingIndex = bindingIndex;
            this.allowedDevices = allowedDevices;
            this.oldEffectivePath = oldEffectivePath;
            this.newEffectivePath = newEffectivePath;
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