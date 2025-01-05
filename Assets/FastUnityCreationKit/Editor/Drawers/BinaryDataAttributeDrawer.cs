using FastUnityCreationKit.Annotations.Editor;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace FastUnityCreationKit.Editor.Drawers
{
    [UsedImplicitly]
    public sealed class BinaryDataAttributeDrawer : OdinAttributeDrawer<BinaryDataAttribute, byte[]>
    {
        private const float MIN_BYTES_PER_ROW = 8;
        private const float MAX_BYTES_PER_PAGE = 1024;
        private const int BYTE_ENTRY_SIZE = 20;
        
        // One page consists of 512 bytes (16x8 array)
        private int _nPage = 0;
        private int _nPages = 0;
        private bool _open;
        
        
        protected override void DrawPropertyLayout([NotNull] GUIContent label)
        {
            // Compute the number of pages
            _nPages = Mathf.CeilToInt(this.ValueEntry.SmartValue.Length / MAX_BYTES_PER_PAGE);
            
            SirenixEditorGUI.BeginHorizontalToolbar();

            // Draw the foldout with small size
            GUIStyle foldoutStyle = new(EditorStyles.foldout)
            {
                fixedWidth = 15
            };
            
            _open = SirenixEditorGUI.Foldout(_open, GUIContent.none, foldoutStyle);
            
            // Draw the page number
            GUIContent pageHeader = new($"<b>{label.text} [Page {_nPage + 1} of {_nPages}]", label.tooltip);
            
            // Ensure rich text is enabled for the label style
            GUIStyle headerStyle = new(EditorStyles.label)
            {
                richText = true,
                alignment = TextAnchor.MiddleLeft
            };

            EditorGUILayout.LabelField(pageHeader, headerStyle);
            
            // Place buttons on right side if the foldout is open
            if (_open)
            {
                if (SirenixEditorGUI.ToolbarButton(EditorIcons.Previous)) Previous();
                if (SirenixEditorGUI.ToolbarButton(EditorIcons.Next)) Next();
            }

            SirenixEditorGUI.EndHorizontalToolbar();
            
            if(!_open) return;

            float availableWidth = EditorGUIUtility.currentViewWidth - BYTE_ENTRY_SIZE*5;
            
            // Compute how many bytes can be fit in a row due to current
            // inspector size
            int bytesInRow = Mathf.FloorToInt(availableWidth / BYTE_ENTRY_SIZE);
            
            // Round bytes to nearest power of two, but lower or equal 512
            bytesInRow = Mathf.ClosestPowerOfTwo(bytesInRow);
            
            float byteRendererWidth = bytesInRow * BYTE_ENTRY_SIZE * 1.2f;
            
            // Check if bytes in row does not exceed the width of the inspector
            if (byteRendererWidth > availableWidth) bytesInRow /= 2;

            // Limit to bytes per page and compute page size
            bytesInRow = Mathf.Clamp(bytesInRow, (int) MIN_BYTES_PER_ROW, (int) MAX_BYTES_PER_PAGE);
            int rowsPerPage = (int) MAX_BYTES_PER_PAGE / bytesInRow;
            
            bool drawingFinished = false;
            
            // Draw the data
            SirenixEditorGUI.BeginBox();
            for (int rowIndex = 0; rowIndex < rowsPerPage; rowIndex++)
            {
                SirenixEditorGUI.BeginHorizontalToolbar();
                // Draw indexing label with hex value
                EditorGUILayout.LabelField($"{rowIndex * 8 + _nPage * 128:X8}", GUILayout.Width(BYTE_ENTRY_SIZE*4));
      
                // Draw row
                for (int byteIndex = 0; byteIndex < bytesInRow; byteIndex++)
                {
                    // Compute index by shifting current byte index by
                    // row index multiplied by bytes in row and adding page offset
                    // to get the correct index in the array
                    int index = (int)(rowIndex * bytesInRow + byteIndex + _nPage * MAX_BYTES_PER_PAGE);
                    if (index >= ValueEntry.SmartValue.Length)
                    {
                        drawingFinished = true;
                        break;
                    }
                    
                    // Draw hex value of the byte
                    EditorGUILayout.LabelField($"{ValueEntry.SmartValue[index]:X2}", GUILayout.Width(BYTE_ENTRY_SIZE));
                    
                }
                SirenixEditorGUI.EndHorizontalToolbar();
                
                // Break if drawing is finished to prevent drawing extra rows
                if (drawingFinished) break;
            }
            SirenixEditorGUI.EndBox();
        }
        
        private void Previous()
        {
            _nPage = Mathf.Max(0, _nPage - 1);
        }
        
        private void Next()
        {
            _nPage = Mathf.Min(_nPages - 1, _nPage + 1);
        }
    }
}