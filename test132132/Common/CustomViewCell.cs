﻿using System;
using Xamarin.Forms;

namespace test132132.Common
{
    public class CustomViewCell : ViewCell
    {
        public CustomViewCell(Models.Test test, int index)
        {
            var testIndexLabel = new Label {
                TextColor = Color.FromHex("#007aff"),
                Text = String.Format("Test #{0}", index)
            };
            Grid.SetColumn(testIndexLabel, 0);
            Grid.SetRow(testIndexLabel, 0);

            var testTitleLabel = new Label {
                TextColor = Color.Silver,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                WidthRequest = 180,
                Text = test.Title
            };
            Grid.SetColumn(testTitleLabel, 0);
            Grid.SetRow(testTitleLabel, 1);

            var qCountLabel = new Label {
                Text = String.Format("Questions: {0}", test.Count),
                TextColor = Color.FromHex("#8e8e93"),
                FontSize = 15,
                HorizontalOptions = LayoutOptions.End
            };
            Grid.SetColumn(qCountLabel, 1);
            Grid.SetRow(qCountLabel, 0);

            var timeCountLabel = new Label {
                Text = String.Format("time: {0} min", test.TimeLimit.Value.Minutes),
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
                //new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) },
                //new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) },
                //new ColumnDefinition() { Width = new GridLength(20) }
          

            View = grid;
        }
    }
}