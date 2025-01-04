#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using FastUnityCreationKit.Core.Logging;
using JetBrains.Annotations;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Editor.Extensions
{
    public static class Addressable
    {
        public static bool IsAddressable(this Object obj)
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

            // Check if settings exist
            // When Addressable settings are not found object can't be addressable
            if (!settings) return false;

            // Get GUID of object
            string assetPath = AssetDatabase.GetAssetPath(obj);
            string guid = AssetDatabase.AssetPathToGUID(assetPath);

            // Check if entry exists
            AddressableAssetEntry e = settings.FindAssetEntry(guid);
            return e != null;
        }


        public static (string, AssetReference) GetAssetReference([NotNull] this Object obj,
            [CanBeNull] Type targetType = null)
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

            // Get object type
            Type objType = obj.GetType();

            // Check if settings exist
            if (settings)
            {
                // Get GUID of object
                string assetPath = AssetDatabase.GetAssetPath(obj);
                string guid = AssetDatabase.AssetPathToGUID(assetPath);

                // Check if entry already exists
                AddressableAssetEntry e = settings.FindAssetEntry(guid);

                if (e == null)
                {
                    Guard<ValidationLogConfig>.Warning(
                        $"Cannot get asset reference for {objType.GetCompilableNiceFullName()} object. Entry is not found.");
                    return (null, null);
                }

                // Get address
                string address = e.address;

                // Create asset reference
                Type genericType = typeof(AssetReferenceT<>).MakeGenericType(targetType ?? objType);
                AssetReference assetReference = (AssetReference) Activator.CreateInstance(genericType, guid);

                return (address, assetReference);
            }

            Guard<ValidationLogConfig>.Error(
                $"Cannot get asset reference for {objType.GetCompilableNiceFullName()} object. Addressable settings are not found.");

            return (null, null);
        }

        public static bool SetAddressableGroup<TObject>(this TObject obj,
            string groupName,
            bool readOnly = false,
            params string[] labels)
            where TObject : Object
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

            // Check if settings exist
            if (settings)
            {
                // Check if group exists, if not create it
                AddressableAssetGroup group = settings.FindGroup(groupName);
                if (!group)
                    group = settings.CreateGroup(groupName, false, false, true, null, typeof(ContentUpdateGroupSchema),
                        typeof(BundledAssetGroupSchema));

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
                    e.ReadOnly = readOnly;

                    NotifyAddressables(settings, group, e, AddressableAssetSettings.ModificationEvent.EntryCreated);
                }
                else if (e.parentGroup != group)
                {
                    e.ReadOnly = false;
                    e = settings.CreateOrMoveEntry(guid, group, false, false);
                    e.SetAddress(obj.name);
                    modified = true;
                    e.ReadOnly = readOnly;

                    NotifyAddressables(settings, group, e, AddressableAssetSettings.ModificationEvent.EntryMoved);
                }

                // Validate labels
                e.ReadOnly = false;
                for (int i = 0; i < labels.Length; i++)
                {
                    // Check if label is already set, if not set it
                    if (e.labels.Contains(labels[i])) continue;

                    e.SetLabel(labels[i], true, true, false);
                    modified = true;
                }

                // Set readonly property
                e.ReadOnly = readOnly;
                NotifyAddressables(settings, group, e, AddressableAssetSettings.ModificationEvent.EntryModified);
                EditorUtility.SetDirty(obj);
                return modified;
            }

            return false;
        }

        private static void NotifyAddressables([NotNull] AddressableAssetSettings settings,
            [NotNull] AddressableAssetGroup group,
            [NotNull] AddressableAssetEntry entry,
            AddressableAssetSettings.ModificationEvent evt)
        {
            List<AddressableAssetEntry> mod = new List<AddressableAssetEntry> {entry};

            group.SetDirty(evt, mod, false, true);
            settings.SetDirty(evt, mod, true);
        }
    }
}
#endif