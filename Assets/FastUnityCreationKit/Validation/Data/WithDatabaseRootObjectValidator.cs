using System.Collections;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Validation.Data;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

[assembly: RegisterValidator(typeof(WithDatabaseRootObjectValidator))]
namespace FastUnityCreationKit.Validation.Data
{
    public sealed class WithDatabaseRootObjectValidator : RootObjectValidator<ScriptableObject>
    {
        protected override void Validate(ValidationResult result)
        {
            // Check if value type is correct
            if (Value is not IWithDatabase withDatabase) return;
            
            // Check if database is set
            if (withDatabase.RawDatabase == null)
            {
                result.AddError($"Database is not set. Something went wrong.");
                return;
            }
                
            // Get list of all items
            IList databaseContent = withDatabase.RawDatabase.RawData;
                
            // If item is not in database, add it 
            if (!databaseContent.Contains(Value))
            {
                databaseContent.Add(Value);
                Debug.Log($"Added {Value.name} to database {withDatabase.RawDatabase}.");
            }
        }
        
    }
}