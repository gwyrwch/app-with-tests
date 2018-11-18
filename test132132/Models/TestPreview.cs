using System;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace test132132.Models
{
    public class TestPreview
    {
        public ObservableCollection<Test> TestsToPreview {
            get; set;
        }

        public void LoadAll()
        {
            //string plain_text = File.ReadAllText(App.userTestsPath); // todo
            string plain_text = File.ReadAllText("tempTests.json");
            TestsToPreview = JsonConvert.DeserializeObject<ObservableCollection<Test>>(plain_text);
        }

        void SortByCount() 
        {
            TestsToPreview = (ObservableCollection<Test>)TestsToPreview.OrderBy(test => test.Count);
        }

        void SortByAlphabet()
        {
            TestsToPreview = (ObservableCollection<Test>)TestsToPreview.OrderBy(test => test.Title);
        }

        void SearchByTitle(string strToSearch)
        {
            TestsToPreview = (ObservableCollection<Test>)TestsToPreview.Where(test => {
                return (
                    test.Title.ToLower().Contains(strToSearch.ToLower()) ||
                    test.Any(
                        question => question.Text.ToLower().Contains(strToSearch.ToLower())
                    )
                );
            });
        }

        int CountRequiredTime()
        {
            return TestsToPreview.Select(test => {
                if (test.Mode == TimeMode.limitForQuestion)
                    return (int)test.TimeLimit.Value.TotalMinutes * test.Count;
                return (int)test.TimeLimit.Value.TotalMinutes;
            }).Sum();
        }

        int CountUnansweredQuestions()
        {
            return TestsToPreview.Select(test => {
                return test.Count;
            }).Sum();
        }

        void SubjectsFilter(string subject)
        {
            TestsToPreview = (ObservableCollection<Test>)TestsToPreview.Where(test => test.Subject == subject);
        }

        void TimeFilter(int left, int right)
        {
            TestsToPreview = (ObservableCollection<Test>)TestsToPreview.Where(test => {
                if (test.Mode == TimeMode.limitForQuestion)
                    return (
                        test.TimeLimit.Value.TotalMinutes * test.Count < right &&
                        test.TimeLimit.Value.TotalMinutes * test.Count > left
                    );

                return ( 
                    test.TimeLimit.Value.TotalMinutes < right && 
                    test.TimeLimit.Value.TotalMinutes > left
                );
            });
        }

        IEnumerable<IGrouping<string, Test>> SplitBySubject() {
            return TestsToPreview.GroupBy(test => test.Subject);
        }
    }
}
