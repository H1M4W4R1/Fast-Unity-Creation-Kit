using FastUnityCreationKit.Annotations.Editor;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace FastUnityCreationKit.Editor.Drawers
{
    /// <summary>
    /// Drawer for <see cref="NoLabelAttribute"/> that removes labels from properties. 
    /// </summary>
    [UsedImplicitly] public sealed class NoLabelAttributeDrawer : OdinAttributeDrawer<NoLabelAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(null);
        }
    }
}