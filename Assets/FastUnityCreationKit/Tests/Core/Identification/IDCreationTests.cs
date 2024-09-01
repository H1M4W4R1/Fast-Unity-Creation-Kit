using System;
using FastUnityCreationKit.Core.Identification;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Identification
{
    [TestFixture]
    public class IDCreationTests : TestFixtureBase
    {
        [Test]
        public void ID8_IsCreated_Correctly()
        {
            // Act
            ID8 id = new ID8(1);
            
            // Assert
            Assert.IsTrue(id.IsCreated);
            Assert.AreEqual(1, id.value);
        }
        
        [Test]
        public void ID16_IsCreated_Correctly()
        {
            // Act
            ID16 id = new ID16(1);
            
            // Assert
            Assert.IsTrue(id.IsCreated);
            Assert.AreEqual(1, id.value);
        }
        
        [Test]
        public void ID32_IsCreated_Correctly()
        {
            // Act
            ID32 id = new ID32(1);
            
            // Assert
            Assert.IsTrue(id.IsCreated);
            Assert.AreEqual(1, id.value);
        }
        
        [Test]
        public void ID64_IsCreated_Correctly()
        {
            // Act
            ID64 id = new ID64(1);
            
            // Assert
            Assert.IsTrue(id.IsCreated);
            Assert.AreEqual(1, id.value);
        }
        
        [Test]
        public void SnowflakeID_IsCreated_Correctly()
        {
            long ticks = DateTime.UtcNow.Ticks;
            
            // Act
            Snowflake128 id = new Snowflake128(ticks, 32, 16);
            
            // Assert
            Assert.IsTrue(id.IsCreated);
            Assert.AreEqual(ticks, id.timestamp);
            Assert.AreEqual(32, id.identifierData);
            Assert.AreEqual(16, id.additionalData);
            Assert.AreEqual(0, id.reserved);
        }
        
        [Test]
        public void Default_ID8_IsNotCreated()
        {
            // Act
            ID8 id = default;
            
            // Assert
            Assert.IsFalse(id.IsCreated);
            Assert.AreEqual(0, id.value);
        }
        
        [Test]
        public void Default_ID16_IsNotCreated()
        {
            // Act
            ID16 id = default;
            
            // Assert
            Assert.IsFalse(id.IsCreated);
            Assert.AreEqual(0, id.value);
        }
        
        [Test]
        public void Default_ID32_IsNotCreated()
        {
            // Act
            ID32 id = default;
            
            // Assert
            Assert.IsFalse(id.IsCreated);
            Assert.AreEqual(0, id.value);
        }
        
        [Test]
        public void Default_ID64_IsNotCreated()
        {
            // Act
            ID64 id = default;
            
            // Assert
            Assert.IsFalse(id.IsCreated);
            Assert.AreEqual(0, id.value);
        }
        
        [Test]
        public void Default_SnowflakeID_IsNotCreated()
        {
            // Act
            Snowflake128 id = default;
            
            // Assert
            Assert.IsFalse(id.IsCreated);
            Assert.AreEqual(0, id.timestamp);
            Assert.AreEqual(0, id.identifierData);
            Assert.AreEqual(0, id.additionalData);
            Assert.AreEqual(0, id.reserved);
        }
        
    }
}