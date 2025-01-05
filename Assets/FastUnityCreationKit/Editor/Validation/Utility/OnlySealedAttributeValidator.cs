using FastUnityCreationKit.Annotations.Utility;
using FastUnityCreationKit.Editor.Validation.Utility;
using Sirenix.OdinInspector.Editor.Validation;
using Sirenix.Utilities;

[assembly: RegisterValidator(typeof(OnlySealedAttributeValidator))]

namespace FastUnityCreationKit.Editor.Validation.Utility
{
    public sealed class OnlySealedAttributeValidator : AttributeValidator<OnlySealedAttribute>
    {
        protected override void Validate(ValidationResult result)
        {
            // Check if type is sealed
            if (Property.BaseValueEntry.TypeOfValue.IsSealed) return;
             
            // Add error
            string message =
                $"This value must be sealed. Found {Property.Info.PropertyName} with type " +
                $"{Property.BaseValueEntry.TypeOfValue.GetNiceFullName()} " +
                $"which is not sealed. Please make sure that the type is sealed and non-abstract as this type most likely " +
                $"does not support polymorphic serialization.";
            result.AddError(message);
        }
    }
}