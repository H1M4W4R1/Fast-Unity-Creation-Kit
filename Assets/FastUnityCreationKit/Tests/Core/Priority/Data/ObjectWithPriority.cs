using System;
using FastUnityCreationKit.Core.PrioritySystem.Abstract;

namespace FastUnityCreationKit.Tests.Core.Priority.Data
{
    public sealed class ObjectWithPriority : IPrioritySupport
    {
        public uint Priority { get; }

        public ObjectWithPriority(uint priority)
        {
            Priority = priority;
        }

        public int CompareTo(IPrioritySupport other) => Priority.CompareTo(other.Priority);
    }
}