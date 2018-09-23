using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace test132132.Models
{
    [JsonConverter(typeof(Converters.TestConverter))]
    public class Test : Collection<Question>
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public TimeMode? Mode { get; set; }
        public int Id { get; set; }

        public Test(
            string title = null,
            string subject = null,
            TimeSpan? timeLimit = null,
            TimeMode? mode = null
        )
        {
            Title = title;
            Subject = subject;
            TimeLimit = timeLimit;
            Mode = mode;
            Id = new Random(Seed: (int)DateTime.UtcNow.Ticks).Next();
        }


        protected override void InsertItem(int index, Question item)
        {
            item.TestId = Id;
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            Items[index].TestId = null;
            base.RemoveItem(index);
        }

        public List<Question> SearchByType(Type type)
        {
            List<Question> list = new List<Question>();

            for (int i = 0; i < Count; i++)
                if (type == Items[i].GetType())
                    list.Add(Items[i]);

            return list;
        }
    }
}
