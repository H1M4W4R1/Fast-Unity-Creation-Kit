using System;
using Sirenix.OdinInspector.Editor.Validation;
using Sirenix.Utilities;
using UnityEngine;

namespace FastUnityCreationKit.Editor.Validation.Abstract
{
    public abstract class QuickAttributeBasedValidator<TSelf, TAttribute, TValueType>
        where TSelf : QuickAttributeBasedValidator<TSelf, TAttribute, TValueType>, new()
        where TAttribute : Attribute
    {
        public abstract void Validate(ValidationResult result, TValueType value);

        /// <summary>
        ///     This internal class performs validation on root object that is of type <see cref="ScriptableObject" />.
        ///     And that is of type has the attribute <see cref="TAttribute" /> attached to it.
        /// </summary>
        public sealed class InternalRootObjectValidator : RootObjectValidator<ScriptableObject>
        {
            protected override void Validate(ValidationResult result)
            {
                if (Value is not TValueType value) return;
                if (Value.GetType().GetCustomAttribute<TAttribute>(true) == null) return;

                new TSelf().Validate(result, value);
            }
        }

        public sealed class InternalValueValidator : ValueValidator<TValueType>
        {
            protected override void Validate(ValidationResult result)
            {
                // Skip if value is null
                if (Value == null) return;
                if (Value.GetType().GetCustomAttribute<TAttribute>(true) == null) return;

                new TSelf().Validate(result, Value);
            }
        }
    }
}