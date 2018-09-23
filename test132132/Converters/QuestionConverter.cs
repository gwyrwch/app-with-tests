using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace test132132.Converters
{
    public class QuestionSpecifiedConcreteClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Models.Question).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null;
            return base.ResolveContractConverter(objectType);
        }
    }
    public class QuestionConverter : JsonConverter
    {
        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings { 
            ContractResolver = new QuestionSpecifiedConcreteClassConverter() 
        };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Models.Question));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            switch (jo["QType"].Value<int>())
            {
                case 1:
                    return JsonConvert.DeserializeObject<Models.MultipleChoiceQuestion>(
                        jo.ToString(), SpecifiedSubclassConversion
                    );
                case 2:
                    return JsonConvert.DeserializeObject<Models.MatchingQuestion>(
                        jo.ToString(), SpecifiedSubclassConversion
                    );
                case 3:
                    return JsonConvert.DeserializeObject<Models.OpenQuestion>(
                        jo.ToString(), SpecifiedSubclassConversion
                    );
                default:
                    throw new Exception();
            }
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }
    }
}
