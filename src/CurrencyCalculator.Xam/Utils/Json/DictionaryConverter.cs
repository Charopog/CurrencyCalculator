using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurrencyCalculator.Xam.Utils.Json
{
    public class DictionaryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IDictionary<string, string>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, 
            object existingValue, JsonSerializer serializer)
        {
            IDictionary<string, string> result;

            if (reader.TokenType == JsonToken.StartArray)
            {
                JArray legacyArray = (JArray)JArray.ReadFrom(reader);

                result = legacyArray.ToDictionary(
                    el => el["Key"].ToString(),
                    el => el["Value"].ToString());
            }
            else
            {
                result =
                    (IDictionary<string, string>)
                        serializer.Deserialize(reader, typeof(IDictionary<string, string>));
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, 
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get { return false; }
        }
    }
}
