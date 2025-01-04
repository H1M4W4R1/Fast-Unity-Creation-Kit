using System;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Annotations.Utility
{
    public sealed class OrderAttribute : Attribute
    {
        public OrderAttribute(int order)
        {
            Order = order;
        }

        public int Order { [UsedImplicitly] get; }
    }
}