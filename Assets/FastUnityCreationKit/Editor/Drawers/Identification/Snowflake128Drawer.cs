using FastUnityCreationKit.Identification.Identifiers;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace FastUnityCreationKit.Editor.Drawers.Identification
{
    [UsedImplicitly] public sealed class Snowflake128Drawer : OdinValueDrawer<Snowflake128>
    {
        protected override void DrawPropertyLayout([NotNull] GUIContent label)
        {
            // Get string value
            string stringValue = ValueEntry.SmartValue.ToString();

            // Configure tooltip
            if (string.IsNullOrEmpty(label.tooltip)) label.tooltip = ValueEntry.SmartValue.GetDebugTooltipText();

            // Draw string value
            GUIContent labelContent = new(stringValue)
            {
                tooltip = label.tooltip
            };

            EditorGUILayout.LabelField(label, labelContent);
        }
    }
}