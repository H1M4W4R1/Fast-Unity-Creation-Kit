using System.Reflection;
using FastUnityCreationKit.Annotations.Utility;
using FastUnityCreationKit.Editor.Validation.Utility;
using Sirenix.OdinInspector.Editor.Validation;
using Sirenix.Utilities;
using UnityEngine;

[assembly: RegisterValidator(typeof(NotAllowedInObjectsAttributeValidator))]

namespace FastUnityCreationKit.Editor.Validation.Utility
{
    public sealed class NotAllowedInObjectsAttributeValidator : AttributeValidator<NotAllowedInObjectsAttribute>
    {
        protected override void Validate(ValidationResult result)
        {
            // Check if type is a subclass of Object, if not skip
            if(!Property.Info.TypeOfOwner.IsSubclassOf(typeof(Object))) return;
            
            // Check if type contains NotAllowedInObjectsAttribute
            if (Property.BaseValueEntry.TypeOfValue.GetCustomAttribute<NotAllowedInObjectsAttribute>() == null)
                return;

            // Add error
            string message =
                $"This value is not allowed in objects. Found {Property.Info.PropertyName} with type " +
                $"{Property.BaseValueEntry.TypeOfValue.GetNiceFullName()}";
            result.AddError(message);
        }
    }
}