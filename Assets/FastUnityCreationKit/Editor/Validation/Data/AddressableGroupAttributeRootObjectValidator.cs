using System.Reflection;
using FastUnityCreationKit.Annotations.Addressables;
using FastUnityCreationKit.Editor.Postprocessing.Annotations;
using FastUnityCreationKit.Editor.Validation.Data;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Editor.Extensions;
using FastUnityCreationKit.Utility.Logging;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

[assembly: RegisterValidator(typeof(AddressableGroupAttributeRootObjectValidator))]
namespace FastUnityCreationKit.Editor.Validation.Data
{
    // TODO: Same for GameObject prefabs
    // TODO: Also add validators for AutoRegisterInAttribute
    public sealed class AddressableGroupAttributeRootObjectValidator : RootObjectValidator<ScriptableObject>
    {
        protected override void Validate(ValidationResult result)
        {
            // Check, if has AddressableGroupAttribute    
            AddressableGroupAttribute attribute = Value.GetType().GetCustomAttribute<AddressableGroupAttribute>(true);
            if (attribute == null) return;

            // Ensure that the object is in the addressable group  
            AddressableGroupAttributeProcessor.TryUpdateAddressableGroup(Value);
            if (Value.SetAddressableGroup(attribute.GroupName, true, attribute.Labels))
                Guard<ValidationLogConfig>.Debug($"Assigned {Value.name} to addressable group {attribute.GroupName}");
        }
    }
}