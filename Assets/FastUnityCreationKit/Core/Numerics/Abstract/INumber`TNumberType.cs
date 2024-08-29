using System;
using Unity.Mathematics;

namespace FastUnityCreationKit.Core.Numerics.Abstract
{
    /// <summary>
    /// Represents a number with a base value of <typeparamref name="TNumberType"/>.
    /// Type <see cref="TNumberType"/> should be a C# representation of a number (primitive or custom structure
    /// like <see cref="float4x2"/>).
    /// For more information see <see cref="INumber"/>.
    /// </summary>
    public interface INumber<TNumberType> : IComparable<TNumberType>, IEquatable<TNumberType>
        where TNumberType : IComparable<TNumberType>, IEquatable<TNumberType>
    {
        /// <summary>
        /// Value of the number - this is the actual value of the number.
        /// </summary>
        public TNumberType Value { get; protected set; }

        /// <inheritdoc/>
        bool IEquatable<TNumberType>.Equals(TNumberType other) => Value.Equals(other);

        /// <inheritdoc cref="IComparable{TNumberType}.CompareTo"/>
        int IComparable<TNumberType>.CompareTo(TNumberType other) => Value.CompareTo(other);
    }
}