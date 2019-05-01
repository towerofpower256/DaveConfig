using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DaveConfig
{
    [Serializable]
    [XmlRoot("options")]
    public class OptionsCollection : Dictionary<string, string>
    {
    }
}
