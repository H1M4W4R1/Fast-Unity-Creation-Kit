using System;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Editor;
using FastUnityCreationKit.Unity.Interfaces.Callbacks;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace FastUnityCreationKit.Editor.Drawers
{
    [UsedImplicitly]
    public sealed class EventsViewDrawer : OdinValueDrawer<EventsView>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // Get parent of current property
            InspectorProperty parent = this.Property.Parent;
            while (parent != null)
            {
                // If parent is CKMonoBehaviour, break the loop
                if (parent?.ValueEntry?.WeakSmartValue is CKMonoBehaviour) break;
                parent = parent.Parent;
            }

            // We can draw this property only if it's parent is CKMonoBehaviour
            if (parent == null)
            {
                EditorGUILayout.LabelField("<color=red>Owner is not CKMonoBehaviour</color>");
                return;
            }

            // Get CKMonoBehaviour instance
            CKMonoBehaviour ownerBehaviour = (CKMonoBehaviour) parent.ValueEntry.WeakSmartValue;

            // Count interfaces that are callbacks and implemented by CKMonoBehaviour
            int count = 0;
            foreach (Type ifx in ownerBehaviour.GetType().GetInterfaces())
            {
                // Skip hidden interfaces
                if (ifx.GetCustomAttribute<HideInInspector>() != null) continue;

                // Skip generic types, we want to handle only concrete types
                if (ifx.IsGenericType) continue;
                if (!ifx.ImplementsOrInherits(typeof(ICKBehaviourCallback))) continue;

                count++;
            }

            GUIContent eventsLabel = new GUIContent("Registered Events");
            GUIContent eventsCountLabel = new GUIContent(count.ToString());

            (string eventsTooltip, string countTooltip) =
                ValueEntry.SmartValue.BuildTooltip(ownerBehaviour);

            eventsLabel.tooltip = eventsTooltip;
            eventsCountLabel.tooltip = countTooltip;
            
            // Draw count label
            EditorGUILayout.LabelField(eventsLabel, eventsCountLabel);
        }
    }
}