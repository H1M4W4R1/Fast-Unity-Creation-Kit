using FastUnityCreationKit.Core.Utility.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Core.Utility
{
    /// <summary>
    /// Represents that an object has an icon.
    /// </summary>
    public interface IWithIcon<[UsedImplicitly] TIconUsage>
        where TIconUsage : IIconUsage
    {
        /// <summary>
        /// Icon of the object.
        /// </summary>
        public Sprite Icon { get; }
    }
}