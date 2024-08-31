// ReSharper disable file ConditionIsAlwaysTrueOrFalse
// ReSharper disable file ConvertTypeCheckToNullCheck

using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Utility.Initialization;
using FastUnityCreationKit.Core.Values.Abstract;
using FastUnityCreationKit.Tests.Core.Values.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Values
{
    [TestFixture]
    public class ModifiableValueTests
    {
        
        [Test]
        public void Implements_IInitializable()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            
            // Act

            bool actual = testValue is IInitializable;
            
            // Assert
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void Implements_IModifiableValue()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            
            // Act
            bool actual = testValue is IModifiableValue<float32>;
            
            // Assert
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void Implements_IValue()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            
            // Act
            bool actual = testValue is IValue<float32>;
            
            // Assert
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void Initialize_Initializes()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            
            // Act
            IInitializable testValueAsInitializable = testValue;
            testValueAsInitializable.Initialize();
            
            // Assert
            Assert.IsTrue(testValueAsInitializable.IsInitialized);
        }
        
        [Test]
        public void EnsureInitialized_Initializes()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            
            // Act
            IInitializable testValueAsInitializable = testValue;
            testValueAsInitializable.EnsureInitialized();
            
            // Assert
            Assert.IsTrue(testValueAsInitializable.IsInitialized);
        }
        
        [Test]
        public void Add_Adds()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetCurrentValue(0);
            float32 amount = 5;
            float32 expected = 5;
            
            // Act
            testValue.Add(amount);
            float32 actual = testValue.CurrentValue;
            IInitializable testValueAsInitializable = testValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(testValueAsInitializable.IsInitialized);
        }
        
        [Test]
        public void Subtract_Subtracts()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetCurrentValue(0);
            float32 amount = 5;
            float32 expected = -5;
            
            // Act
            testValue.Subtract(amount);
            float32 actual = testValue.CurrentValue;
            IInitializable testValueAsInitializable = testValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(testValueAsInitializable.IsInitialized);
        }
        
        [Test]
        public void Multiply_Multiplies()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetCurrentValue(5);
            float32 amount = 5;
            float32 expected = 25;
            
            // Act
            testValue.Multiply(amount);
            float32 actual = testValue.CurrentValue;
            IInitializable testValueAsInitializable = testValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(testValueAsInitializable.IsInitialized);
        }
        
        [Test]
        public void Divide_Divides()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetCurrentValue(5);
            float32 amount = 5;
            float32 expected = 1;
            
            // Act
            testValue.Divide(amount);
            float32 actual = testValue.CurrentValue;
            IInitializable testValueAsInitializable = testValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(testValueAsInitializable.IsInitialized);
        }
        
        [Test]
        public void SetCurrentValue_Sets()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetCurrentValue(5);
            float32 expected = 5;
            
            // Act
            float32 actual = testValue.CurrentValue;
            IInitializable testValueAsInitializable = testValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(testValueAsInitializable.IsInitialized);
        }
        
        [Test]
        public void SetBaseValue_Sets()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(5);
            float32 expected = 5;
            
            // Act
            float32 actual = testValue.CurrentValue;
            IInitializable testValueAsInitializable = testValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(testValueAsInitializable.IsInitialized);
        }
        
        [Test]
        public void ApplyModifier_Applies()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(5);
            float32 amount = 5;
            float32 expected = 10;
            
            // Act
            testValue.ApplyModifier(new TestFlatAddModifier(amount));
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
      
        [Test]
        public void RemoveModifier_Removes()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            TestFlatAddModifier modifier = new TestFlatAddModifier(5);
            
            testValue.SetBaseValue(10);
            testValue.ApplyModifier(modifier);
            float32 expected = 10;
            
            // Act
            testValue.RemoveModifier(modifier);
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Apply_Flat_Modifier_WorksCorrectly()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            float32 amount = 5;
            float32 expected = 15;
            
            // Act
            testValue.ApplyModifier(new TestFlatAddModifier(amount));
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Apply_Multiply_Modifier_WorksCorrectly()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            float32 amount = 5;
            float32 expected = 50;
            
            // Act
            testValue.ApplyModifier(new TestMultiplyModifier(amount));
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Apply_Add_Modifier_WorksCorrectly()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            float32 amount = 5;
            float32 expected = 15;
            
            // Act
            testValue.ApplyModifier(new TestAddModifier(amount));
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Apply_Add_Flat_MultipleTimes_WorksCorrectly()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            float32 amount = 5;
            float32 expected = 20;
            
            // Act
            testValue.ApplyModifier(new TestFlatAddModifier(amount));
            testValue.ApplyModifier(new TestFlatAddModifier(amount));
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Apply_Multiply_MultipleTimes_WorksCorrectly()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            
            float32 amount = 5;
            float32 expected = 250;
            
            // Act
            testValue.ApplyModifier(new TestMultiplyModifier(amount));
            testValue.ApplyModifier(new TestMultiplyModifier(amount));
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Apply_Add_MultipleTimes_WorksCorrectly()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            
            float32 amount = 5;
            float32 expected = 20;
            
            // Act
            testValue.ApplyModifier(new TestAddModifier(amount));
            testValue.ApplyModifier(new TestAddModifier(amount));
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Remove_Modifier_RemovesOnlyOne_IfDifferentReferences()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            
            TestFlatAddModifier modifier = new TestFlatAddModifier(5);
            
            testValue.ApplyModifier(modifier);
            testValue.ApplyModifier(new TestFlatAddModifier(5));
            
            float32 expected = 15;
            
            // Act
            testValue.RemoveModifier(modifier);
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Remove_Modifier_RemovesAll_IfMultipleSameModifiersAdded()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            
            TestFlatAddModifier modifier = new TestFlatAddModifier(5);
            
            testValue.ApplyModifier(modifier);
            testValue.ApplyModifier(modifier);
            
            float32 expected = 10;
            
            // Act
            testValue.RemoveModifier(modifier);
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void FlatAdd_And_Multiply_Together_WorksCorrectly()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            
            float32 amount = 5;
            float32 expected = 75;
            
            // Act
            testValue.ApplyModifier(new TestMultiplyModifier(amount));
            testValue.ApplyModifier(new TestFlatAddModifier(amount));
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void FlatAdd_And_Add_Together_WorksCorrectly()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            
            float32 amount = 5;
            float32 expected = 20;
            
            // Act
            testValue.ApplyModifier(new TestAddModifier(amount));
            testValue.ApplyModifier(new TestFlatAddModifier(amount));
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Multiply_And_Add_Together_WorksCorrectly()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            
            float32 amount = 5;
            float32 expected = 55;
            
            // Act
            testValue.ApplyModifier(new TestAddModifier(amount));
            testValue.ApplyModifier(new TestMultiplyModifier(amount));
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void All_Modifiers_Together_WorksCorrectly()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            
            float32 amount = 5;
            float32 expected = 80;
            
            // Act
            testValue.ApplyModifier(new TestAddModifier(amount));
            testValue.ApplyModifier(new TestMultiplyModifier(amount));
            testValue.ApplyModifier(new TestFlatAddModifier(amount));
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void SetCurrentValue_IsOverriden_ByModifiers()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetCurrentValue(10);
            
            float32 amount = 5;
            float32 expected = 5;
            
            // Act
            testValue.ApplyModifier(new TestAddModifier(amount));
            float32 actual = testValue.CurrentValue;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemovableModifiers_AreChecked_WhenGettingCurrentValue()
        {
            // Arrange
            TestModifiableValue testValue = new TestModifiableValue();
            testValue.SetBaseValue(10);
            
            float32 amount = 5;
            float32 expected0 = 20;
            float32 expected1 = 15;

            var removableModifier = new TestRemovableModifier(amount);
            
            // Act
            testValue.ApplyModifier(removableModifier);
            testValue.ApplyModifier(new TestFlatAddModifier(amount));
            
            float32 actual0 = testValue.CurrentValue;
            
            // Assert that both modifiers are applied
            Assert.AreEqual(expected0, actual0);
            
            // Mark modifier to be removed
            removableModifier.toRemove = true;
            float32 actual1 = testValue.CurrentValue;
            
            // Assert that modifier was automatically removed
            Assert.AreEqual(expected1, actual1);
        }
    }
}