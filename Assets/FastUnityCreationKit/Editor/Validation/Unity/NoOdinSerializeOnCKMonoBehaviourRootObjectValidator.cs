using System;
using System.Reflection;
using FastUnityCreationKit.Editor.Validation.Unity;
using FastUnityCreationKit.Unity;
using Sirenix.OdinInspector.Editor.Validation;
using Sirenix.Serialization;

[assembly: RegisterValidator(typeof(NoOdinSerializeOnCKMonoBehaviourRootObjectValidator))]

namespace FastUnityCreationKit.Editor.Validation.Unity
{
    public sealed class NoOdinSerializeOnCKMonoBehaviourRootObjectValidator : RootObjectValidator<CKMonoBehaviour>
    {
        protected override void Validate(ValidationResult result)
        {
            // Get type
            Type withType = Value.GetType();

            // Check if type has any [OdinSerialize] fields
            foreach (FieldInfo field in withType.GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                           BindingFlags.NonPublic))
            {
                if (field.GetCustomAttributes(typeof(OdinSerializeAttribute), false).Length <=
                    0)
                    continue;
                result.AddError("CKMonoBehaviour cannot have any [OdinSerialize] fields.");
                return;
            }

            // Check if type has any [OdinSerialize] properties
            foreach (PropertyInfo property in withType.GetProperties(BindingFlags.Instance | BindingFlags.Public |
                                                                     BindingFlags.NonPublic))
            {
                if (property.GetCustomAttributes(typeof(OdinSerializeAttribute), false).Length <=
                    0)
                    continue;
                result.AddError("CKMonoBehaviour cannot have any [OdinSerialize] properties.");
                return;
            }
        }
    }
}