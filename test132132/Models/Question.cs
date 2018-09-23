using System;
using Newtonsoft.Json;

namespace test132132.Models
{
    [JsonConverter(typeof(Converters.QuestionConverter))]
    public abstract class Question
    {
        public int QType { get; set; }

        protected Question(string text, int? points)
        {
            Text = text;
            Points = points;
            TestId = null;
        }
        public string Text { get; }
        public int? Points { get; }
        public int? TestId { get; set; }

        public virtual void Validate() 
        {
            if (
                string.IsNullOrEmpty(Text) ||
                Points == null
            )
                throw new NullReferenceException("Error! There are empty fields");
        }
    }
}
