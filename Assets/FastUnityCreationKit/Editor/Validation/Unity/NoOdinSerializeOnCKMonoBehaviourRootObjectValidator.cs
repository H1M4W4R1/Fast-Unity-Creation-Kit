using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastUnityCreationKit.Core.Extensions;
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
        [CanBeNull] private ValidationResult _currentResult;

        protected override void Validate([NotNull] ValidationResult result)
        {
            _currentResult = result;

            // Get type
            Type withType = Value.GetType();

            int count = withType.PerformCascadeSearch(CheckOdinSerializeAttribute,
                AddErrorWhenAttributeWasFound);

            // Check if count is 0, if not, add error as OdinSerialize is not allowed on CKMonoBehaviour
            if (count == 0) return;
            string message =
                "CKMonoBehaviour should not have [OdinSerialize] attribute on any field or property " +
                $"(including nested ones). Found {count} invalid OdinSerialize attributes within " +
                $"{withType.GetNiceFullName()}";

            result.AddError(message);

            _currentResult = null;
        }

        private bool CheckOdinSerializeAttribute([NotNull] MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(typeof(OdinSerializeAttribute), false).Length != 0;
        }

        private void AddErrorWhenAttributeWasFound([NotNull] MemberInfo memberInfo)
        {
            if (_currentResult == null)
            {
                Guard<ValidationLogConfig>.Error("Current result is null, this should not happen.");
                return;
            }
            
            // Check if member is property and has no setter
            // those properties wouldn't be serialized anyway, so we can skip them
            if(memberInfo is PropertyInfo { SetMethod: null }) return;

            if(memberInfo.DeclaringType == null) return;
            _currentResult.AddError(
                $"Non-allowed [OdinSerialize] found in {memberInfo.GetNiceName()} of {memberInfo.DeclaringType.GetNiceFullName()} type.");
        }
    }
}