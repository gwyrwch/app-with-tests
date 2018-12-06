using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test132132.Views.TestPreview
{
    public partial class TestChosedPage : ContentPage
    {
        private Models.Test test;

        public TestChosedPage(Models.Test test)
        {
            this.test = test;
            InitializeComponent();

            SetLabelsValues();
        }

        void SetLabelsValues()
        {
            SubjectLabel.Text = test.Subject;
            TestTitleLabel.Text = test.Title;
            QuestionsNumberLabel.Text = test.Count.ToString();

            var totalTimeInSeconds = (test.Mode == Models.TimeMode.limitForTest ? 1 : test.Count) * test.TimeLimit.Value.TotalSeconds;
            int hours = (int)totalTimeInSeconds / 3600;
            int minutes = (int)(totalTimeInSeconds - hours * 3600) / 60;
            int seconds = (int)(totalTimeInSeconds - minutes * 60 - hours * 3600);

            Func<int, string, string> makeString = (int t, string s) => {
                return t == 0 ? "" : string.Format("{0} {1} ", t, s);
            };

            string hoursStr = makeString(hours, "h");
            string minutesStr = makeString(minutes, "min");
            string secondsStr = makeString(seconds, "sec");

            RequiredTimeLabel.Text = string.Join("", hoursStr, minutesStr, secondsStr);

        }

        async void Solve_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("", "", "");
        }
    }
}
