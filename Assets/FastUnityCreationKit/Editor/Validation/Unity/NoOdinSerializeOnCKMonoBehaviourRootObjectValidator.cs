using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Editor.Validation.Unity;
using FastUnityCreationKit.Unity;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor.Validation;
using Sirenix.Serialization;
using Sirenix.Utilities;

[assembly: RegisterValidator(typeof(NoOdinSerializeOnCKMonoBehaviourRootObjectValidator))]

namespace FastUnityCreationKit.Editor.Validation.Unity
{
    public sealed class NoOdinSerializeOnCKMonoBehaviourRootObjectValidator : RootObjectValidator<CKMonoBehaviour>
    {
        protected override void Validate([NotNull] ValidationResult result)
        {
            // Get type
            Type withType = Value.GetType();

            // Check if type has ANY OdinSerializeAttribute
            int count = PerformOdinSerializeValidation(result, withType);

            // Check if count is 0, if not, add error as OdinSerialize is not allowed on CKMonoBehaviour
            if (count == 0) return;
            string message =
                "CKMonoBehaviour should not have [OdinSerialize] attribute on any field or property " +
                $"(including nested ones). Found {count} invalid OdinSerialize attributes within " +
                $"{withType.GetNiceFullName()}";

            result.AddError(message);
            Guard<ValidationLogConfig>.Error(message);
        }

        private int PerformOdinSerializeValidation(
            [NotNull] ValidationResult result,
            [NotNull] Type type,
            [CanBeNull] List<Type> verifiedTypes = null)
        {
            // Initialize verified types
            verifiedTypes ??= new List<Type>();

            if (verifiedTypes.Any(vType => vType == type)) return 0;

            // Add current type to verified types
            verifiedTypes.Add(type);

            // Check if type is class or struct, otherwise we ignore it
            if (type is {IsClass: false, IsValueType: false}) return 0;

            // Add all base classes to verified types            
            Type bType = type.BaseType;
            while (bType != null)
            {
                // Add base type to verified types
                verifiedTypes.Add(bType);
                bType = bType.BaseType;
            }

            int counter = 0;

            // Check all fields
            foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                       BindingFlags.NonPublic))
            {
                // Check if field has OdinSerializeAttribute
                if (field.GetCustomAttributes(typeof(OdinSerializeAttribute), false).Length > 0)
                {
                    result.AddError(
                        $"Invalid [OdinSerialize] found in {field.GetNiceName()} field of {type.GetNiceFullName()} type.");
                    counter++;
                }

                // Check internal type
                counter += PerformOdinSerializeValidation(result, field.FieldType, verifiedTypes);
            }

            // Check all properties
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public |
                                                                 BindingFlags.NonPublic))
            {
                // Skip properties without setter as they are read-only
                // and attribute won't matter anyways
                if(property.SetMethod == null) continue;
                
                // Check if property has OdinSerializeAttribute
                if (property.GetCustomAttributes(typeof(OdinSerializeAttribute), false).Length > 0)
                {
                    result.AddError(
                        $"Invalid [OdinSerialize] found in {property.GetNiceName()} property of {type.GetNiceFullName()} type.");
                    counter++;
                }

                // Check property type
                counter += PerformOdinSerializeValidation(result, property.PropertyType, verifiedTypes);
            }

            // Return counter
            return counter;
        }
    }
}