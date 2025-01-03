using System.Collections;
using System.Collections.Generic;
using FastUnityCreationKit.Editor.Validation.Utility;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Extensions;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(ItemNotNullAttributeValidator))]
namespace FastUnityCreationKit.Editor.Validation.Utility
{
    public sealed class ItemNotNullAttributeValidator : AttributeValidator<ItemNotNullAttribute, object>
    {
        protected override void Validate(ValidationResult result)
        {
            // Built-in validation for list, automatically removes null items
            if (Value is IList list)
            {
                for(int n = 0; n < list.Count; n++)
                {
                    if (list[n].IsNull())
                        list.RemoveAt(n);
                }
                
                return;
            }
            
            // Built-in validation for dictionary, automatically removes null items
            if (Value is IDictionary dictionary)
            {
                List<object> keysToRemove = new List<object>();
                
                foreach (object key in dictionary.Keys)
                {
                    // If item at key is null, remove it
                    if (dictionary[key].IsNull())
                        keysToRemove.Add(key);
                }
                
                // We need to stash keys to prevent exceptions
                foreach (object key in keysToRemove)
                    dictionary.Remove(key);
                
                return;
            }
            
            if (Value is IEnumerable)
            {
                foreach (object item in (IEnumerable) Value)
                {
                    // Skip if item is not null
                    if (!item.IsNull()) continue;
                    
                    result.AddError("This collection cannot contain null items.");
                    return;
                }
            }
        }
        
    }
}