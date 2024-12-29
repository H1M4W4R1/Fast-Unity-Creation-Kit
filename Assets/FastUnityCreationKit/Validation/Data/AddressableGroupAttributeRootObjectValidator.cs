using System.Reflection;
using FastUnityCreationKit.Utility.Attributes;
using FastUnityCreationKit.Utility.Editor.Extensions;
using FastUnityCreationKit.Validation.Data;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

[assembly: RegisterValidator(typeof(AddressableGroupAttributeRootObjectValidator))]
namespace FastUnityCreationKit.Validation.Data
{
    public sealed class AddressableGroupAttributeRootObjectValidator : RootObjectValidator<ScriptableObject>
    {
        protected override void Validate(ValidationResult result)
        {
            // Check, if has AddressableGroupAttribute
            AddressableGroupAttribute attribute = GetType().GetCustomAttribute<AddressableGroupAttribute>();
            if (attribute == null) return;

            if (Value.SetAddressableGroup(attribute.GroupName, attribute.Labels))
            {
                Debug.Log($"Assigned {Value.name} to addressable group {attribute.GroupName}");
            }

        }

    }
}