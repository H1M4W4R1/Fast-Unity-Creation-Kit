using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace FastUnityCreationKit.Editor.Drawers
{
    [UsedImplicitly]
    public sealed class TimeSpanDrawer : OdinValueDrawer<TimeSpan>
    {
        protected override void DrawPropertyLayout([NotNull] GUIContent label)
        {
            // Set tooltip for the property if it is not set.
            if(string.IsNullOrEmpty(label.tooltip))   
                label.tooltip = "TimeSpan in seconds.";
            
            // Draw text label for the property.
            ValueEntry.SmartValue = TimeSpan.FromSeconds(EditorGUILayout.FloatField(label, (float) ValueEntry.SmartValue.TotalSeconds));
        }
    }
}