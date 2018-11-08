using System;
using System.Collections.ObjectModel;

namespace test132132.ViewModels.Tests
{
    public class SubjectsViewModel
    {
        public ObservableCollection<Models.Test> tests;
        public ObservableCollection<string> subjects;
        public SubjectsViewModel()
        {
            tests = App.CreateSomeTestsTemp();
            subjects = App.Subjects;
        }
    }
}
