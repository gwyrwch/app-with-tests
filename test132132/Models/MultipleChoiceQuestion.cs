using System;
using System.Collections.Generic;
using System.Linq;

namespace test132132.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion(
            string text,
            int? points,
            List<Variant> variants
        ) : base(text, points)
        {
            QType = 1;
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

        public override void Validate()
        {
            base.Validate();
            if (Variants.Any(var => string.IsNullOrWhiteSpace(var.Answer)))
                throw new NullReferenceException("Error! There are empty fields.");

            if (
                Variants.All(var => var.IsTrue == false) ||
                Variants.All(var => var.IsTrue == true)
            )
                throw new Exception("All answers can not be either correct or incorrect");
        }
    }
}
