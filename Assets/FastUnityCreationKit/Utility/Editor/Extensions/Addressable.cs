#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;

namespace FastUnityCreationKit.Utility.Editor.Extensions
{
    public static class Addressable
    {
        public static bool SetAddressableGroup<TObject>(this TObject obj, string groupName, params string[] labels)
            where TObject : Object
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

            // Check if settings exist
            if (settings)
            {
                // Check if group exists, if not create it
                AddressableAssetGroup group = settings.FindGroup(groupName);
                if (!group)
                    group = settings.CreateGroup(groupName, false, false, true, null, typeof(ContentUpdateGroupSchema), typeof(BundledAssetGroupSchema));

                // Get GUID of object
                string assetPath = AssetDatabase.GetAssetPath(obj);
                string guid = AssetDatabase.AssetPathToGUID(assetPath);

                bool modified = false;
                
                // Check if entry already exists
                AddressableAssetEntry e = settings.FindAssetEntry(guid);
                
                // If entry doesn't exist, create it
                // If entry exists but is in a different group, move it  
                if (e == null)
                {
                    e = settings.CreateOrMoveEntry(guid, group, false, false);
                    e.SetAddress(obj.name);
           
                    modified = true;
                }
                
                else if (e.parentGroup != group)
                {
                    e = settings.CreateOrMoveEntry(guid, group, false, false);
                    e.SetAddress(obj.name);
                    modified = true;
                }
                    
                // Validate labels
                for (int i = 0; i < labels.Length; i++)
                {
                    // Check if label is already set, if not set it
                    if (e.labels.Contains(labels[i])) continue;
                    
                    e.SetLabel(labels[i], true, true, false);
                    modified = true;
                }

                // Set dirty
                List<AddressableAssetEntry> entriesModified = new() {e};
                group.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entriesModified, false, true);
                settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entriesModified, true, false);

                return modified;
            }

            return false;
        }
        
    }
}
#endif