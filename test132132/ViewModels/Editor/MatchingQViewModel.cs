using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using test132132.Common.Algorithm;

namespace test132132.ViewModels.Editor
{
    public class QPart
    {
        public string Text { get; set; }
    }

    public class MatchingQViewModel : BaseViewModel
    {
        public ObservableCollection<QPart> Lefts { get; set; }
        public ObservableCollection<QPart> Rights { get; set; }
        public ObservableCollection<int?> Relation { get; set; }
        public bool Shuffled { get; set; }

        public MatchingQViewModel()
        {
            Lefts = new ObservableCollection<QPart>();
            Rights = new ObservableCollection<QPart>();
            Relation = new ObservableCollection<int?>();
            Shuffled = false;
        }

        public void SetListLength(int length)
        {
            while (Lefts.Count > length)
            {
                Rights.RemoveAt(Lefts.Count - 1);
                Lefts.RemoveAt(Lefts.Count - 1);
                Relation.RemoveAt(Lefts.Count - 1);
            }

            while (Lefts.Count < length)
            {
                Lefts.Add(new QPart{ Text = "" });
                Rights.Add(new QPart { Text = "" });
                Relation.Add(Relation.Count);
            }
        }

        public void Shuffle()
        {
            long seed = DateTime.UtcNow.Ticks;
            Rights.Shuffle(
                new Random((int)seed)
            );
            Relation.Shuffle(
                new Random((int)seed)
            );
        }

        // todo refactor ->
        public List<string> LeftsAsList() 
        {
            List<string> result = new List<string>();
            foreach (var a in Lefts) {
                result.Add(a.Text);
            }
            return result;
        }

        public List<string> RightsAsList()
        {
            List<string> result = new List<string>();
            foreach (var a in Rights)
            {
                result.Add(a.Text);
            }
            return result;
        }
    }
}
