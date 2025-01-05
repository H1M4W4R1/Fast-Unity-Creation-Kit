using FastUnityCreationKit.Annotations.Editor;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace FastUnityCreationKit.Editor.Drawers
{
    [UsedImplicitly] [DrawerPriority(int.MaxValue)] // Make sure this drawer is the first to be called
    public sealed class NamedSectionAttributeDrawer : OdinAttributeDrawer<NamedSectionAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // Generate label text
            string labelText = Attribute.Name;

            // Setup color
            Color color = new Color(Attribute.Red, Attribute.Green, Attribute.Blue);

            // Setup color
            if (Attribute.WithColor)
                labelText = "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + labelText + "</color>";

            // Setup font size
            if (Attribute.FontSize > 0) labelText = "<size=" + Attribute.FontSize + ">" + labelText + "</size>";

            // Setup bold and italic
            if (Attribute.Bold) labelText = "<b>" + labelText + "</b>";
            if (Attribute.Italic) labelText = "<i>" + labelText + "</i>";

            GUIStyle richText = new(EditorStyles.label)
            {
                richText = true
            };

            // Draw label
            EditorGUILayout.LabelField(labelText, richText);

            // Line with color drawing 
            Color lineColor = Attribute.WithColor ? color : richText.normal.textColor;
            SirenixEditorGUI.HorizontalLineSeparator(lineColor);

            // Make ident
            EditorGUI.indentLevel++;

            // Proceed to next drawer
            CallNextDrawer(null);

            // Reset ident
            EditorGUI.indentLevel--;
        }
    }
}