using System;
using FastUnityCreationKit.Core.Identification;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Identification
{
    [TestFixture]
    public class IDComparisonTests
    {
        
        [Test]
        public void ID8_Comparison_WorksCorrectly()
        {
            // Act
            ID8 id1 = new ID8(1);
            ID8 id2 = new ID8(1);
            ID8 id3 = new ID8(2);
            
            // Assert
            Assert.IsTrue(id1.Equals(id2));
            Assert.IsFalse(id1.Equals(id3));
        }
        
        [Test]
        public void ID16_Comparison_WorksCorrectly()
        {
            // Act
            ID16 id1 = new ID16(1);
            ID16 id2 = new ID16(1);
            ID16 id3 = new ID16(2);
            
            // Assert
            Assert.IsTrue(id1.Equals(id2));
            Assert.IsFalse(id1.Equals(id3));
        }
        
        [Test]
        public void ID32_Comparison_WorksCorrectly()
        {
            // Act
            ID32 id1 = new ID32(1);
            ID32 id2 = new ID32(1);
            ID32 id3 = new ID32(2);
            
            // Assert
            Assert.IsTrue(id1.Equals(id2));
            Assert.IsFalse(id1.Equals(id3));
        }
        
        [Test]
        public void ID64_Comparison_WorksCorrectly()
        {
            // Act
            ID64 id1 = new ID64(1);
            ID64 id2 = new ID64(1);
            ID64 id3 = new ID64(2);
            
            // Assert
            Assert.IsTrue(id1.Equals(id2));
            Assert.IsFalse(id1.Equals(id3));
        }
        
        [Test]
        public void SnowflakeID_Comparison_WorksCorrectly()
        {
            long ticks = DateTime.UtcNow.Ticks;
            
            // Act
            Snowflake128 id1 = new Snowflake128(ticks, 32, 16);
            Snowflake128 id2 = new Snowflake128(ticks, 32, 16);
            Snowflake128 id3 = new Snowflake128(ticks, 16, 32);
            
            // Assert
            Assert.IsTrue(id1.Equals(id2));
            Assert.IsFalse(id1.Equals(id3));
        }
        
    }
}