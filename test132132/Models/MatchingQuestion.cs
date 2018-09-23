using System;
using System.Collections.Generic;
using System.Linq;

namespace test132132.Models
{
    public class MatchingQuestion : Question
    {
        public MatchingQuestion(
            string text,
            int? points,
            List<string> lefts,
            List<string> rights,
            List<int?> relation
        ) : base(text, points)
        {
            QType = 2;
            Lefts = lefts;
            Rights = rights;
            Relation = relation;
        }

        public List<string> Lefts { get; }
        public List<string> Rights { get; }
        public List<int?> Relation { get; }

        public override string ToString()
        {
            string _out = Text + "\n";
            for (int i = 0; i < Lefts.Count; i++) {
                _out += String.Join(
                    "",
                    (i + 1).ToString(),
                    ". ",
                    Lefts[i],
                    "    ",
                    (i + 1).ToString(),
                    ". ",
                    Rights[(int)(Relation[i] - 1)],
                    "\n"
                );
            }
            return _out;
        }

        public override void Validate()
        {
            base.Validate();

            if(Lefts.Any(l => string.IsNullOrWhiteSpace(l)) ||
               Rights.Any(r => string.IsNullOrWhiteSpace(r)) ||
               Relation.Any(digit => !digit.HasValue)
            )
               throw new NullReferenceException("Error! There are empty fields.");
        }
    }
}
