using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace DaveConfig
{
    public class XmlConfigurationFileStore : IConfigurationFileStore
    {
        string _configFilename;
        XmlSerializer _serializer;

        public XmlConfigurationFileStore(string configFilename)
        {
            _configFilename = configFilename;
            _serializer = new XmlSerializer(typeof(OptionsCollection));
        }

        public OptionsCollection LoadConfiguration()
        {
            using (var f = File.Open(_configFilename, FileMode.Open))
            {
                var config = _serializer.Deserialize(f);

                return (OptionsCollection)config;
            }
        }

        public void SaveConfiguration(OptionsCollection options)
        {
            using (var f = File.Open(_configFilename, FileMode.Create))
            {
                _serializer.Serialize(f, options);
            }
        }
    }
}
