using System;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace test132132.Models
{
    public class TestPreview
    {
        public ObservableCollection<Test> TestsToPreview {
            get; set;
        }
        
        public void LoadAll()
        {
            string testsPath;
            if (App.settings.Language == "English") {
                testsPath = "tempTests.json";
            } else {
                testsPath = "tempTestsRU.json";
            }
            string plain_text = File.ReadAllText(testsPath);
            TestsToPreview = JsonConvert.DeserializeObject<ObservableCollection<Test>>(plain_text);
        }

        public void SortByCountDescending() 
        {
            TestsToPreview = new ObservableCollection<Test>(TestsToPreview.OrderByDescending(test => test.Count));
        }

        public void SortByCount()
        {
            TestsToPreview = new ObservableCollection<Test>(TestsToPreview.OrderBy(test => test.Count));
        }

        public void SortByAlphabetDescending()
        {
            TestsToPreview = new ObservableCollection<Test>(TestsToPreview.OrderByDescending(test => test.Title));
        }

        public void SortByAlphabet()
        {
            TestsToPreview = new ObservableCollection<Test>(TestsToPreview.OrderBy(test => test.Title));
        }

        public void SearchByTitle(string strToSearch) 
        {
            TestsToPreview = new ObservableCollection<Test>(TestsToPreview.Where(test =>
            {
                return (
                    test.Title.ToLower().Contains(strToSearch.ToLower()) ||
                    test.Any(
                        question => question.Text.ToLower().Contains(strToSearch.ToLower())
                    )
                );
            }));
        }

        public int CountRequiredTime() 
        {
            return TestsToPreview.Select(test => {
                if (test.Mode == TimeMode.limitForQuestion)
                    return (int)test.TimeLimit.Value.TotalMinutes * test.Count;
                return (int)test.TimeLimit.Value.TotalMinutes;
            }).Sum();
        }

        public int CountUnansweredQuestions() 
        {
            return TestsToPreview.Select(test => {
                return test.Count;
            }).Sum();
        }

        public void SubjectsFilter(string subject) 
        {
            TestsToPreview = new ObservableCollection<Test>(TestsToPreview.Where(test => test.Subject == subject));
        }

        public void TimeFilter(int left, int right) 
        {
            TestsToPreview = new ObservableCollection<Test>(TestsToPreview.Where(test => {
                if (test.Mode == TimeMode.limitForQuestion)
                    return (
                        test.TimeLimit.Value.TotalMinutes * test.Count <= right &&
                        test.TimeLimit.Value.TotalMinutes * test.Count >= left
                    );

                return ( 
                    test.TimeLimit.Value.TotalMinutes <= right && 
                    test.TimeLimit.Value.TotalMinutes >= left
                );
            }));
        }

        public IEnumerable<IGrouping<string, Test>> SplitBySubject() {
            return TestsToPreview.GroupBy(test => test.Subject);
        }
    }
}
