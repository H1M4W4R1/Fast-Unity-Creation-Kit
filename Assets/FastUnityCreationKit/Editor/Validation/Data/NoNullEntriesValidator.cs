using System.Collections;
using FastUnityCreationKit.Annotations.Data;
using FastUnityCreationKit.Core.Extensions;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Editor.Validation.Abstract;
using FastUnityCreationKit.Editor.Validation.Data;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor.Validation;
using Sirenix.Utilities;

[assembly:
    RegisterValidator(
        typeof(QuickAttributeBasedValidator<NoNullEntriesValidator, NoNullEntriesAttribute, IDataContainer>.
            InternalValueValidator))]
[assembly:
    RegisterValidator(
        typeof(QuickAttributeBasedValidator<NoNullEntriesValidator, NoNullEntriesAttribute, IDataContainer>.
            InternalRootObjectValidator))]

namespace FastUnityCreationKit.Editor.Validation.Data
{
    public sealed class NoNullEntriesValidator : QuickAttributeBasedValidator<
        NoNullEntriesValidator, NoNullEntriesAttribute, IDataContainer>
    {
        public override void Validate(ValidationResult result, [NotNull] IDataContainer value)
        {
            IList list = value.RawData;
            for (int i = list.Count - 1; i >= 0; i--)
                if (list[i].IsNull())
                {
                    Guard<ValidationLogConfig>.Error(
                        $"Removing null item found in {value.GetType().GetCompilableNiceFullName()} at index {i}");
                    list.RemoveAt(i);
                }
        }
    }
}