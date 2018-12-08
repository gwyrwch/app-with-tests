using System;
using Xamarin.Forms;

namespace test132132.Views.TestPreview
{
    public partial class OpenQuestionPage : ContentPage
    {
        Models.Test test;
        int curQ;
        Models.TestSolving.TestResults testResults;
        DateTime startTime;
        TimeSpan limit;
        bool answered;

        public OpenQuestionPage(Models.Test test, int currentQuestion, Models.TestSolving.TestResults testResults)
        {
            this.testResults = testResults;
            this.test = test;
            this.curQ = currentQuestion;
            startTime = DateTime.Now;
            answered = false;

            InitializeComponent();

            limit = test.TimeLimit.Value;
            if (test.Mode == Models.TimeMode.limitForQuestion)
            {
                TimeLeftBar.Progress = 1;

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

                        TimeLeftBar.Progress = (limit - PassedTime()).TotalSeconds / limit.TotalSeconds;
                        return true;
                    }
                );
            } else {
                TimeLeftBar.Progress = (limit - testResults.UsedTime).TotalSeconds / limit.TotalSeconds;

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

                        TimeLeftBar.Progress = (limit - testResults.UsedTime - PassedTime()).TotalSeconds /
                            limit.TotalSeconds;
                        return true;
                    }
                );
            }

            QuestionLabel.Text = string.Join("", 
                (currentQuestion + 1).ToString(), 
                ". ",
                test[currentQuestion].Text
            );
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
            if (string.IsNullOrEmpty(AnswerEntry.Text))
                return;

            answered = true;

            if (AnswerEntry.Text.ToLower() == ((Models.OpenQuestion)test[curQ]).Answer.ToLower())
            {
                testResults.Update(test[curQ], PassedTime(), true);
            } else {
                testResults.Update(test[curQ], PassedTime(), false);
            }

            NextQuestion();
        }

        async void NextQuestion()
        {
            Page nextPage = Models.TestSolving.TestKeeper.NextPage(test, curQ, testResults);
            await Navigation.PushAsync(nextPage);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}