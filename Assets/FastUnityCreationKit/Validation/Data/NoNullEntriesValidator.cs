using System.Collections;
using FastUnityCreationKit.Data.Attributes;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Validation.Abstract;
using FastUnityCreationKit.Validation.Data;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

[assembly: RegisterValidator(typeof(NoNullEntriesValidator.InternalValueValidator))]
[assembly: RegisterValidator(typeof(NoNullEntriesValidator.InternalRootObjectValidator))]
namespace FastUnityCreationKit.Validation.Data
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
                    Debug.Log($"Null item removed from the container at index {i}: {list[i]}");
                    list.RemoveAt(i);
                }
            }
        }
    }
}