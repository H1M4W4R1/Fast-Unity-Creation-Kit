using System.Reflection;
using FastUnityCreationKit.Annotations.Utility;
using FastUnityCreationKit.Editor.Validation.Utility;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using Sirenix.Utilities;

[assembly: RegisterValidator(typeof(RequiresOdinSerializationAttributeValidator))]
namespace FastUnityCreationKit.Editor.Validation.Utility
{
    /// <summary>
    /// This validator checks if value requires Odin serialization.
    /// May not work properly for backing fields.
    /// </summary>
    public sealed class RequiresOdinSerializationAttributeValidator : ValueValidator<object>
    {
        protected override void Validate(ValidationResult result)
        {
            // Check if value requires Odin serialization
            if (ValueEntry.TypeOfValue.GetCustomAttribute<RequiresOdinSerializationAttribute>() == null) return;
            
            // Check if type has Unity serialization attribute
            if (Property.GetAttribute<SerializeField>() != null)
            {
                result.AddError(
                    $"This value requires Odin serialization. Found {Property.Info.PropertyName} with type " +
                    $"{ValueEntry.TypeOfValue.GetNiceFullName()} which is serialized by Unity. " +
                    $"Please make sure that the type is serialized by Odin Serializer.");
                return;
            }
            
            // Check backing members
            MemberInfo[] backingMembers = Property.Info.GetMemberInfos();
            foreach (MemberInfo member in backingMembers)
            {
                // Check if member has Unity serialization attribute
                if (member.GetCustomAttribute<SerializeField>() == null) continue;
                
                result.AddError(
                    $"This value requires Odin serialization. Found {Property.Info.PropertyName} with backing field " +
                    $"{member.Name} with type {ValueEntry.TypeOfValue.GetNiceFullName()} which is serialized by Unity. " +
                    $"Please make sure that the type is serialized by Odin Serializer.");
                return;
            }

        }
    }
}