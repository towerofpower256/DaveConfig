using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaveConfig.Test
{
    [TestClass]
    public class ConfigurationFileStoreTests
    {
        public const string CONFIG_FILE_NAME = "test.config.xml";

        [TestMethod]
        public void BasicSetGetTest()
        {
            var provider = new FileConfigurationProvider(CONFIG_FILE_NAME);
            CommonTestRunner commonTests = new CommonTestRunner(provider);
            commonTests.RunBasicSetGetTest();
        }

        [TestMethod]
        public void BulkSetGetTest()
        {
            var provider = new FileConfigurationProvider(CONFIG_FILE_NAME);
            CommonTestRunner commonTests = new CommonTestRunner(provider);
            commonTests.RunBulkSetGetTest();
        }

        [TestMethod]
        public void ReadUnsetOptionsTest()
        {
            var provider = new FileConfigurationProvider(CONFIG_FILE_NAME);
            CommonTestRunner commonTests = new CommonTestRunner(provider);
            commonTests.RunReadUnsetOptionTest();
        }

        [TestMethod]
        public void ReadUnsetOptionsWithDefaultValueTest()
        {
            var provider = new FileConfigurationProvider(CONFIG_FILE_NAME);
            CommonTestRunner commonTests = new CommonTestRunner(provider);
            commonTests.RunReadUnsetOptionsWithDefaultValueTest();
        }

        [TestMethod]
        public void SaveOptionsToFileTest()
        {
            var provider = new FileConfigurationProvider(CONFIG_FILE_NAME);

            // Fill it with random information
            TestUtils.FillProviderWithTestData(provider, 50);

            // Save
            provider.SaveConfiguration();
        }

        [TestMethod]
        public void SaveOptionsToFileThenLoadTest()
        {
            var writeProvider = new FileConfigurationProvider(CONFIG_FILE_NAME);

            // Fill it with random information
            var testData = TestUtils.FillProviderWithTestData(writeProvider, 50);

            // Save
            writeProvider.SaveConfiguration();

            // Read and validate
            var readProvider = new FileConfigurationProvider(CONFIG_FILE_NAME);
            readProvider.LoadConfiguration();

            foreach (var td in testData)
            {
                var expectedValue = td.Value;
                var actualValue = readProvider.GetOption(td.Key);
                Assert.AreEqual(expectedValue, actualValue);
            }
        }
    }
}
