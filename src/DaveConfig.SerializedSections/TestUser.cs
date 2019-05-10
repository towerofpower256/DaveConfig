using System;
using System.Collections.Generic;
using System.Text;

namespace DaveConfig.SerializedSections
{
    [Serializable]
    public class TestUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public TestUserTitle Title { get; set; }
    }

    public enum TestUserTitle
    {
        Mr,
        Mrs,
        Miss,
        Dr
    }
}
