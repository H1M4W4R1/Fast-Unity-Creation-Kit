using System;
using FastUnityCreationKit.Core.Numerics.Types;
using Unity.Mathematics;

namespace FastUnityCreationKit.Core.Numerics.Abstract
{
    /// <summary>
    /// This interface represents a number.
    ///
    /// It supports basic arithmetic operations, however numbers must support float conversion as
    /// arithmetic operations converts numbers to double, then performs operation and at the end converts
    /// double back to number.
    /// </summary>
    public interface INumber
    {
        public bool IsFloatingPointNumber => this is IFloatingPointNumber;
        public bool IsSignedNumber => this is ISignedNumber;
        public bool IsUnsignedNumber => this is IUnsignedNumber;
        
        public static INumber operator +(INumber lhs, INumber rhs)
        {
            // Check if first and second numbers can be converted to double, if not throw exception
            if (lhs is not ISupportsFloatConversion lhsFloat || rhs is not ISupportsFloatConversion rhsFloat)
                throw new NotSupportedException("Numbers must support float conversion to perform operation.");
            
            // Convert both numbers to double
            double lhsValue = lhsFloat.ToDouble();
            double rhsValue = rhsFloat.ToDouble();
                
            // Return new number with sum of two numbers
            return lhsFloat.NumberFromDouble(lhsValue + rhsValue);
        }
        
        public static INumber operator -(INumber lhs, INumber rhs)
        {
            // Check if first and second numbers can be converted to double, if not throw exception
            if (lhs is not ISupportsFloatConversion lhsFloat || rhs is not ISupportsFloatConversion rhsFloat)
                throw new NotSupportedException("Numbers must support float conversion to perform operation.");
            
            // Convert both numbers to double
            double lhsValue = lhsFloat.ToDouble();
            double rhsValue = rhsFloat.ToDouble();
                
            // Return new number with difference of two numbers
            return lhsFloat.NumberFromDouble(lhsValue - rhsValue);
        }
        
        public static INumber operator *(INumber lhs, INumber rhs)
        {
            // Check if first and second numbers can be converted to double, if not throw exception
            if (lhs is not ISupportsFloatConversion lhsFloat || rhs is not ISupportsFloatConversion rhsFloat)
                throw new NotSupportedException("Numbers must support float conversion to perform operation.");
            
            // Convert both numbers to double
            double lhsValue = lhsFloat.ToDouble();
            double rhsValue = rhsFloat.ToDouble();
                
            // Return new number with product of two numbers
            return lhsFloat.NumberFromDouble(lhsValue * rhsValue);
        }
        
        public static INumber operator /(INumber lhs, INumber rhs)
        {
            // Check if first and second numbers can be converted to double, if not throw exception
            if (lhs is not ISupportsFloatConversion lhsFloat || rhs is not ISupportsFloatConversion rhsFloat)
                throw new NotSupportedException("Numbers must support float conversion to perform operation.");
            
            // Convert both numbers to double
            double lhsValue = lhsFloat.ToDouble();
            double rhsValue = rhsFloat.ToDouble();
                
            // Return new number with quotient of two numbers
            return lhsFloat.NumberFromDouble(lhsValue / rhsValue);
        }

        public static INumber operator %(INumber lhs, INumber rhs)
        {
            // Check if first and second numbers can be converted to double, if not throw exception
            if (lhs is not ISupportsFloatConversion lhsFloat || rhs is not ISupportsFloatConversion rhsFloat)
                throw new NotSupportedException("Numbers must support float conversion to perform operation.");

            // Convert both numbers to double
            double lhsValue = lhsFloat.ToDouble();
            double rhsValue = rhsFloat.ToDouble();

            // Return new number with remainder of two numbers
            return lhsFloat.NumberFromDouble(lhsValue % rhsValue);
        }
        
        /// <summary>
        /// Converts number to specified number type.
        /// Must support float conversion.
        /// </summary>
        public static TNumberType As<TNumberType>(INumber number)
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            // Create new number
            TNumberType newNumber = default;
            
            // Check if number can be converted to double, if not throw exception
            if (number is not ISupportsFloatConversion numberFloat)
                throw new NotSupportedException("Number must support float conversion to perform operation.");
            
            // Convert number to double
            double numberValue = numberFloat.ToDouble();
            
            // Return new number with value of input number
            return newNumber.FromDouble(numberValue);
        }
        
        /// <summary>
        /// Internal method to check equality of two numbers.
        /// </summary>
        internal static bool CheckEquality<TNumber>(TNumber self, object other)
            where TNumber : struct, INumber, ISupportsFloatConversion<TNumber>
        {
            // Get self value
            double selfValue = self.ToDouble();
            
            // Check if other is INumber
            if (other is INumber otherNumber)
            {
                // Check if other number can be converted to double, if not return false
                if (otherNumber is not ISupportsFloatConversion otherFloat)
                    return self.Equals(other);
                
                // Get other value
                double otherValue = otherFloat.ToDouble();
                
                // Return equality of two numbers
                return selfValue.Equals(otherValue);
            }
            
            // Check remaining types
            return other switch
            {
                byte otherByte => math.abs(selfValue - otherByte) < math.EPSILON,
                sbyte otherSByte => math.abs(selfValue - otherSByte) < math.EPSILON,
                ushort otherUShort => math.abs(selfValue - otherUShort) < math.EPSILON,
                short otherShort => math.abs(selfValue - otherShort) < math.EPSILON,
                uint otherUInt => math.abs(selfValue - otherUInt) < math.EPSILON,
                int otherInt => math.abs(selfValue - otherInt) < math.EPSILON,
                ulong otherULong => math.abs(selfValue - otherULong) < math.EPSILON,
                long otherLong => math.abs(selfValue - otherLong) < math.EPSILON,
                float otherFloat => math.abs(selfValue - otherFloat) < math.EPSILON,
                double otherDouble => math.abs(selfValue - otherDouble) < math.EPSILON, // This is just in case
                _ => self.Equals(other)
            };
        }
    }
}