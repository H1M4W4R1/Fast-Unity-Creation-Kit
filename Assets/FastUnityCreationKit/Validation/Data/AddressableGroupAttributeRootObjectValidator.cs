using System.Reflection;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Attributes;
using FastUnityCreationKit.Utility.Editor.Extensions;
using FastUnityCreationKit.Utility.Logging;
using FastUnityCreationKit.Validation.Data;
using FastUnityCreationKit.Validation.Postprocessors;
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
            AddressableGroupAttribute attribute = Value.GetType().GetCustomAttribute<AddressableGroupAttribute>(true);
            if (attribute == null) return;

            // Ensure that the object is in the addressable group 
            FastUnityCreationKitAttributePostprocessor.TryUpdateAddressableGroup(Value);
            if (Value.SetAddressableGroup(attribute.GroupName, true, attribute.Labels))
                Guard<ValidationLogConfig>.Debug($"Assigned {Value.name} to addressable group {attribute.GroupName}");
        }
    }
}