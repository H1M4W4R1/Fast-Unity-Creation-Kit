using System.Reflection;
using FastUnityCreationKit.Annotations.Addressables;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Editor.Extensions;
using FastUnityCreationKit.Editor.Postprocessing.Annotations;
using FastUnityCreationKit.Editor.Validation.Data;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

[assembly: RegisterValidator(typeof(AddressableGroupAttributeRootObjectValidator))]
namespace FastUnityCreationKit.Editor.Validation.Data
{
    public sealed class AddressableGroupAttributeRootObjectValidator : RootObjectValidator<Object>
    {
        protected override void Validate(ValidationResult result)
        {
            // Check, if object type has AddressableGroupAttribute    
            AddressableGroupAttribute attribute =
                Value.GetType().GetCustomAttribute<AddressableGroupAttribute>(true);
            if (attribute == null) return;
            
            // Skip if value is null
            if(!Value) return;

            // Ensure that the object is in the addressable group  
            AddressableGroupAttributeProcessor.TryUpdateAddressableGroup(Value);
            if (Value.SetAddressableGroup(attribute.GroupName, true, attribute.Labels))
                Guard<ValidationLogConfig>.Debug(
                    $"Assigned {Value.name} to addressable group {attribute.GroupName}");
        }
    }
}