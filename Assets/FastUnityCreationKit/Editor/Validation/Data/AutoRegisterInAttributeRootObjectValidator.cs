using System.Reflection;
using FastUnityCreationKit.Annotations.Data;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Editor.Postprocessing.Annotations;
using FastUnityCreationKit.Editor.Validation.Data;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

[assembly: RegisterValidator(typeof(AutoRegisterInAttributeRootObjectValidator))]
namespace FastUnityCreationKit.Editor.Validation.Data
{
    public sealed class AutoRegisterInAttributeRootObjectValidator : RootObjectValidator<Object>
    {
        protected override void Validate(ValidationResult result)
        {
            // Check, if object type has AutoRegisterInAttribute
            AutoRegisterInAttribute attribute =
                Value.GetType().GetCustomAttribute<AutoRegisterInAttribute>(true);
            if (attribute == null) return;
            
            // Skip if value is null
            if(!Value) return;
            
            // Try to register in containers
            AutoRegisterInAttributeProcessor.TryToRegisterInContainers(Value);
        }
    }
}