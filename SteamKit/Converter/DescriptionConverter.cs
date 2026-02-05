using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamKit.Game;
using SteamKit.Model;

namespace SteamKit.Converter
{
    /// <summary>
    /// 资产描述处理
    /// </summary>
    public class DescriptionConverter : JsonConverter
    {
        private static string[] tagAppIds = [AppId.CS2, AppId.Dota2, AppId.TF2];

        /// <summary>
        /// 
        /// </summary>
        public DescriptionConverter()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            bool result = typeof(IEnumerable<BaseDescription>).Equals(objectType);
            if (result)
            {
                return result;
            }

            result = typeof(BaseDescription).IsAssignableFrom(objectType);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (typeof(IEnumerable<BaseDescription>).Equals(objectType))
            {
                var descriptions = new List<BaseDescription>();

                JArray? array = serializer.Deserialize<JArray>(reader);
                if (array?.Count > 0)
                {
                    BaseDescription? description;
                    foreach (var item in array)
                    {
                        description = (BaseDescription?)ToDescription(item, typeof(BaseDescription));
                        if (description != null)
                        {
                            descriptions.Add(description);
                        }
                    }
                }

                return descriptions;
            }

            if (typeof(BaseDescription).IsAssignableFrom(objectType))
            {
                JToken? token = serializer.Deserialize<JToken>(reader);
                var value = ToDescription(token, objectType);
                return value;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        private object? ToDescription(JToken? token, Type objectType)
        {
            if (token == null)
            {
                return null;
            }

            if (typeof(BaseDescription).Equals(objectType))
            {
                BaseDescription? description;
                description = token.ToObject<BaseDescription>(JsonSerializer.Create(new JsonSerializerSettings { Converters = [new BoolConverter()] }))!;
                if (tagAppIds.Contains(description.AppId))
                {
                    return token.ToObject(typeof(TagDescription), JsonSerializer.Create(new JsonSerializerSettings { Converters = [new BoolConverter()] }))!;
                }
                return description;
            }

            return token.ToObject(objectType, JsonSerializer.Create(new JsonSerializerSettings { Converters = [new BoolConverter()] }))!;
        }
    }
}
