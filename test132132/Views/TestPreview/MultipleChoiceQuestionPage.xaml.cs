using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using test132132.Models;
using Xamarin.Forms;
using System.Linq;

namespace test132132.Views.TestPreview
{
    public partial class MultipleChoiceQuestionPage : ContentPage
    {
        Test test;
        int curQ;
        Models.TestSolving.TestResults testResults;
        DateTime startTime;
        TimeSpan limit;
        bool answered;

        ViewModels.Editor.MultipleChoiceQViewModel vm;

        public MultipleChoiceQuestionPage(Test test, int currentQuestion, Models.TestSolving.TestResults testResults)
        {
            this.testResults = testResults;
            this.test = test;
            this.curQ = currentQuestion;
            startTime = DateTime.Now;
            answered = false;
        
            InitializeComponent();

            var clonedList = ((Models.MultipleChoiceQuestion)test[curQ]).Variants.Select(objEntity => (Variant)objEntity.Clone()).ToList();

            var Variants = new ObservableCollection<Variant>(clonedList);
           
            foreach (var variant in Variants)
                variant.IsTrue = false;
            BindingContext = vm = new ViewModels.Editor.MultipleChoiceQViewModel { Variants = Variants };

            QuestionLabel.Text = string.Join("",
                (curQ + 1).ToString(),
                ". ",
                test[curQ].Text
            );

            limit = test.TimeLimit.Value;
            if (test.Mode == Models.TimeMode.limitForQuestion)
            {
                TimeLeftSlider.Minimum = 0;
                TimeLeftSlider.Maximum = limit.TotalSeconds;
                TimeLeftSlider.Value = limit.TotalSeconds;

                Device.StartTimer(
                    TimeSpan.FromSeconds(0.1),
                    () => {
                        if (answered)
                            return false;
                        if (PassedTime() >= limit)
                        {
                            TimeExpired();
                            return false;
                        }

                        TimeLeftSlider.Value = (limit - PassedTime()).TotalSeconds;
                        return true;
                    }
                );
            }
            else
            {
                TimeLeftSlider.Minimum = 0;
                TimeLeftSlider.Maximum = limit.TotalSeconds;
                TimeLeftSlider.Value = (limit - testResults.UsedTime).TotalSeconds;

                Device.StartTimer(
                    TimeSpan.FromSeconds(0.1),
                    () =>
                    {
                        if (answered)
                            return false;
                        if (PassedTime() + testResults.UsedTime >= limit)
                        {
                            TestTimeExpired();
                            return false;
                        }

                        TimeLeftSlider.Value = (limit - testResults.UsedTime - PassedTime()).TotalSeconds;
                        return true;
                    }
                );
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            AnswersListView.SelectedItem = null;
        }

        TimeSpan PassedTime()
        {
            return DateTime.Now - startTime;
        }

        void TimeExpired()
        {
            testResults.Update(test[curQ], limit, false);
            NextQuestion();
        }

        async void TestTimeExpired()
        {
            testResults.UsedTime = limit;
            Page nextPage = new ResultsPage(
                test, testResults
            );
            NavigationPage.SetHasBackButton(nextPage, false);
            await Navigation.PushAsync(nextPage);
        }

        public void Answer_Clicked(object sender, EventArgs e)
        {
            if (vm.Variants.All((var) => var.IsTrue) || vm.Variants.All((var)=> !var.IsTrue))
                return;

            answered = true;

            if (((MultipleChoiceQuestion)test[curQ]).Variants.SequenceEqual(vm.Variants))
                testResults.Update(test[curQ], PassedTime(), true);
            else
                testResults.Update(test[curQ], PassedTime(), false);

            NextQuestion();
        }

        async void NextQuestion()
        {
            Page nextPage = Models.TestSolving.TestKeeper.NextPage(test, curQ, testResults);
            NavigationPage.SetHasBackButton(nextPage, false);
            await Navigation.PushAsync(nextPage);
            Navigation.RemovePage(Navigation.NavigationStack[0]);
        }

    }
}
