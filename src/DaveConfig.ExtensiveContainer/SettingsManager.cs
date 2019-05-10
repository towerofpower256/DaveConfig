using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

namespace DaveConfig.ExtensiveContainer
{
    public class SettingsManagerOptions
    {
        public string FileName { get; set; } = "settings.xml";
    }

    public class SettingsManager
    {
        private XmlSerializer _serializer = new XmlSerializer(typeof(SettingsContainer));

        private object _lock;
        private SettingsManagerOptions _options;
        private SettingsContainer _settings;

        public SettingsContainer Settings
        {
            get { lock (_lock) return _settings; }
            set { lock (_lock) _settings = value; }
        }

        public SettingsManager(SettingsManagerOptions options)
        {
            _lock = new object();
            _options = options;
            _settings = new SettingsContainer();
        }

        public void SaveSettings()
        {
            lock (_lock)
            {
                using (var fs = File.Open(_options.FileName, FileMode.Create))
                {
                    _serializer.Serialize(fs, Settings);
                }
            }
        }

        public void LoadSettings()
        {
            lock (_lock)
            {
                using (var fs = File.Open(_options.FileName, FileMode.Open))
                {
                    Settings = (SettingsContainer)_serializer.Deserialize(fs);
                }
            }
        }
    }
}
