using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using labWork4.Models;

namespace labWork4.Core.Serializers
{
    internal class JSONSerializer : ISerializer
    {
        private const string DEFAULT_SOURCE_PATH = "repository_JSON.json";
        private string _soursePath;
        public JSONSerializer(string? sourcePath)
        {
            if(string.IsNullOrEmpty(sourcePath))
            {
                this._soursePath = DEFAULT_SOURCE_PATH;
                return;
            }
            this._soursePath = sourcePath;
        }
        Task<List<Contact>> ISerializer.Deserialize()
        {
            return Task<List<Contact>>.Run(() =>
            {
                if (!File.Exists(_soursePath))
                {
                    return new List<Contact>();
                }
                else
                {
                    string jsonString = File.ReadAllText(_soursePath);
                    if (string.IsNullOrEmpty(jsonString))
                    {
                        return new List<Contact>();
                    }
                    return JsonSerializer.Deserialize<List<Contact>>(jsonString) ?? new List<Contact>();
                }
            });

        }

        Task ISerializer.Serialize(List<Contact> contacts)
        {
            return Task.Run(() =>
            {
                string jsonString = JsonSerializer.Serialize<IList<Contact>>(contacts);
                File.WriteAllText(_soursePath, jsonString);
            });
        }
    }
}
