using System;

namespace test132132.Models
{
    public class OpenQuestion : Question
    {
        public OpenQuestion(string text, int? points, string answer) : base(text, points)
        {
            QType = 3;
            Answer = answer;
        }
        public string Answer { get; }

        public override string ToString()
        {
            return Text + "\n";
        }

        public override void Validate() {
            base.Validate();
            if (string.IsNullOrEmpty(Answer)) 
                throw new NullReferenceException("Error! There are empty fields."); 
        }
    }
}
