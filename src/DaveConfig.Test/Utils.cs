using System;
using System.Collections.Generic;
using System.Text;

namespace DaveConfig.Test
{
    public static class TestUtils
    {
        public static string[] GenerateTestList(int count)
        {
            var r = new string[count];
            for (var i = 0; i < count; i++)
            {
                r[i] = GetRandomString();
            }

            return r;
        }

        public static Dictionary<string, string> GenerateTestDataDictionary(int optionCount)
        {
            var r = new Dictionary<string, string>();
            r.EnsureCapacity(optionCount);

            for (int i = 0; i < optionCount; i++)
            {
                r.Add(GetRandomString(), GetRandomString());
            }

            return r;
        }

        public static Dictionary<string, string> FillProviderWithTestData(IConfigurationProvider configProvider, int optionCount)
        {
            var testData = GenerateTestDataDictionary(optionCount);

            foreach (var td in testData)
            {
                configProvider.SetOption(td.Key, td.Value);
            }

            return testData;
        }

        public static string GetRandomString()
        {
            return Guid.NewGuid().ToString("N"); // Use a random GUID
        }
    }
}
