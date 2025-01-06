using System;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Input.Enums;
using FastUnityCreationKit.Input.Events;
using FastUnityCreationKit.Input.Events.Data;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FastUnityCreationKit.Input
{
    /// <summary>
    /// Used to handle 
    /// </summary>
    public static class InputAPI
    {
        /// <summary>
        ///     Rebinding operation that is already in progress.
        /// </summary>
        private static InputActionRebindingExtensions.RebindingOperation _rebindingOperation;
        
        /// <summary>
        ///     Initializes the input API. Call this method at the start of the application
        ///     to ensure that input system is properly initialized.
        /// </summary>
        public static void Initialize()
        {
            // Attach to input system action change event
            InputSystem.onActionChange += OnInputActionChanged;
            
            // Ensure that rebinding operation is disposed when application quits
            Application.quitting += () =>
            {
                _rebindingOperation?.Cancel();
                _rebindingOperation?.Dispose();
                _rebindingOperation = null;
                
                // Unsubscribe from input system action change event
                InputSystem.onActionChange -= OnInputActionChanged;
            };
        }

        /// <summary>
        ///     Starts to rebind the provided action with the provided binding name.
        /// </summary>
        public static bool Rebind(
            [NotNull] this InputAction action,
            [NotNull] string bindingName,
            InputDeviceType allowedDevices = InputDeviceType.All)
        {
            // Get binding index from action and binding name
            if (!GetBindingFromAction(action, bindingName, out int bindingIndex)) return false;

            if (action.bindings[bindingIndex].isComposite)
            {
                int firstPartIndex = bindingIndex + 1;
                if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isPartOfComposite)
                    _Rebind(action, firstPartIndex, allowedDevices, allCompositeParts: true);
            }
            else
            {
                _Rebind(action, bindingIndex, allowedDevices);
            }

            return true;
        }

        /// <summary>
        ///     Internal rebind process for input action.
        /// </summary>
        private static void _Rebind(
            [NotNull] InputAction action,
            int bindingIndex,
            InputDeviceType allowedDevices,
            bool allCompositeParts = false)
        {
            // Cancel current rebind operation to prevent conflicts
            _rebindingOperation?.Cancel();
            _rebindingOperation?.Dispose();
            _rebindingOperation = null;

            // Create new rebinding operation
            _rebindingOperation = action.PerformInteractiveRebinding(bindingIndex);

            // Handle controls check using allowed devices
            if ((allowedDevices & InputDeviceType.Keyboard) == 0)
                _rebindingOperation = _rebindingOperation.WithControlsExcluding("<Keyboard>");
            if ((allowedDevices & InputDeviceType.Mouse) == 0)
                _rebindingOperation = _rebindingOperation.WithControlsExcluding("<Mouse>");
            if ((allowedDevices & InputDeviceType.Gamepad) == 0)
                _rebindingOperation = _rebindingOperation.WithControlsExcluding("<Gamepad>");
            if ((allowedDevices & InputDeviceType.Touch) == 0)
            {
                _rebindingOperation = _rebindingOperation.WithControlsExcluding("<Touch>");
                _rebindingOperation = _rebindingOperation.WithControlsExcluding("<Touchscreen>");
            }

            if ((allowedDevices & InputDeviceType.Pointer) == 0)
                _rebindingOperation = _rebindingOperation.WithControlsExcluding("<Pointer>");

            // Handle rebind operation events
            _rebindingOperation = _rebindingOperation.OnCancel(OnOperationCancelled);
            _rebindingOperation = _rebindingOperation.OnComplete(OnOperationCompleted);

            // Trigger global event for binding change started
            OnBindingChangeStartedGlobalEvent.TriggerEvent(new BindingChangeData(action, bindingIndex));
            
            // Start rebind operation
            _rebindingOperation.Start();
            
            return;

#region REBIND_EVENTS

            // Handle operation cancelled event
            void OnOperationCancelled(
                [NotNull] InputActionRebindingExtensions.RebindingOperation rebindingOperation)
            {
                OnBindingChangeCancelledGlobalEvent.TriggerEvent(new BindingChangeData(action, bindingIndex));
                _rebindingOperation?.Dispose();
                _rebindingOperation = null;
            }

            // Handle operation completed event
            void OnOperationCompleted(
                [NotNull] InputActionRebindingExtensions.RebindingOperation rebindingOperation)
            {
                // TODO: Handle duplicates / conflicts within current actionMap
                //     if duplicate is found create a new event to handle this scenario.
                //     This will also affect the reset to default method as bindings may collide after reset.
                //     For now we will assume that there are no duplicates and proceed with the rebind.
                
                OnBindingChangedGlobalEvent.TriggerEvent(new BindingChangeData(action, bindingIndex));
                _rebindingOperation?.Dispose();
                _rebindingOperation = null;

                // If there's more composite parts we should bind, initiate a rebind
                // for the next part.
                if (!allCompositeParts) return;

                // Get next binding index and perform rebind
                int nextBindingIndex = bindingIndex + 1;
                if (nextBindingIndex < action.bindings.Count &&
                    action.bindings[nextBindingIndex].isPartOfComposite)
                    _Rebind(action, nextBindingIndex, allowedDevices, true);
            }

#endregion
        }

        /// <summary>
        ///     Used to reset input action binding to default value.
        /// </summary>
        public static bool ResetToDefault([NotNull] this InputAction action, [NotNull] string bindingName)
        {
            // Get binding index from action and binding name
            if (!GetBindingFromAction(action, bindingName, out int bindingIndex)) return false;

            // Reset binding to default, for composite bindings remove all parts
            if (action.bindings[bindingIndex].isComposite)
            {
                for (int i = bindingIndex + 1;
                     i < action.bindings.Count && action.bindings[i].isPartOfComposite;
                     ++i)
                    action.RemoveBindingOverride(i);
            }
            else
                action.RemoveBindingOverride(bindingIndex);

            // Execute global events on changed binding
            BindingChangeData changeData = new(action, bindingIndex);

            OnBindingChangedGlobalEvent.TriggerEvent(changeData);
            OnBindingResetGlobalEvent.TriggerEvent(changeData);
            return true;
        }

        /// <summary>
        ///     Uses provided <see cref="InputActionAsset"/> to find action and binding index of the provided
        ///     action and binding name.
        ///     Internally calls to <see cref="GetBindingFromAction"/> to find binding index on action that was
        ///     found within the asset.
        /// </summary>
        public static bool GetActionAndBinding(
            [NotNull] this InputActionAsset asset,
            [NotNull] string actionName,
            [NotNull] string bindingName,
            [CanBeNull] out InputAction action,
            out int bindingIndex)
        {
            // Get action from asset
            action = asset.FindAction(actionName);

            // Get binding from action
            return GetBindingFromAction(action, bindingName, out bindingIndex);
        }

        /// <summary>
        ///     Uses provided <see cref="InputActionReference"/> to find binding index of the provided binding name.
        ///     Internally calls to <see cref="GetBindingFromAction"/> to find binding index.
        /// </summary>
        public static bool GetActionAndBinding(
            [NotNull] this InputActionReference reference,
            [NotNull] string bindingName,
            [CanBeNull] out InputAction action,
            out int bindingIndex)
        {
            // Get action from reference
            action = reference.action;

            // Get binding from action
            return GetBindingFromAction(action, bindingName, out bindingIndex);
        }

        /// <summary>
        ///     Uses provided <see cref="InputAction"/> to find binding index of the provided binding name.
        /// </summary>
        public static bool GetBindingFromAction(
            [CanBeNull] InputAction action,
            [NotNull] string bindingName,
            out int bindingIndex)
        {
            bindingIndex = -1;

            // Ensure action is not null or binding name is not provided
            if (action == null || string.IsNullOrEmpty(bindingName))
            {
                return false;
            }

            // Get binding from action
            Guid bindingId = new(bindingName);
            bindingIndex = action.bindings.IndexOf(x => x.id == bindingId);
            if (bindingIndex != -1) return true;

            // Log error if binding was not found
            Guard<ValidationLogConfig>.Error($"Cannot find binding with ID '{bindingId}' on '{action.name}'");
            return false;
        }

        /// <summary>
        ///     This is event attached to <see cref="InputSystem.onActionChange"/> to handle changes
        ///     in input actions.
        /// </summary>
        private static void OnInputActionChanged(object obj, InputActionChange change)
        {
            if (change != InputActionChange.BoundControlsChanged)
                return;

            // Notify for update of all bindings in the action map
            switch (obj)
            {
                case InputAction action: NotifyActionBindingsChanged(action); break;
                case InputActionMap actionMap: NotifyActionMapBindingsChanged(actionMap); break;
            }
        }
        
        private static void NotifyActionMapBindingsChanged([NotNull] InputActionMap actionMap)
        {
            // Notify for update of all bindings in the action map
            foreach (InputAction action in actionMap.actions)
                NotifyActionBindingsChanged(action);
        }

        private static void NotifyActionBindingsChanged([NotNull] InputAction action)
        {
            // Notify for update of all bindings in the action asset
            for (int bindingIndex = 0; bindingIndex < action.bindings.Count; bindingIndex++)
                OnBindingChangedGlobalEvent.TriggerEvent(new BindingChangeData(action, bindingIndex));
        }
    }
}