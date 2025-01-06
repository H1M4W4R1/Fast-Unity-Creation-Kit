using System;
using System.Collections.Generic;
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
        ///     Used to rebind the provided action with default binding (at index 0).
        /// </summary>
        public static bool Rebind(
            [NotNull] this InputActionReference reference,
            InputDeviceType allowedDevices = InputDeviceType.All)
        {
            InputAction action = reference.action;
            return action != null && action.Rebind(allowedDevices);
        }

        /// <summary>
        ///     Used to rebind the provided action with default binding (at index 0).
        /// </summary>
        public static bool Rebind(
            [NotNull] this InputAction action,
            InputDeviceType allowedDevices = InputDeviceType.All)
        {
            // Get action bindings count
            if (action.bindings.Count != 1)
            {
                Guard<ValidationLogConfig>.Error($"Cannot rebind action '{action.name}' with multiple bindings. " +
                                                 $"You need to specify binding name or index.");
                return false;
            }

            // Rebind action
            return action.Rebind(0, allowedDevices);
        }

        /// <summary>
        ///     Used to rebind the provided action with the provided binding name.
        ///     Requires map to be disabled before rebind.
        /// </summary>
        public static bool Rebind(
            [NotNull] this InputActionReference reference,
            [NotNull] string bindingName,
            InputDeviceType allowedDevices = InputDeviceType.All)
        {
            // Get action from reference
            InputAction action = reference.action;
            return action != null && action.Rebind(bindingName, allowedDevices);
        }

        /// <summary>
        ///     Starts to rebind the provided action with the provided binding name.
        ///     Requires map to be disabled before rebind.
        /// </summary>
        public static bool Rebind(
            [NotNull] this InputAction action,
            [NotNull] string bindingName,
            InputDeviceType allowedDevices = InputDeviceType.All)
        {
            // Get binding index from action and binding name
            return GetBindingFromAction(action, bindingName, out int bindingIndex) &&
                   Rebind(action, bindingIndex, allowedDevices);
        }

        /// <summary>
        ///     Starts to rebind the provided action with the provided binding name.
        ///     Requires map to be disabled before rebind.
        /// </summary>
        public static bool Rebind(
            [NotNull] this InputActionReference reference,
            int bindingIndex,
            InputDeviceType allowedDevices = InputDeviceType.All)
        {
            // Get action from reference
            InputAction action = reference.action;
            return action != null && action.Rebind(bindingIndex, allowedDevices);
        }

        /// <summary>
        ///     Starts to rebind the provided action with the provided binding index.
        ///     Requires map to be disabled before rebind.
        /// </summary>
        public static bool Rebind(
            [NotNull] this InputAction action,
            int bindingIndex,
            InputDeviceType allowedDevices = InputDeviceType.All)
        {
            // Check if device type is unknown, this is not allowed for rebind
            if ((allowedDevices & InputDeviceType.Unknown) != 0)
            {
                Guard<ValidationLogConfig>.Error("Cannot rebind with unknown allowed devices.");
                return false;
            }

            // Check if binding index is valid (within action bindings range)
            if (bindingIndex < 0 || bindingIndex >= action.bindings.Count)
            {
                Guard<ValidationLogConfig>.Error($"Invalid binding index '{bindingIndex}' for '{action.name}'");
                return false;
            }

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

            // Cache current binding override path
            // to be able to reset it if binding has failed.
            string oldEffectivePath = action.bindings[bindingIndex].effectivePath;
            string oldBindingOverride = action.bindings[bindingIndex].overridePath;

            // Create new rebinding operation
            _rebindingOperation = action.PerformInteractiveRebinding(bindingIndex);

            // Run through allowed device enum
            // we're skipping unknown device as it's internal value
            for (byte i = 0; i < 31; i++)
            {
                // Get enum value
                InputDeviceType deviceType = (InputDeviceType) (1 << i);
                
                // Get device name
                string deviceName = deviceType.ToString();
                
                // Check if device name should be skipped as enum value is not defined
                if (string.IsNullOrEmpty(deviceName)) continue;
                
                // If device is not allowed, exclude it
                if ((allowedDevices & deviceType) == 0)
                    _rebindingOperation = _rebindingOperation.WithControlsExcluding($"<{deviceName}>");
            }
            
            // Handle rebind operation events
            _rebindingOperation = _rebindingOperation.OnCancel(OnOperationCancelled);
            _rebindingOperation = _rebindingOperation.OnComplete(OnOperationCompleted);

            // Trigger global event for binding change started
            OnBindingChangeStartedGlobalEvent.TriggerEvent(new BindingChangeData(action, bindingIndex,
                allowedDevices, oldEffectivePath, action.bindings[bindingIndex].effectivePath));

            // Start rebind operation
            _rebindingOperation.Start();

            return;

#region REBIND_EVENTS

            // Handle operation cancelled event
            void OnOperationCancelled(
                [NotNull] InputActionRebindingExtensions.RebindingOperation rebindingOperation)
            {
                OnBindingChangeCancelledGlobalEvent.TriggerEvent(new BindingChangeData(action, bindingIndex,
                    allowedDevices, oldEffectivePath, action.bindings[bindingIndex].effectivePath));
                _rebindingOperation?.Dispose();
                _rebindingOperation = null;
            }

            // Handle operation completed event
            void OnOperationCompleted(
                [NotNull] InputActionRebindingExtensions.RebindingOperation rebindingOperation)
            {
                // Check for duplicates in the action map and handle them accordingly.
                // We don't need to handle composites here as they are already handled
                // by the method that is recursive.
                if (SearchForDuplicate(action, bindingIndex, allCompositeParts))
                {
                    string newEffectivePath = action.bindings[bindingIndex].effectivePath;

                    // Reset binding to default if duplicate is found and old binding was set.
                    if (oldBindingOverride != null) action.ApplyBindingOverride(bindingIndex, oldBindingOverride);

                    // Notify for duplicate found
                    OnBindingDuplicateFoundGlobalEvent.TriggerEvent(new BindingChangeData(action, bindingIndex,
                        allowedDevices, oldEffectivePath, newEffectivePath));

                    // Dispose rebinding operation and return.
                    _rebindingOperation?.Dispose();
                    _rebindingOperation = null;
                    return;
                }

                // Trigger global event for binding change completed
                OnBindingChangeCompletedGlobalEvent.TriggerEvent(new BindingChangeData(action, bindingIndex,
                    allowedDevices, oldEffectivePath, action.bindings[bindingIndex].effectivePath));
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
        ///     Searches for duplicate bindings in the action map.
        /// </summary>
        public static bool SearchForDuplicate(
            [NotNull] this InputActionReference reference,
            [NotNull] string bindingName,
            bool allCompositeParts = false)
        {
            // Get action from reference
            InputAction action = reference.action;

            // Get binding from action
            if (!GetBindingFromAction(action, bindingName, out int bindingIndex)) return false;
            return action != null && action.SearchForDuplicate(bindingIndex, allCompositeParts);
        }

        /// <summary>
        ///     Searches for duplicate bindings in the action map.
        /// </summary>
        public static bool SearchForDuplicate(
            [NotNull] this InputAction action,
            int bindingIndex,
            bool allCompositeParts = false)
        {
            InputBinding newBinding = action.bindings[bindingIndex];
            int currentIndex = -1;

            // Search all bindings in the action map for duplicates
            foreach (InputBinding binding in action.actionMap.bindings)
            {
                currentIndex++;

                // For current action binding we need to handle composite bindings
                // with different indexes.
                if (binding.action == newBinding.action)
                {
                    if (binding.isPartOfComposite && currentIndex != bindingIndex)
                    {
                        if (binding.effectivePath == newBinding.effectivePath) return true;
                    }
                    else
                        continue;
                }

                // Otherwise we can check for duplicates by just comparing effective path
                if (binding.effectivePath == newBinding.effectivePath) return true;
            }

            // If we don't need to check all composite parts for duplicates
            // we can just return false.
            if (!allCompositeParts) return false;

            // If we need to check all composite parts for duplicates
            // we shall loop through all bindings in the action.
            for (int i = 1; i < bindingIndex; i++)
            {
                if (action.bindings[i].effectivePath == newBinding.overridePath) return true;
            }

            // If we don't find any duplicates we can return false.
            return false;
        }

        /// <summary>
        ///     Used to reset input action binding to default value.
        /// </summary>
        public static bool ResetToDefault(
            [NotNull] this InputActionReference reference,
            [NotNull] string bindingName,
            InputDeviceType allowedDevices = InputDeviceType.All)
        {
            // Get action from reference
            InputAction action = reference.action;
            return action != null && action.ResetToDefault(bindingName, allowedDevices);
        }

        /// <summary>
        ///     Used to reset input action binding to default value.
        /// </summary>
        public static bool ResetToDefault(
            [NotNull] this InputActionReference reference,
            int bindingIndex,
            InputDeviceType allowedDevices = InputDeviceType.All)
        {
            // Get action from reference
            InputAction action = reference.action;
            return action != null && action.ResetToDefault(bindingIndex, allowedDevices);
        }

        /// <summary>
        ///     Used to reset input action binding to default value.
        /// </summary>
        public static bool ResetToDefault(
            [NotNull] this InputAction action,
            [NotNull] string bindingName,
            InputDeviceType allowedDevices = InputDeviceType.All)
        {
            // Get binding from action
            return GetBindingFromAction(action, bindingName, out int bindingIndex) &&
                   action.ResetToDefault(bindingIndex, allowedDevices);
        }

        /// <summary>
        ///     Used to reset input action binding to default value.
        /// </summary>
        public static bool ResetToDefault(
            [NotNull] this InputAction action,
            int bindingIndex,
            InputDeviceType allowedDevices = InputDeviceType.All)
        {
            // Create binding overrides dictionary to cache all bindings that will be reset
            // to be able to revert them if duplicate is found.
            //
            // Key is always a binding index.
            Dictionary<int, string> oldBindingOverrides = new();
            Dictionary<int, string> oldBindingEffectivePaths = new();

            // Reset binding to default, for composite bindings remove all parts
            if (action.bindings[bindingIndex].isComposite)
            {
                for (int i = bindingIndex + 1;
                     i < action.bindings.Count && action.bindings[i].isPartOfComposite;
                     i++)
                {
                    oldBindingOverrides.Add(i, action.bindings[i].overridePath);
                    oldBindingEffectivePaths.Add(i, action.bindings[i].effectivePath);
                    action.RemoveBindingOverride(i);
                }
            }
            else
            {
                oldBindingOverrides.Add(bindingIndex, action.bindings[bindingIndex].overridePath);
                oldBindingEffectivePaths.Add(bindingIndex, action.bindings[bindingIndex].effectivePath);
                action.RemoveBindingOverride(bindingIndex);
            }

            // Check if any duplicates were created by resetting the binding
            // if any duplicates were found, reset all bindings to previous state
            // and notify for duplicate found.
            if (SearchForDuplicate(action, bindingIndex))
            {
                // If duplicate was found, reset binding to override and notify for duplicate found
                // perform for all bindings that were reset.
                foreach (KeyValuePair<int, string> bindingOverride in oldBindingOverrides)
                {
                    // Get effective path by index
                    string oldEffectivePath = oldBindingEffectivePaths[bindingOverride.Key];
                    string newEffectivePath = action.bindings[bindingOverride.Key].effectivePath;

                    // Revert binding override if it was set
                    if (bindingOverride.Value != null)
                        action.ApplyBindingOverride(bindingOverride.Key, bindingOverride.Value);

                    // Notify for duplicate found
                    OnBindingDuplicateFoundGlobalEvent.TriggerEvent(new BindingChangeData(action,
                        bindingOverride.Key,
                        allowedDevices, oldEffectivePath, newEffectivePath));
                }

                return false;
            }

            // IF NO DUPLICATES WERE FOUND
            // Raise events for all bindings that were reset
            foreach (KeyValuePair<int, string> bindingOverride in oldBindingOverrides)
            {
                // Get effective path by index
                string oldEffectivePath = oldBindingEffectivePaths[bindingOverride.Key];
                string newEffectivePath = action.bindings[bindingOverride.Key].effectivePath;

                // Notify for binding change completed
                OnBindingChangeCompletedGlobalEvent.TriggerEvent(new BindingChangeData(action, bindingOverride.Key,
                    allowedDevices, oldEffectivePath, newEffectivePath));
                OnBindingResetGlobalEvent.TriggerEvent(new BindingChangeData(action, bindingOverride.Key,
                    allowedDevices, oldEffectivePath, newEffectivePath));
            }

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
            if (change != InputActionChange.BoundControlsChanged) return;

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
            foreach (InputAction action in actionMap.actions) NotifyActionBindingsChanged(action);
        }

        private static void NotifyActionBindingsChanged([NotNull] InputAction action)
        {
            // Notify for update of all bindings in the action asset
            // TODO: Check if this event is not raised twice during rebind via InputAPI and remove it if so.
            for (int bindingIndex = 0; bindingIndex < action.bindings.Count; bindingIndex++)
                OnBindingChangeCompletedGlobalEvent.TriggerEvent(new BindingChangeData(action, bindingIndex,
                    InputDeviceType.Unknown, string.Empty, string.Empty));
        }
    }
}