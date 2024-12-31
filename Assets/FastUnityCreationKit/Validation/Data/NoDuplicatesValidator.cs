using System.Collections;
using FastUnityCreationKit.Data.Attributes;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Logging;
using FastUnityCreationKit.Validation.Abstract;
using FastUnityCreationKit.Validation.Data;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(NoDuplicatesValidator.InternalRootObjectValidator))]
[assembly: RegisterValidator(typeof(NoDuplicatesValidator.InternalValueValidator))]
namespace FastUnityCreationKit.Validation.Data
{
    public sealed class NoDuplicatesValidator :
        QuickAttributeBasedValidator<NoDuplicatesValidator, NoDuplicatesAttribute, IDataContainer>
    {
        public override void Validate(ValidationResult result, IDataContainer value)
        {
            IList list = value.RawData;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                bool foundDuplicate = false;

                // Loop through remaining items to check for duplicates
                for (int j = i - 1; j >= 0; j--)
                {
                    // Skip if not duplicate, if found duplicate, break the loop
                    if (!list[i].Equals(list[j])) continue;
                    foundDuplicate = true;
                    break;
                }

                // Remove the duplicate item
                if (foundDuplicate)
                {
                    Guard<ValidationLogConfig>.Error($"Duplicate item found in {value.GetType().Name}: {list[i]}");
                    list.RemoveAt(i);
                }
            }
        }
    }
}