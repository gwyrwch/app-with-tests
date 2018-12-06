using System;
using System.Linq;
using System.Collections.ObjectModel;
using test132132.Models;
using test132132.ViewModels.Editor;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace test132132.Views.TestPreview
{
    public partial class MatchingQuestionPage : ContentPage
    {
        Test test;
        int curQ;
        Models.TestSolving.TestResults testResults;
        DateTime startTime;
        TimeSpan limit;
        bool answered;
        ViewModels.TestSolving.MatchingQSolveViewModel vm;

        public MatchingQuestionPage(Test test, int currentQuestion, Models.TestSolving.TestResults testResults)
        {
            this.testResults = testResults;
            this.test = test;
            this.curQ = currentQuestion;
            startTime = DateTime.Now;
            answered = false;

            InitializeComponent();

            MatchingQuestion question = (MatchingQuestion)test[curQ];

            BindingContext = vm = new ViewModels.TestSolving.MatchingQSolveViewModel {
                Lefts = new ObservableCollection<QPart>(
                    question.Lefts.Select((text) => new QPart { Text = text })
                ),
                Rights = new ObservableCollection<QPart>(
                    question.Rights.Select((text) => new QPart { Text = text })
                ),
                Relation = new ObservableCollection<int?>(
                    question.Relation
                )
            };

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

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas;
            SKPaint paint;

            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            canvas = surface.Canvas;

            canvas.Clear();
            paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.FromHex("#5ac8fa").ToSKColor(),
                StrokeWidth = 5
            };

            foreach (var pair in vm.Pairs)
            {
                (int left, int right) = pair;

                float y1 = LeftPartListView.RowHeight * (left + (float)0.5) / (float)LeftPartListView.Height * info.Height;
                float y2 = LeftPartListView.RowHeight * (right + (float)0.5) / (float)LeftPartListView.Height * info.Height;

                canvas.DrawLine(
                    0, y1,
                    info.Width, y2,
                    paint
                );
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (LeftPartListView.SelectedItem != null && RightPartListView.SelectedItem != null)
            {
                vm.AddPair(
                    vm.Lefts.IndexOf(LeftPartListView.SelectedItem as QPart),
                    vm.Rights.IndexOf(RightPartListView.SelectedItem as QPart)
                );

                LeftPartListView.SelectedItem = RightPartListView.SelectedItem = null;

                // redraw all pairs
                CanvasView.InvalidateSurface();
            }
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
            answered = true;

            if (vm.CorrectAnswer()) {
                testResults.Update(test[curQ], PassedTime(), true);
            } else {
                testResults.Update(test[curQ], PassedTime(), false);
            }

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
