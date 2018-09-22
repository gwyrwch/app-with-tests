using System;

namespace test132132.Models
{
    public class OpenQuestion : Question
    {
        public OpenQuestion(string text, int? points, string answer) : base(text, points)
        {
            Answer = answer;
        }
        public string Answer { get; }

        public override string ToString()
        {
            return Text + "\n";
        }

        public override void Validate() {
            if (
                string.IsNullOrEmpty(Text) ||
                Points == null ||
                string.IsNullOrEmpty(Answer) 
               ) 
                throw new NullReferenceException("Error! There are empty fields."); 
        }
    }
}
