using NUnit.Framework;
using UnityEngine;

namespace FastUnityCreationKit.Tests
{
    public class TestFixtureBase
    {
        [SetUp]
        public void BaseSetUp()
        {
            Debug.unityLogger.logEnabled = false;
        }
        
        [TearDown]
        public void BaseTearDown()
        {
            Debug.unityLogger.logEnabled = true;
        }
        
    }
}