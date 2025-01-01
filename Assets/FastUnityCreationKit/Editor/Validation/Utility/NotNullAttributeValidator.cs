using System.Diagnostics.CodeAnalysis;
using FastUnityCreationKit.Editor.Validation.Utility;
using FastUnityCreationKit.Utility;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(SystemDiagnosticsNotNullAttributeValidator))]
[assembly: RegisterValidator(typeof(JetbrainsNotNullAttributeValidator))]
namespace FastUnityCreationKit.Editor.Validation.Utility
{
    public sealed class SystemDiagnosticsNotNullAttributeValidator : AttributeValidator<NotNullAttribute, object>
    {
        protected override void Validate(ValidationResult result)
        {
            if (Value.IsNull()) result.AddError("Value cannot be null.");
        }
    }

    public sealed class
        JetbrainsNotNullAttributeValidator : AttributeValidator<JetBrains.Annotations.NotNullAttribute, object>
    {
        protected override void Validate(ValidationResult result)
        {
            if (Value.IsNull()) result.AddError("Value cannot be null.");
        }
    }
}