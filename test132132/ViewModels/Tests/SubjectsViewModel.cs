using System;
using System.Collections.ObjectModel;

namespace test132132.ViewModels.Tests
{
    public class SubjectsViewModel : BaseViewModel
    {
        //public ObservableCollection<Models.Test> tests;
        //public ObservableCollection<string> subjects;
        public Models.TestPreview testPreview { get; set; };
        public SubjectsViewModel()
        {
            testPreview = new Models.TestPreview();
            testPreview.LoadAll();


            //tests = App.CreateSomeTestsTemp();
            //subjects = App.Subjects;
        }
    }
}
