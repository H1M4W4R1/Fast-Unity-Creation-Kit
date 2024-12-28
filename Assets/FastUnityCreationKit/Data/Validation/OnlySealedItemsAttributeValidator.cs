using System.Collections;
using FastUnityCreationKit.Data.Attributes;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Data.Validation;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(OnlySealedItemsAttributeValidator))]
namespace FastUnityCreationKit.Data.Validation
{
    public sealed class OnlySealedItemsAttributeValidator : AttributeValidator<OnlySealedElementsAttribute, IDataContainer>
    {
        protected override void Validate(ValidationResult result)
        {
            IList list = Value.RawData;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                // Skip if the type is sealed
                if (list[i].GetType().IsSealed) continue;
                
                // Copy value to avoid closure issues, which may cause the index to be incorrect
                // when compiler improves code performance
                int index = i;
                
                // Add error if the type is not sealed
                result.AddError($"Type [{list[i].GetType()}] is not sealed. It cannot be stored in the container.")
                    .WithFix(() => list.RemoveAt(index));
            }            
        }
    }
    
}