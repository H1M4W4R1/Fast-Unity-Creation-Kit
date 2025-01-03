using System;
using FastUnityCreationKit.Annotations.Unity;
using FastUnityCreationKit.Editor.Validation.Utility;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Validation;
using Sirenix.Utilities;

[assembly: RegisterValidator(typeof(InvalidDefinitionValidator))]
namespace FastUnityCreationKit.Editor.Validation.Utility
{
    public sealed class InvalidDefinitionValidator : AttributeValidator<AutoCreatedObjectAttribute, SerializedScriptableObject>
    {
        protected override void Validate(ValidationResult result)
        {
            // Check if object name is equal to expected type name
            // if not it means that object was renamed
            Type type = Value.GetType();
            if(type.GetCompilableNiceFullName() != Value.name)
                result.AddError("Object name is not equal to expected type name.");
        }
    }
}