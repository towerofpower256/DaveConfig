using System;
using System.Collections.Generic;
using System.Text;

namespace DaveConfig.SerializedSections
{
    public class TestSettingsSectionB : BaseSettingsSection
    {
        public List<string> StringList { get; set; } = new List<string>();
        public List<TestUser> UserList { get; set; } = new List<TestUser>();
    }
}
