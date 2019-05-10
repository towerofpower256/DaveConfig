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
            RootElementName = "Settings";
            FileName = "settings.xml";
        }
    }

    public class SettingsManager
    {
        private Dictionary<Type, XElement> _sections = new Dictionary<Type, XElement>();
        private object _lock = new object();
        private SettingsManagerOptions _options;

        public SettingsManager() : this(new SettingsManagerOptions())
        {

        }
        
        public SettingsManager(SettingsManagerOptions options)
        {
            _options = options;
        }

        public IEnumerable<Type> GetSections()
        {
            lock (_lock)
            {
                return new List<Type>(_sections.Keys).ToArray();
            }
        }

        public bool HasSection<TSection>() where TSection : ISettingsSection
        {
            lock (_lock)
            {
                return _sections.ContainsKey(typeof(TSection));
            }
        }

        public TSection GetSection<TSection>() where TSection : ISettingsSection
        {
            lock (_lock)
            {
                // Do we have that section?
                if (!_sections.ContainsKey(typeof(TSection)))
                {
                    // Handle unknown section
                }

                // Get it and deserialize it
                XElement serializedSection = _sections[typeof(TSection)];
                XmlSerializer serializer = new XmlSerializer(typeof(TSection));
                TSection section = (TSection)serializer.Deserialize(serializedSection.CreateReader());

                return section;
            }
        }

        public void SetSection<TSection>(TSection section) where TSection : ISettingsSection
        {
            //Serialize the section
            MemoryStream memStream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(TSection));
            serializer.Serialize(memStream, section);

            // Convert to XElement
            memStream.Position = 0; // Rewind the stream
            var xelement = XElement.Load(memStream);

            //Store it
            lock (_lock)
            {
                _sections[typeof(TSection)] = xelement;
            }
        }

        /// <summary>
        /// Export the settings sections as an XmlElement.
        /// </summary>
        /// <returns></returns>
        public XElement ExportSettings()
        {
            var r = new XElement(_options.RootElementName);

            lock (_lock)
            {
                foreach (var section in _sections)
                {
                    r.Add(section);
                }
            }

            return r;
        }

        public void ImportSettings(XElement serializedSettings)
        {
            lock (_lock)
            {
                //TODO how do I deserialize it into the correct types of objects?
            }
        }

        public void SaveSettings()
        {
            var exportedSettings = ExportSettings();

            using (var fs = File.Open(_options.FileName, FileMode.Create))
            {
                var writer = XmlWriter.Create(fs);
                exportedSettings.Save(writer);
            }
        }

        public void LoadSettings()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
