using System;
using System.Collections.Generic;
using test132132.Models;
using Xamarin.Forms;

namespace test132132.Views.TestPreview
{
    public partial class ResultsPage : ContentPage
    {
        private Test test;

        public ResultsPage(Test test, Models.TestSolving.TestResults testResults)
        {
            this.test = test;
            InitializeComponent();

            PointsAmountLabel.Text = string.Format("You earned {0} points.", testResults.TotalPoints.ToString());
            TimeLabel.Text = string.Format("You used {0} seconds.", testResults.UsedTime.Seconds);
            CorrectQLabel.Text = string.Format("Total number of correct answers: {0}", testResults.CorrectAnswers.ToString());
            IncorrectQLabel.Text = string.Format("and number of incorrect answers: {0}", (test.Count - testResults.CorrectAnswers).ToString());
        }
    }
}
