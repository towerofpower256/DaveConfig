using System;
using System.Collections.Generic;
using System.Text;

namespace DaveConfig.SerializedSections
{
    public class TestSettingsSectionA : BaseSettingsSection
    {
        public int IntSetting { get; set; }
        public string StringSetting { get; set; }
    }
}
