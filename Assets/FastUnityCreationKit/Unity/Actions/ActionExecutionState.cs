using System;

namespace FastUnityCreationKit.Unity.Actions
{
    /// <summary>
    ///     Represents action execution state - success, failure, cooldown, etc.
    ///     Can be used to validate action execution result.
    /// </summary>
    [Flags] public enum ActionExecutionState
    {
        // Action execution was successful.
        Success = 1 << 0,
        
        // Action execution failed for any reason.
        Failed = 1 << 1,
        
        // Action execution failed due to cooldown.
        OnCooldown = 1 << 2,
        
        // Action execution was interrupted by other action or event.
        Interrupted = 1 << 3,
        
        // Action execution was canceled by user.
        Cancelled = 1 << 4,
        
        // Action is disabled and can't be executed
        Disabled = 1 << 5,
        
        // Action can't be executed due to missing requirements
        MissingRequirements = 1 << 6,
        
        // Action is not ready to be executed
        NotReady = 1 << 7
    }
}