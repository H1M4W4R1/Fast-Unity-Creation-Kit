using System;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.Limits
{
    /// <summary>
    ///     Represents a maximum limit for a number.
    /// </summary>
    public interface IWithMaxLimit : ILimited
    {
        public double MaxLimit { get; }
    }
}