using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;

namespace test132132.ViewModels.Editor
{
    public class EditorTestsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Models.Test> UserTests { get; set; }

        public EditorTestsViewModel()
        {
            //UserTests = new ObservableCollection<Models.Test>();
            string plain_text = File.ReadAllText(App.UserTestsPath);
            UserTests = JsonConvert.DeserializeObject<ObservableCollection<Models.Test> >(plain_text);
        }

        public void Add(Models.Test test) 
        {
            UserTests.Add(test);
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("UserTests"));
            File.WriteAllText(
                App.UserTestsPath,
                JsonConvert.SerializeObject(UserTests)
            );
        }
    }
}
