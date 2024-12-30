using FastUnityCreationKit.Identification.Abstract.Identifiers;
using FastUnityCreationKit.Identification.Validation;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(IdentifierValueValidator))]
namespace FastUnityCreationKit.Identification.Validation
{
    public sealed class IdentifierValueValidator : ValueValidator<IIdentifier>
    {
        protected override void Validate(ValidationResult result)
        {
            if(!Value?.IsCreated ?? false)
                result.AddError($"Identifier is not created. Something went wrong.");
        }
    }
}