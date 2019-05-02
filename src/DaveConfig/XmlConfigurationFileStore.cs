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
            _serializer = new XmlSerializer(typeof(SerializableOptionsCollection));
        }

        public OptionsCollection LoadConfiguration()
        {
            if (!File.Exists(_configFilename))
                return new OptionsCollection();

            using (var f = File.Open(_configFilename, FileMode.Open))
            {
                var config = _serializer.Deserialize(f);

                return ((SerializableOptionsCollection)config).ToOptionsCollection();
            }
        }

        public void SaveConfiguration(OptionsCollection options)
        {
            var serializableOptions = new SerializableOptionsCollection(options);

            using (var f = File.Open(_configFilename, FileMode.Create))
            {
                _serializer.Serialize(f, serializableOptions);
            }
        }
    }
}
