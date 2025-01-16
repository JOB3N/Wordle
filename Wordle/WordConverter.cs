using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wordle.Class;

namespace Wordle
{
    internal class WordConverter : JsonConverter<Word>
    {
        public override Word Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Создаем JSON-документ
            var jsonDoc = JsonDocument.ParseValue(ref reader);
            try
            {
                // Получаем тип объекта из JSON
                string type = jsonDoc.RootElement.GetProperty("category").GetString();

                switch (type)
                {
                    case "Животные":
                        return JsonSerializer.Deserialize<AnimalWord>(jsonDoc.RootElement.GetRawText(), options);
                    case "Фрукты":
                        return JsonSerializer.Deserialize<FruitWord>(jsonDoc.RootElement.GetRawText(), options);
                    case "Цвета":
                        return JsonSerializer.Deserialize<ColorWord>(jsonDoc.RootElement.GetRawText(), options);
                    case "Глаголы":
                        return JsonSerializer.Deserialize<VerbWord>(jsonDoc.RootElement.GetRawText(), options);
                    default:
                        throw new NotSupportedException($"Unknown type: {type}");
                }
            }
            finally
            {
                jsonDoc.Dispose();
            }
        }
        public override void Write(Utf8JsonWriter writer, Word value, JsonSerializerOptions options)
        {
            string type = value.GetType().Name;
            string json = JsonSerializer.Serialize(value, value.GetType(), options);
            var jsonDoc = JsonDocument.Parse(json);

            try
            {
                writer.WriteStartObject();
                writer.WriteString("category", type);

                foreach (var property in jsonDoc.RootElement.EnumerateObject())
                {
                    property.WriteTo(writer);
                }
                writer.WriteEndObject();
            }
            finally
            {
                jsonDoc.Dispose();
            }
        }
    }
}
