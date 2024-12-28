using System.Collections;
using FastUnityCreationKit.Data.Attributes;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Data.Validation;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(NoDuplicateEntriesAttributeValidator))]
namespace FastUnityCreationKit.Data.Validation
{
    public sealed class NoDuplicateEntriesAttributeValidator : AttributeValidator<NoDuplicatesAttribute, IDataContainer>
    {
        protected override void Validate(ValidationResult result)
        {
            IList list = Value.RawData;
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
                    list.RemoveAt(i);
            }
        }
        
    }
}