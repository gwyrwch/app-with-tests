using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

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
        }

        public void SetListLength(int length)
        {
            while (Lefts.Count > length)
            {
                Rights.RemoveAt(Lefts.Count - 1);
                Lefts.RemoveAt(Lefts.Count - 1);
            }

            while (Lefts.Count < length)
            {
                Lefts.Add("");
                Rights.Add("");
            }

            SaveRights();
            SaveLefts();
        }

        public void SaveLefts()
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Lefts"));
        }

        public void SaveRights()
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Rights")); //?
        }
    }
}
