using System;
using System.Linq;
using System.Collections.Generic;

namespace test132132.ViewModels.TestSolving
{
    public class MatchingQSolveViewModel: Editor.MatchingQViewModel
    {
        public List<Tuple<int, int> > Pairs { get; set; }

        public MatchingQSolveViewModel(): base()
        {
            Pairs = new List<Tuple<int, int>>();
        }

        public void AddPair(int left, int right)
        {
            Pairs.RemoveAll((item) => {
                return item.Item1 == left || item.Item2 == right;
            });

            Pairs.Add(
                (left, right).ToTuple()
            );
        }

        List<int> GenerateRelation()
        {
            if (Pairs.Count < Lefts.Count)
                return null;
            Pairs.Sort((a, b) => a.Item1 < b.Item1 ? -1 : 1);
            return Pairs.Select((a) => a.Item2).ToList();
        }

        public bool CorrectAnswer()
        {
            var currentAnswer = GenerateRelation();
            if (currentAnswer == null)
                return false;
            return currentAnswer.Cast<int?>().SequenceEqual(Relation);
        }
    }
}
