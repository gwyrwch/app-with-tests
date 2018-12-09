using System;
using System.Collections.Generic;

using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace test132132.Views.UserProfile
{
    public partial class StatisticsPage : ContentPage
    {
        IEnumerable<double> PlotPoints { get; set; }

        public StatisticsPage(Models.UserStatistics userStatistics)
        {
            InitializeComponent();

            PlotView.HeightRequest = (double)1 / 3 * Application.Current.MainPage.Height;

            SolvedTestsLabel.Text = string.Format("{0}", userStatistics.SolvedTests.Count);
            TotalPointsLabel.Text = userStatistics.Points.Sum().ToString();

            var tests = new Models.TestPreview();
            tests.LoadAll();

            PlotPoints = userStatistics.Points.Zip(
                userStatistics.SolvedTests, (p, id) => (p, id).ToTuple()
            ).Select(
                (tp) => {
                    var count = tests.TestsToPreview.First((test) => test.Id == tp.Item2).Sum((q) => q.Points.Value);
                    return (double)tp.Item1 / count;
                }
            );
        }

        void OnPlotViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
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
                Color = ((Color)Application.Current.Resources["TextColor"]).ToSKColor(),
                StrokeWidth = 5
            };

            SKPaint thinPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = ((Color)Application.Current.Resources["Silver"]).ToSKColor(),
                StrokeWidth = 2
            };

            PlotPoints = PlotPoints.Select((y) => y * 0.8 + 0.1); // 0 .. 1 0.1 0.9

            float lx = (float)0.5 / 8 * info.Width;

            float space = (info.Width - lx) / PlotPoints.Count();

            for (int i = 0; i < PlotPoints.Count(); i += 1)
            {
                canvas.DrawOval(
                    lx + i * space + space / 2f,
                    info.Height - (float)PlotPoints.ElementAt(i) * info.Height, 
                    3, 3, paint
                );
            }

            for (int i = 0; i + 1 < PlotPoints.Count(); i += 1)
            {
                canvas.DrawLine(
                    lx + i * space + space / 2,
                    info.Height - (float)PlotPoints.ElementAt(i) * info.Height,
                    lx + i * space + space + space / 2, 
                    info.Height - (float)PlotPoints.ElementAt(i + 1) * info.Height,
                    paint
                );
            }

            canvas.DrawLine(
                lx, (float)info.Height - (float)0.05 * info.Height, info.Width, (float)info.Height - (float)0.05 * info.Height, thinPaint
            );
            canvas.DrawLine(
                lx, (float)0.05 * info.Height, lx, (float)info.Height - (float)0.05 * info.Height, thinPaint
            );

            var gridLines = new List<Tuple<float, Color>> {
                (1.0f, Color.FromHex("#4cd964")).ToTuple(),
                (0.8f, Color.FromHex("#ffcc00")).ToTuple(),
                (0.4f, Color.FromHex("#ff3b30")).ToTuple()
            };

            foreach (var line in gridLines)
            {
                float y = line.Item1 * 0.8f + 0.1f;

                thinPaint.Color = line.Item2.ToSKColor();
                thinPaint.StrokeWidth = 1;

                canvas.DrawLine(
                    lx, info.Height - y * info.Height, info.Width, info.Height - y * info.Height, thinPaint
                );


                // on the same y

                SKPaint textPaint = new SKPaint
                {
                    Color = ((Color)Application.Current.Resources["TextColor"]).ToSKColor(),
                };

                string str = ((int)(line.Item1 * 100f + 0.5f)).ToString();

                while (str.Length < 3) str = " " + str;

                float textWidth = textPaint.MeasureText("100");
                textPaint.TextSize = 0.05f * textPaint.TextSize / textWidth * info.Width;

                SKRect textBounds = new SKRect();
                textPaint.MeasureText(str, ref textBounds);

                canvas.DrawText(str, 0, info.Height - y * info.Height, textPaint);
            }
        }
    }
}

