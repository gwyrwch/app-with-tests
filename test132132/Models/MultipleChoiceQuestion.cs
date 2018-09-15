using System.Collections.Generic;

namespace test132132.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion(
            string text,
            int points,
            List<Variant> variants
        ) : base(text, points)
        {
            Variants = variants;
        }
        public List<Variant> Variants { get; }

        public override string ToString()
        {
            string _out = "";
            for (int i = 0; i < Variants.Count; i++)
                _out += Variants[i] + "\t";

            return string.Join("\n", Text, _out);
        }
    }
}
