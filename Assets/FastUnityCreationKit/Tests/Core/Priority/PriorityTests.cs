using System.Collections.Generic;
using FastUnityCreationKit.Core.PrioritySystem;
using FastUnityCreationKit.Core.PrioritySystem.Tools;
using FastUnityCreationKit.Tests.Core.Priority.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Priority
{
    [TestFixture]
    public sealed class PriorityTests 
    {
        [Test]
        public void PrioritizedList_SortsObjects_InCorrectOrder_WhenObject_IsInserted()
        {
            // Arrange
            PrioritizedList<ObjectWithPriority> list = new PrioritizedList<ObjectWithPriority>();
            ObjectWithPriority firstObject = new ObjectWithPriority(1);
            ObjectWithPriority secondObject = new ObjectWithPriority(0);
            
            // Act
            list.Add(firstObject);
            list.Add(secondObject);
            
            // Assert
            Assert.AreEqual(secondObject, list[0]);
            Assert.AreEqual(firstObject, list[1]);
        }
        
        [Test]
        public void PrioritizedList_SortsObjects_InCorrectOrder_WhenObject_IsInserted_WithMultipleObjects()
        {
            // Arrange
            PrioritizedList<ObjectWithPriority> list = new PrioritizedList<ObjectWithPriority>();
            ObjectWithPriority firstObject = new ObjectWithPriority(1);
            ObjectWithPriority secondObject = new ObjectWithPriority(0);
            ObjectWithPriority thirdObject = new ObjectWithPriority(2);
            
            // Act
            list.Add(firstObject);
            list.Add(secondObject);
            list.Add(thirdObject);
            
            // Assert
            Assert.AreEqual(secondObject, list[0]);
            Assert.AreEqual(firstObject, list[1]);
            Assert.AreEqual(thirdObject, list[2]);
        }
        
        [Test]
        public void PrioritizedList_SortsObjects_InCorrectOrder_WhenObject_IsInserted_WithMultipleObjects_WithEqualPriority()
        {
            // Arrange
            PrioritizedList<ObjectWithPriority> list = new PrioritizedList<ObjectWithPriority>();
            ObjectWithPriority firstObject = new ObjectWithPriority(1);
            ObjectWithPriority secondObject = new ObjectWithPriority(0);
            ObjectWithPriority thirdObject = new ObjectWithPriority(0);
            
            // Act
            list.Add(firstObject);
            list.Add(secondObject);
            list.Add(thirdObject);
            
            // Assert
            
            // Third object was inserted after the second object and thus
            // due to inserting behaviour it will end up being before the second object.
            Assert.AreEqual(thirdObject, list[0]); 
            Assert.AreEqual(secondObject, list[1]);
            Assert.AreEqual(firstObject, list[2]);
        }

        [Test]
        public void PrioritizedList_SortsObject_InReverseOrder_WhenAllObjects_HaveSamePriority()
        {
            // Arrange
            PrioritizedList<ObjectWithPriority> list = new PrioritizedList<ObjectWithPriority>();
            ObjectWithPriority firstObject = new ObjectWithPriority(0);
            ObjectWithPriority secondObject = new ObjectWithPriority(0);
            ObjectWithPriority thirdObject = new ObjectWithPriority(0);
            
            // Act
            list.Add(firstObject);
            list.Add(secondObject);
            list.Add(thirdObject);
            
            // Assert
            Assert.AreEqual(thirdObject, list[0]);
            Assert.AreEqual(secondObject, list[1]);
            Assert.AreEqual(firstObject, list[2]);
        }
        
        [Test]
        public void List_SortByPriority_SortsObjects_InCorrectOrder()
        {
            // Arrange
            List<ObjectWithPriority> list = new List<ObjectWithPriority>
            {
                new ObjectWithPriority(1),
                new ObjectWithPriority(0),
                new ObjectWithPriority(2)
            };
            
            // Act
            list.SortByPriority();
            
            // Assert
            Assert.AreEqual(0, list[0].Priority);
            Assert.AreEqual(1, list[1].Priority);
            Assert.AreEqual(2, list[2].Priority);
        }
        
        [Test]
        public void List_SortByPriority_DoesNotFail_WhenListIsEmpty()
        {
            // Arrange
            List<ObjectWithPriority> list = new List<ObjectWithPriority>();
            
            // Act
            list.SortByPriority();
            
            // Assert
            Assert.IsEmpty(list);
        }
        
        [Test]
        public void List_SortByPriority_DoesNotFail_WhenListHasOneObject()
        {
            // Arrange
            List<ObjectWithPriority> list = new List<ObjectWithPriority>
            {
                new ObjectWithPriority(1)
            };
            
            // Act
            list.SortByPriority();
            
            // Assert
            Assert.AreEqual(1, list[0].Priority);
        }
        
        [Test]
        public void List_SortByPriority_DoesNotFail_WhenListHasTwoObjects_WithEqualPriority()
        {
            // Arrange
            List<ObjectWithPriority> list = new List<ObjectWithPriority>
            {
                new ObjectWithPriority(1),
                new ObjectWithPriority(1)
            };
            
            // Act
            list.SortByPriority();
            
            // Assert
            Assert.AreEqual(1, list[0].Priority);
            Assert.AreEqual(1, list[1].Priority);
        }
        
        [Test]
        public void List_SortByPriority_SortsObjects_InCorrectOrder_WhenListHasManyObjects_AndSomeWithEqualPriority()
        {
            // Arrange
            List<ObjectWithPriority> list = new List<ObjectWithPriority>
            {
                new ObjectWithPriority(1),
                new ObjectWithPriority(0),
                new ObjectWithPriority(1)
            };
            
            // Act
            list.SortByPriority();
            
            // Assert
            Assert.AreEqual(0, list[0].Priority);
            Assert.AreEqual(1, list[1].Priority);
            Assert.AreEqual(1, list[2].Priority);
        }
    }
}