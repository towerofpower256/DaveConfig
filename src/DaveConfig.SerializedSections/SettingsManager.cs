using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace DaveConfig.SerializedSections
{
    public class SettingsManager
    {
        private Dictionary<Type, XElement> _sections = new Dictionary<Type, XElement>();
        private object _lock = new object();


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

        public void SaveSection<TSection>(TSection section) where TSection : ISettingsSection
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


    }
}
