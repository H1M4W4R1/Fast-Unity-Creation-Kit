using FastUnityCreationKit.Identification.Abstract.Identifiers;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace FastUnityCreationKit.Editor.Drawers.Identification
{
    /// <summary>
    ///     Base class to render all numeric identifiers.
    /// </summary>
    public abstract class
        NumericIdentifierDrawerBase<TNumberIdentifier, TNumber> : OdinValueDrawer<TNumberIdentifier>
        where TNumberIdentifier : INumberIdentifier<TNumber>
    {
        protected override void DrawPropertyLayout([NotNull] GUIContent label)
        {
            // Get string value
            string stringValue = ValueEntry.SmartValue.ToString();

            // Configure tooltip
            if (string.IsNullOrEmpty(label.tooltip)) label.tooltip = ValueEntry.SmartValue.GetDebugTooltipText();

            GUIContent labelContent = new(stringValue)
            {
                tooltip = label.tooltip
            };

            // Draw string value
            EditorGUILayout.LabelField(label, labelContent);
        }
    }
}