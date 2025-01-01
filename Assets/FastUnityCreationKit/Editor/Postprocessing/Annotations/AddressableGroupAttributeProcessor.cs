using System;
using System.Reflection;
using FastUnityCreationKit.Annotations.Attributes;
using FastUnityCreationKit.Editor.Postprocessing.Abstract;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Editor.Extensions;
using FastUnityCreationKit.Utility.Logging;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Editor.Postprocessing.Annotations
{
    [UsedImplicitly]
    [Order(int.MinValue)] // Execute before any other processors for created assets to have correct addressable group
    public sealed class AddressableGroupAttributeProcessor : QuickAssetProcessor<AddressableGroupAttributeProcessor>
    {
        protected override bool AssetIsRequired => true;

        public override void PostprocessCreatedAsset(string assetPath)
        {
            TryUpdateAddressableGroup(CurrentAsset);
        }

        internal static bool TryUpdateAddressableGroup([NotNull] Object obj)
        {
            Type type = obj.GetType();

            // Check if object has AddressableGroupAttribute 
            AddressableGroupAttribute groupAttribute = type.GetCustomAttribute<AddressableGroupAttribute>(true);
            if (groupAttribute != null)
            {
                // Assign object to addressable group, make it read-only
                // to prevent user modifications like changing address.
                if (obj.SetAddressableGroup(groupAttribute.GroupName, true, groupAttribute.Labels))
                    Guard<ValidationLogConfig>.Debug(
                        $"Found AddressableGroupAttribute and assigned {obj.name} to addressable group {groupAttribute.GroupName}");

                return true;
            }

            return false;
        }
    }
}