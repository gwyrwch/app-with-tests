using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using test132132.Common.Algorithm;

namespace test132132.ViewModels.Editor
{
    public class MatchingQViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Lefts { get; set; }
        public ObservableCollection<string> Rights { get; set; }
        public ObservableCollection<int?> Relation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MatchingQViewModel()
        {
            Lefts = new ObservableCollection<string>();
            Rights = new ObservableCollection<string>();
            Relation = new ObservableCollection<int?>();
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
                Lefts.Add("");
                Rights.Add("");
                Relation.Add(Relation.Count);
            }

            Save("Rights");
            Save("Lefts");
        }

        public void Save(string arg)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(arg));
        }

        public void Shuffle()
        {
            Save("Rights");
            Save("Lefts");
            long seed = DateTime.UtcNow.Ticks;
            Rights.Shuffle(
                new Random((int)seed)
            );
            Relation.Shuffle(
                new Random((int)seed)
            );
            Save("Rights");
        }
    }
}
