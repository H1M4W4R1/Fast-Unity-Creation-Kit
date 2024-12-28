using System.Collections;
using FastUnityCreationKit.Data.Attributes;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Data.Validation;
using FastUnityCreationKit.Utility;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(NoNullEntriesAttributeValidator))]
namespace FastUnityCreationKit.Data.Validation
{
    public sealed class NoNullEntriesAttributeValidator : AttributeValidator<NoNullEntriesAttribute, IDataContainer>
    {
        protected override void Validate(ValidationResult result)
        {
            IList list = Value.RawData;
            for(int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].IsNull())
                    list.RemoveAt(i);
            }
        }
    }
}