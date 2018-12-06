using System;
using Xamarin.Forms;

namespace test132132.Common
{
    public class CustomViewCell : ViewCell
    {
        public Models.Test test;
        public CustomViewCell(Models.Test test, int index)
        {
            this.test = test;

            var testIndexLabel = new Label {
                TextColor = (Color)Application.Current.Resources["MainColor"],
                Text = String.Format(iOS.AppResources.TestViewCellTestTemplate, index)
            };
            Grid.SetColumn(testIndexLabel, 0);
            Grid.SetRow(testIndexLabel, 0);

            var testTitleLabel = new Label {
                TextColor = (Color)Application.Current.Resources["Silver"],
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                WidthRequest = 180,
                Text = test.Title
            };
            Grid.SetColumn(testTitleLabel, 0);
            Grid.SetRow(testTitleLabel, 1);

            var qCountLabel = new Label {
                Text = String.Format(iOS.AppResources.TestViewCellQuestionTemplate, test.Count),
                TextColor = (Color)Application.Current.Resources["MainGreyColor"],
                FontSize = 15,
                HorizontalOptions = LayoutOptions.End
            };
            Grid.SetColumn(qCountLabel, 1);
            Grid.SetRow(qCountLabel, 0);

            var timeCountLabel = new Label {
                Text = String.Format(iOS.AppResources.TestViewCellTimeTemplate, test.TimeLimit.Value.Minutes),
                TextColor = Color.FromHex("#4cd964"),
                FontSize = 13,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.End
            };
            Grid.SetColumn(timeCountLabel, 1);
            Grid.SetRow(timeCountLabel, 1);

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(10, 0, 10, 0),
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)}
                },
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star)}
                },
                Children = {
                    testIndexLabel,
                    testTitleLabel,
                    qCountLabel,
                    timeCountLabel
                }
            };

            View = grid;
        }
    }
}
