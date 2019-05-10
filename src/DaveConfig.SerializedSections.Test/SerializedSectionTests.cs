using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DaveConfig.SerializedSections.Test
{
    [TestClass]
    public class SerializedSectionTests
    {
        private SettingsManagerOptions ManagerOptions;

        public SerializedSectionTests()
        {
            ManagerOptions = new SettingsManagerOptions()
            {
                FileName = "test.settings.xml"
            };
        }

        [TestMethod]
        public void SimpleSet()
        {
            SettingsManager manager = new SettingsManager(ManagerOptions);

            var sectionA = new TestSettingsSectionA()
            {
                IntSetting = 5,
                StringSetting = "Test value"
            };

            var sectionB = new TestSettingsSectionB();
            sectionB.StringList.Add("Value 1");
            sectionB.StringList.Add("Value 2");
            sectionB.StringList.Add("Value 3");
            sectionB.UserList.Add(new TestUser()
            {
                Name = "Test user 1",
                Id = NewGuid(),
                Title = TestUserTitle.Mr
            });
            sectionB.UserList.Add(new TestUser()
            {
                Name = "Test user 2",
                Id = NewGuid(),
                Title = TestUserTitle.Dr
            });
            sectionB.UserList.Add(new TestUser()
            {
                Name = "Test user 3",
                Id = NewGuid(),
                Title = TestUserTitle.Miss
            });

            manager.SetSection(sectionA);
            manager.SetSection(sectionB);
        }

        [TestMethod]
        public void SaveAndLoadTest()
        {
            var newSection = new TestSettingsSectionB();
            newSection.UserList.Add(new TestUser()
            {
                Name = "Test user 1",
                Id = NewGuid(),
                Title = TestUserTitle.Mr
            });
            newSection.UserList.Add(new TestUser()
            {
                Name = "Test user 2",
                Id = NewGuid(),
                Title = TestUserTitle.Dr
            });
            newSection.UserList.Add(new TestUser()
            {
                Name = "Test user 3",
                Id = NewGuid(),
                Title = TestUserTitle.Miss
            });

            SettingsManager saveManager = new SettingsManager(ManagerOptions);
            saveManager.SetSection(newSection);

            saveManager.SaveSettings();

            SettingsManager loadManager = new SettingsManager(ManagerOptions);
            loadManager
        }

        public static string NewGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
