using System;
using System.Collections.Generic;
using System.Text;
using DaveConfig;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaveConfig.Test
{
    public class CommonTestRunner
    {
        IConfigurationProvider _config;

        public CommonTestRunner(IConfigurationProvider configProvider)
        {
            _config = configProvider;
        }


        /// <summary>
        /// Basic test for setting a property and then getting it back.
        /// </summary>
        public void RunBasicSetGetTest()
        {
            var optName = TestUtils.GetRandomString();
            var optValue = TestUtils.GetRandomString();
            _config.SetOption(optName, optValue);
            Assert.AreEqual(_config.GetOption(optName), optValue);
        }

        /// <summary>
        /// Test for setting a bunch of properties, and then pull them from the provider and check that they're all correct.
        /// </summary>
        public void RunBulkSetGetTest()
        {
            var testData = TestUtils.GenerateTestDataDictionary(50);

            // Set options
            foreach (var td in testData)
            {
                _config.SetOption(td.Key, td.Value);
            }

            // Check options
            foreach (var td in testData)
            {
                var valueFromProvider = _config.GetOption(td.Key);
                Assert.AreEqual(td.Value, valueFromProvider, $"Option validation failed. Option {td.Key} should be {td.Value}, provider returned {valueFromProvider}");
            }
        }

        /// <summary>
        /// Test for attempting to get an option that hasn't been set.
        /// </summary>
        public void RunReadUnsetOptionTest()
        {
            for (var i = 0; i < 50; i++)
            {
                var testOptionName = TestUtils.GetRandomString();
                Assert.IsInstanceOfType(_config.GetOption(testOptionName), typeof(string));
            }
        }

        /// <summary>
        /// Test retrieving an option with a default value. The provider should have no options set, and all get's should fall-back to the default value.
        /// </summary>
        public void RunReadUnsetOptionsWithDefaultValueTest()
        {
            var testData = TestUtils.GenerateTestDataDictionary(50);

            foreach (var td in testData)
            {
                var returnedValue = _config.GetOption(td.Key, td.Value);
                Assert.AreEqual(td.Value, returnedValue);
            }
        }
    }
}
