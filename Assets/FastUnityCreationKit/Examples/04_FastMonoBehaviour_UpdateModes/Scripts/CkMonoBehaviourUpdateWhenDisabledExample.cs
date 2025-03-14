﻿using FastUnityCreationKit.Unity.Time.Enums;

namespace FastUnityCreationKit.Examples._04_FastMonoBehaviour_UpdateModes.Scripts
{
    /// <summary>
    ///     You can update the object even when it is disabled.
    ///     Beware that it needs to be enabled at least once to start updating.
    /// </summary>
    public sealed class CkMonoBehaviourUpdateWhenDisabledExample : CkMonoBehaviourExampleBase
    {
        public override UpdateMode UpdateMode => UpdateMode.UpdateWhenDisabled;
    }
}