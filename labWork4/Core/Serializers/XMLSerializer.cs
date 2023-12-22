using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using labWork4.Models;

namespace labWork4.Core.Serializers
{
    public class XMLSerializer : ISerializer
    {
        private const string DEFAULT_SOURCE_PATH = "repository_XML.xml";
        private string _soursePath;
        private static XmlSerializer serializer = new XmlSerializer(typeof(List<Contact>));
        public XMLSerializer(string? sourcePath)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                this._soursePath = DEFAULT_SOURCE_PATH;
                return;
            }
            this._soursePath = sourcePath;
        }
        IList<Contact> ISerializer.Deserialize()
        {
            if (!File.Exists(_soursePath))
            {
                return new List<Contact>();
            }
            else
            {
                var xmlString = File.ReadAllText(_soursePath);
                if(string.IsNullOrEmpty(xmlString))
                {
                    return new List<Contact>();
                }
                using FileStream fs = new(_soursePath, FileMode.Open);
                return (List<Contact>)serializer.Deserialize(fs)!;
            }
        }

        void ISerializer.Serialize(IList<Contact> contacts)
        {
            using StreamWriter sw = new(_soursePath);
            serializer.Serialize(sw, contacts);
        }
    }
}
