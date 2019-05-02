using System;
using System.Xml.Serialization;

namespace DaveConfig
{
    [Serializable]
    public class SerializableOption
    {
        public SerializableOption() { }

        public SerializableOption(string name, string value)
        {
            OptionName = name;
            OptionValue = value;
        }

        [XmlAttribute("name")]
        public string OptionName { get; set; }

        [XmlAttribute("value")]
        public string OptionValue { get; set; }
    }
}