using System.Collections;
using FastUnityCreationKit.Annotations.Data;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Editor.Validation.Abstract;
using FastUnityCreationKit.Editor.Validation.Data;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Logging;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(NoNullEntriesValidator.InternalValueValidator))]
[assembly: RegisterValidator(typeof(NoNullEntriesValidator.InternalRootObjectValidator))]
namespace FastUnityCreationKit.Editor.Validation.Data
{
    public sealed class NoNullEntriesValidator : QuickAttributeBasedValidator<
        NoNullEntriesValidator, NoNullEntriesAttribute, IDataContainer>
    {
        public override void Validate(ValidationResult result, IDataContainer value)
        {
            IList list = value.RawData;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].IsNull())
                {
                    Guard<ValidationLogConfig>.Error($"Removing null item found in {value.GetType().Name} at index {i}");
                    list.RemoveAt(i);
                }
            }
        }
    }
}