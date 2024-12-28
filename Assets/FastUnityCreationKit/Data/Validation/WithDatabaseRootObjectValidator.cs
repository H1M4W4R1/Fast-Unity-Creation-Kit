using System.Collections;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Data.Validation;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

[assembly: RegisterValidator(typeof(WithDatabaseRootObjectValidator))]
namespace FastUnityCreationKit.Data.Validation
{
    public sealed class WithDatabaseRootObjectValidator : RootObjectValidator<ScriptableObject>
    {
        protected override void Validate(ValidationResult result)
        { 
            if (Value is IWithDatabase withDatabase)
            {
                // Check if database is set
                if (withDatabase.RawDatabase == null)
                {
                    result.AddError($"Database is not set. Something went wrong.");
                    return;
                }
                
                // Get list of all items
                IList databaseContent = withDatabase.RawDatabase.RawData;
                if (!databaseContent.Contains(Value))
                {
                    result.AddError($"Object is not in the database. Please add it to the database.")
                        .WithFix(() =>
                        {
                            // TODO: Check if this works properly
                            databaseContent.Add(Value);
                        });
                }
            }
        }
        
    }
}