using FastUnityCreationKit.Editor.Validation.Identification;
using FastUnityCreationKit.Identification.Abstract.Identifiers;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(IdentifierValueValidator))]

namespace FastUnityCreationKit.Editor.Validation.Identification
{
    public sealed class IdentifierValueValidator : ValueValidator<IIdentifier>
    {
        protected override void Validate(ValidationResult result)
        {
            if (!Value?.IsCreated ?? false) result.AddError("Identifier is not created. Something went wrong.");
        }
    }
}