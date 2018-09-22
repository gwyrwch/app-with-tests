using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace test132132.Models
{

    public class Test : Collection<Question>
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public TimeMode? Mode { get; set; }

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
        }

        protected override void InsertItem(int index, Question item)
        {
            item.Test = this;
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            Items[index].Test = null;
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

        //public void Validate()
        //{
        //    if (
        //        string.IsNullOrEmpty(Title) ||
        //        string.IsNullOrEmpty(Subject) ||
        //        Mode == null ||
        //        TimeLimit == null
        //    )
        //        throw new NullReferenceException("Error! There are empty fields.");
        //}
    }
}
