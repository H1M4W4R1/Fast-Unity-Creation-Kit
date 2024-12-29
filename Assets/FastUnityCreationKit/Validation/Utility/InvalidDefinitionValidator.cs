using System;
using FastUnityCreationKit.Utility.Attributes;
using FastUnityCreationKit.Validation.Utility;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(InvalidDefinitionValidator))]
namespace FastUnityCreationKit.Validation.Utility
{
    public sealed class InvalidDefinitionValidator : AttributeValidator<AutoCreatedObjectAttribute, SerializedScriptableObject>
    {
        protected override void Validate(ValidationResult result)
        {
            base.Validate(result);
            
            // Check if object name is equal to expected type name
            // if not it means that object was renamed
            Type type = Value.GetType();
            if(type.Name != Value.name)
                result.AddError("Object name is not equal to expected type name.");
        }
    }
}