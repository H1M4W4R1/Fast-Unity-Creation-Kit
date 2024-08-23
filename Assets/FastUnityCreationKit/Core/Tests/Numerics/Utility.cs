using System;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Core.Numerics.Abstract.Operations;
using NUnit.Framework;

namespace FastUnityCreationKit.Core.Tests.Numerics
{
    public static class Utility
    {
        public static void Add_T_WorksCorrectly<TSource, TValue, TResult>(TValue value)
            where TSource : struct, INumber, ISupportsFloatConversion<TSource>
            where TValue : INumber
            where TResult : struct, INumber, ISupportsFloatConversion<TResult>
        {
            // Create source number
            TSource number = new TSource();
            number = number.FromFloat(10);

            // Ensure that the number supports addition operation
            if (number is not IAddOperationSupport<TValue, TResult> support)
            {
                Assert.Fail("The number does not support addition operation.");
                return;
            }

            // Perform addition operation
            TResult result = support.Add(value);

            // Convert to float and check the result
            float resultFloat = result.ToFloat();
            Assert.AreEqual(20f, resultFloat);
        }

        public static void Subtract_T_WorksCorrectly<TSource, TValue, TResult>(TValue value)
            where TSource : struct, INumber, ISupportsFloatConversion<TSource>
            where TValue : INumber
            where TResult : struct, INumber, ISupportsFloatConversion<TResult>
        {
            // Create source number
            TSource number = new TSource();
            number = number.FromFloat(10);
            
            // Ensure that the number supports subtraction operation
            if (number is not ISubtractOperationSupport<TValue, TResult> support)
            {
                Assert.Fail("The number does not support subtraction operation.");
                return;
            }
            
            // Perform subtraction operation
            TResult result = support.Subtract(value);
            
            // Convert to float and check the result
            float resultFloat = result.ToFloat();
            Assert.AreEqual(0f, resultFloat);
        }

        public static void Multiply_T_WorksCorrectly<TSource, TValue, TResult>(TValue value)
            where TSource : struct, INumber, ISupportsFloatConversion<TSource>
            where TValue : INumber
            where TResult : struct, INumber, ISupportsFloatConversion<TResult>
        {
            // Create source number
            TSource number = new TSource();
            number = number.FromFloat(10);
            
            // Ensure that the number supports multiplication operation
            if (number is not IMultiplyOperationSupport<TValue, TResult> support)
            {
                Assert.Fail("The number does not support multiplication operation.");
                return;
            }
            
            // Perform multiplication operation
            TResult result = support.Multiply(value);
            
            // Convert to float and check the result
            float resultFloat = result.ToFloat();
            Assert.AreEqual(100f, resultFloat);
        }

        public static void Divide_T_WorksCorrectly<TSource, TValue, TResult>(TValue value)
            where TSource : struct, INumber, ISupportsFloatConversion<TSource>
            where TValue : INumber
            where TResult : struct, INumber, ISupportsFloatConversion<TResult>
        {
            // Create source number
            TSource number = new TSource();
            number = number.FromFloat(10);
            
            // Ensure that the number supports division operation
            if (number is not IDivideOperationSupport<TValue, TResult> support)
            {
                Assert.Fail("The number does not support division operation.");
                return;
            }
            
            // Perform division operation
            TResult result = support.Divide(value);
            
            // Convert to float and check the result
            float resultFloat = result.ToFloat();
            Assert.AreEqual(1f, resultFloat);
        }
    }
}