using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using Wordle.Class;

namespace Wordle
{
    public class JsonEnemySaver : ISaveList<List<Word>>
    {
        private readonly JsonSerializerOptions _options;

        public JsonEnemySaver()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new WordConverter() }
            };
        }

        public List<Word> Load(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);

                return JsonSerializer.Deserialize<List<Word>>(json, _options) ?? new List<Word>();
            }

            return new List<Word>();
        }

        public void Save(List<Word> data, string path)
        {
            string json = JsonSerializer.Serialize(data, _options);
            File.WriteAllText(path, json);
        }
    }
}
