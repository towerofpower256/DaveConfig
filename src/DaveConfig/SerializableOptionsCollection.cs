using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DaveConfig
{
    [Serializable]
    [XmlRoot("configuration")]
    public class SerializableOptionsCollection
    {
        [XmlArray("options"), XmlArrayItem(ElementName = "option", Type = typeof(SerializableOption))]
        public List<SerializableOption> SerializableOptions;

        public SerializableOptionsCollection()
        {
            SerializableOptions = new List<SerializableOption>();
        }

        public SerializableOptionsCollection(OptionsCollection options)
        {
            SerializableOptions = new List<SerializableOption>();
            SerializableOptions.Capacity = options.Count;

            foreach (var opt in options)
            {
                SerializableOptions.Add(new SerializableOption(opt.Key, opt.Value));
            }
        }

        public OptionsCollection ToOptionsCollection()
        {
            var r = new OptionsCollection();

            foreach (var opt in SerializableOptions)
            {
                r.Add(opt.OptionName, opt.OptionValue);
            }

            return r;
        }
    }
}
