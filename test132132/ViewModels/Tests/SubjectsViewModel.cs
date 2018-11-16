using System;
using System.Collections.ObjectModel;

namespace test132132.ViewModels.Tests
{
    public class SubjectsViewModel
    {
        //public ObservableCollection<Models.Test> tests;
        //public ObservableCollection<string> subjects;
        Models.TestPreview testPreview;
        public SubjectsViewModel()
        {
            testPreview = new Models.TestPreview();
            testPreview.LoadAll();


            //tests = App.CreateSomeTestsTemp();
            //subjects = App.Subjects;
        }
    }
}
