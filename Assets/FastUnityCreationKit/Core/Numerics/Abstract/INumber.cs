using System;
using FastUnityCreationKit.Core.Numerics.Types;

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
    }
}