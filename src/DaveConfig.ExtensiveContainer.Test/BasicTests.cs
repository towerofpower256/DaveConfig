using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaveConfig.ExtensiveContainer.Test
{
    [TestClass]
    public class BasicTests
    {
        public const string SETTINGS_FILENAME = "test.settings.xml";

        [TestMethod]
        public void BasicSetGet()
        {
            var settingsManager = new SettingsManager(
                new SettingsManagerOptions() { FileName = SETTINGS_FILENAME }
                );

            settingsManager.Settings.SectionA.IntOption = 5;
            settingsManager.Settings.SectionA.StringOption = "Test Value";
        }

        [TestMethod]
        public void TestSaveLoad()
        {
            var settingsManagerSave = new SettingsManager(
                new SettingsManagerOptions() { FileName = SETTINGS_FILENAME }
                );

            settingsManagerSave.Settings.SectionA.IntOption = 5;
            settingsManagerSave.Settings.SectionA.StringOption = "Test Value";

            settingsManagerSave.Settings.SectionB.TestList.Add("Test Value 1");
            settingsManagerSave.Settings.SectionB.TestList.Add("Test Value 2");
            settingsManagerSave.Settings.SectionB.TestList.Add("Test Value 3");

            settingsManagerSave.SaveSettings();

            var settingsManagerLoad = new SettingsManager(new SettingsManagerOptions() { FileName = SETTINGS_FILENAME });
            settingsManagerLoad.LoadSettings();
        }
    }
}
