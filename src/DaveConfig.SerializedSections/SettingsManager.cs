using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace DaveConfig.SerializedSections
{
    public class SettingsManagerOptions
    {
        public string RootElementName { get; set; }
        public string FileName { get; set; }

        public SettingsManagerOptions()
        {
            // Default settings
            RootElementName = "Settings";
            FileName = "settings.xml";
        }
    }

    public class SettingsManager
    {
        private Dictionary<string, XElement> _sections = new Dictionary<string, XElement>();
        private object _lock = new object();
        private SettingsManagerOptions _options;

        public SettingsManager() : this(new SettingsManagerOptions())
        {

        }
        
        public SettingsManager(SettingsManagerOptions options)
        {
            _options = options;
        }

        public IEnumerable<string> GetSections()
        {
            lock (_lock)
            {
                return new List<string>(_sections.Keys).ToArray();
            }
        }

        public bool HasSection(string desiredSection)
        {
            lock (_lock)
            {
                return _sections.ContainsKey(desiredSection);
            }
        }

        public TSection GetSection<TSection>(string sectionName) where TSection : ISettingsSection
        {
            lock (_lock)
            {
                // Do we have that section?
                if (!_sections.ContainsKey(sectionName))
                {
                    // Handle unknown section
                }

                // Get it and deserialize it
                XElement serializedSection = _sections[sectionName];
                XmlSerializer serializer = new XmlSerializer(typeof(TSection));
                TSection section = (TSection)serializer.Deserialize(serializedSection.CreateReader());

                return section;
            }
        }

        public void SetSection<TSection>(TSection section) where TSection : ISettingsSection
        {
            SetSection(section.GetSectionName(), section);
        }

        public void SetSection<TSection>(string sectionName, TSection section) where TSection : ISettingsSection
        {
            //Serialize the section
            MemoryStream memStream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(TSection));

            // Set some writer settings
            var xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.OmitXmlDeclaration = true; // Prevent namespace declarations

            var xmlWriter = XmlWriter.Create(memStream, xmlWriterSettings);

            serializer.Serialize(xmlWriter, section);

            // Convert to XElement
            memStream.Position = 0; // Rewind the stream
            var xelement = XElement.Load(memStream);

            SetSection(sectionName, xelement);
        }

        public void SetSection(string sectionName, XElement serializedSection)
        {
            //Store it
            lock (_lock)
            {
                _sections[sectionName] = serializedSection;
            }
        }

        /// <summary>
        /// Export the settings sections as an XmlElement.
        /// </summary>
        /// <returns></returns>
        public XElement ExportSettings()
        {
            var sections = new List<XElement>();

            lock (_lock)
            {
                foreach (var section in _sections)
                {
                    XElement newElement = new XElement("Section", (XElement)section.Value);
                    newElement.SetAttributeValue("name", section.Key);
                    sections.Add(newElement);
                }
            }

            var r = new XElement(_options.RootElementName, sections);
            return r;
        }

        public void ImportSettings(IEnumerable<XElement> elements)
        {
            foreach(var element in elements)
            {
                //string key = element.Name.LocalName;
                string key = (string)element.Attribute("name");
                SetSection(key, new XElement((XElement)element.FirstNode));
            }
            
        }

        public void SaveSettings()
        {
            var exportedSettings = ExportSettings();

            
            new XDocument(exportedSettings).Save(_options.FileName);
        }

        public void LoadSettings()
        {
            XDocument doc = XDocument.Load(_options.FileName);
            ImportSettings(doc.Root.Elements());
        }
    }
}
