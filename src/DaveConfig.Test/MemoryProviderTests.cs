using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaveConfig.Test
{
    [TestClass]
    public class MemoryProviderTests
    {
        [TestMethod]
        public void BasicSetGetTest()
        {
            var provider = new MemoryConfigurationProvider();
            CommonTestRunner commonTests = new CommonTestRunner(provider);
            commonTests.RunBasicSetGetTest();
        }

        [TestMethod]
        public void BulkSetGetTest()
        {
            var provider = new MemoryConfigurationProvider();
            CommonTestRunner commonTests = new CommonTestRunner(provider);
            commonTests.RunBulkSetGetTest();
        }

        [TestMethod]
        public void ReadUnsetOptionsTest()
        {
            var provider = new MemoryConfigurationProvider();
            CommonTestRunner commonTests = new CommonTestRunner(provider);
            commonTests.RunReadUnsetOptionTest();
        }

        [TestMethod]
        public void ReadUnsetOptionsWithDefaultValueTest()
        {
            var provider = new MemoryConfigurationProvider();
            CommonTestRunner commonTests = new CommonTestRunner(provider);
            commonTests.RunReadUnsetOptionsWithDefaultValueTest();
        }
    }
}
