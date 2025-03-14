﻿using System.Collections;
using FastUnityCreationKit.Annotations.Data;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Editor.Validation.Abstract;
using FastUnityCreationKit.Editor.Validation.Data;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor.Validation;
using Sirenix.Utilities;

[assembly:
    RegisterValidator(
        typeof(QuickAttributeBasedValidator<NoDuplicatesValidator, NoDuplicatesAttribute, IDataContainer>.
            InternalRootObjectValidator))]
[assembly:
    RegisterValidator(
        typeof(QuickAttributeBasedValidator<NoDuplicatesValidator, NoDuplicatesAttribute, IDataContainer>.
            InternalValueValidator))]

namespace FastUnityCreationKit.Editor.Validation.Data
{
    public sealed class NoDuplicatesValidator :
        QuickAttributeBasedValidator<NoDuplicatesValidator, NoDuplicatesAttribute, IDataContainer>
    {
        public override void Validate(ValidationResult result, [NotNull] IDataContainer value)
        {
            IList list = value.RawData;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                bool foundDuplicate = false;

                // Loop through remaining items to check for duplicates
                for (int j = i - 1; j >= 0; j--)
                {
                    // Skip if not duplicate, if found duplicate, break the loop
                    if (!list[i].Equals(list[j])) continue;
                    foundDuplicate = true;
                    break;
                }

                // Remove the duplicate item
                if (foundDuplicate)
                {
                    Guard<ValidationLogConfig>.Error(
                        $"Duplicate item found in {value.GetType().GetCompilableNiceFullName()}: {list[i]}");
                    list.RemoveAt(i);
                }
            }
        }
    }
}