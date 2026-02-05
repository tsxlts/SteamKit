using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SteamKit.Converter
{
    /// <summary>
    /// 
    /// </summary>
    public class BoolConverter : JsonConverter<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public BoolConverter()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="hasExistingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var stringValue = reader.Value?.ToString();
                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    return false;
                }

                if (new[] { "1", "true" }.Contains(stringValue, StringComparer.OrdinalIgnoreCase))
                {
                    return true;
                }
                if (new[] { "0", "false" }.Contains(stringValue, StringComparer.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            JToken? token = serializer.Deserialize<JToken>(reader);
            bool value = token?.Value<bool>() ?? false;
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}
