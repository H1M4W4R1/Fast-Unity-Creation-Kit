﻿using System;

namespace FastUnityCreationKit.Annotations.Editor
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class |
                    AttributeTargets.Struct)]
    public sealed class NoLabelAttribute : Attribute
    {
    }
}