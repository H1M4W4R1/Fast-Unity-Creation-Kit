using System;

namespace FastUnityCreationKit.Input.Enums
{
    [Flags] public enum InputDeviceType : uint
    {
        None = 0,
        
        Keyboard = 1 << 0,
        Mouse = 1 << 1,
        Gamepad = 1 << 2,
        Touch = 1 << 3,
        Pointer = 1 << 4,
        
        // Indicate that change was made externally and InputDeviceType is unknown.
        Unknown = 1 << 30,
        All = 0xFFFFFFFF
    }
}