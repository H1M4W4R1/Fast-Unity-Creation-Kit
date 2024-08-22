using System;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Values.Abstract;
using UnityEngine;

namespace FastUnityCreationKit.Core.Values
{
    /// <summary>
    /// Represents a static value.
    /// Static value is a value that does not change during the game.
    ///
    /// For a value that can be modified during the game, see <see cref="ModifiableValue{TNumberType}"/>.
    /// </summary>
    [Serializable]
    public abstract class StaticValue<TNumberType> : IValue<TNumberType>
        where TNumberType : struct, INumber
    {
        /// <summary>
        /// Value of the static value.
        /// </summary>
        [SerializeField]
        private TNumberType value;

        /// <inheritdoc />
        public TNumberType CurrentValue { get; protected set; }
    }
}