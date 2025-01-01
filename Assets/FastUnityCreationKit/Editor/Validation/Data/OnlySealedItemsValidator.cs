using System.Collections;
using FastUnityCreationKit.Annotations.Utility;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Editor.Validation.Abstract;
using FastUnityCreationKit.Editor.Validation.Data;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(OnlySealedItemsValidator.InternalValueValidator))]
[assembly: RegisterValidator(typeof(OnlySealedItemsValidator.InternalRootObjectValidator))]
namespace FastUnityCreationKit.Editor.Validation.Data
{
    public sealed class OnlySealedItemsValidator : QuickAttributeBasedValidator<
        OnlySealedItemsValidator, OnlySealedElementsAttribute, IDataContainer>
    {
        public override void Validate(ValidationResult result, IDataContainer value)
        {
            IList list = value.RawData;
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