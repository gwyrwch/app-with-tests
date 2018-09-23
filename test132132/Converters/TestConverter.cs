using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;
using System.Linq;

namespace test132132.Converters
{
    class TestSchema
    {
        public List<Models.Question> Collection { get; set; }

        public string Title { get; set; }
        public string Subject { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public Models.TimeMode? Mode { get; set; }
        public int Id { get; set; }
    }

    public class TestConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Models.Test);
        }

        public override object ReadJson(
            JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
                return null;
            TestSchema obj = serializer.Deserialize<TestSchema>(reader);
            List<Models.Question> questions = obj.Collection;

            Models.Test test = new Models.Test { 
                Title = obj.Title, 
                Subject = obj.Subject, 
                TimeLimit = obj.TimeLimit, 
                Mode = obj.Mode,
                Id = obj.Id
            };
            foreach (Models.Question q in questions)
                test.Add(q);
            return test;
        }

        public override void WriteJson(JsonWriter writer, object value,
                                       JsonSerializer serializer)
        {
            if (value == null)
                return;
            var test = (Models.Test)value;

            var obj = new TestSchema()
            {
                Collection = test.ToList(),
                Title = test.Title,
                Subject = test.Subject,
                TimeLimit = test.TimeLimit,
                Mode = test.Mode,
                Id = test.Id
            };
            serializer.Serialize(writer, obj);
        }
    }
}
