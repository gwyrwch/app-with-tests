using System;
using System.Collections.Generic;
using test132132.Models;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace test132132.Views.TestPreview
{
    public partial class ResultsPage : ContentPage
    {
        private Test test;
        Models.TestSolving.TestResults testResults;

        public ResultsPage(Test test, Models.TestSolving.TestResults testResults)
        {
            this.test = test;
            this.testResults = testResults;
            InitializeComponent();

            var totalTime = (test.Mode == TimeMode.limitForTest ? 1 : test.Count) * test.TimeLimit.Value.TotalSeconds;
            var usedTime = testResults.UsedTime.TotalSeconds;
            var userPoints = testResults.TotalPoints;
            var totalPoints = test.Sum((q) => q.Points);

            EvaluateResultQuality(
                usedTime / totalTime, 
                userPoints / totalPoints.Value,
                testResults.CorrectAnswers / test.Count
            );

            PointsAmountLabel.Text = string.Format(
                "{0} points of {1}",
                userPoints.ToString(),
                totalPoints.ToString()
            );

            TimeLabel.Text = string.Format(
                "You used {0} seconds of {1}",
                (int)(usedTime + 0.5),
                totalTime
            );

            CorrectQLabel.Text = string.Format("Correct answers {0} of {1}", 
                testResults.CorrectAnswers.ToString(), 
                test.Count
            );
        }

        void EvaluateResultQuality(double timeStep, double pointsStep, double qStep)
        {
            double percent = (double)testResults.CorrectAnswers / test.Count * 100;
            Color progressColor;

            if (percent >= 90)
            {
                ResultQualityImage.Source = "QualityImages/excelent.png";
                progressColor = Color.FromHex("#4cd964");
            }

            else if (percent >= 50)
            {
                ResultQualityImage.Source = "QualityImages/good.png";
                progressColor = Color.FromHex("#ffcc00");
            }

            else {
                ResultQualityImage.Source = "QualityImages/failed.png";
                progressColor = Color.FromHex("#ff3b30");
            }

            TimeProgressBar.ProgressColor = PointsProgressBar.ProgressColor =
                CorrectQProgressBar.ProgressColor = progressColor;
            Task.Run(async () => await TimeProgressBar.ProgressTo(timeStep, 1000, Easing.SinOut));
            Task.Run(async () => await PointsProgressBar.ProgressTo(pointsStep, 1000, Easing.SinOut));
            Task.Run(async () => await CorrectQProgressBar.ProgressTo(qStep, 1000, Easing.SinOut));
        }
    }
}
